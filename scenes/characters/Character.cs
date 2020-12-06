using Godot;
using System;

public class Character : KinematicBody2D
{
    private Sprite Sprite;
    private AnimationTree AnimationTree;
    private AnimationNodeStateMachinePlayback AnimationStateMachine;
    private Timer DisableTimer;
    private AudioStreamPlayer JumpSound;
    private AudioStreamPlayer PunchSound;
    private AudioStreamPlayer ChompSound;

    [Export] public float Speed = 150.0f;
    [Export] public float Friction = 0.6f;
    [Export] public float Gravity = 22.0f;
    [Export] public float JumpForce = 400.0f;

    [Signal] public delegate void Killed(State state);
    [Signal] public delegate void Punched();
    [Signal] public delegate void Dodged(EnemyProperties.Type enemyType);
    [Signal] public delegate void Won();

    public enum State { Idle, Walk, Jump, Fall, Squash, Chomp, Dance };

    private Vector2 Velocity = Vector2.Zero;
    private bool Disabled = true;
    private State CurrentState;
    private bool IsDying = false;
    private bool Immortal = false;

    public override void _Ready()
    {
        this.Sprite = this.GetNode<Sprite>("Sprite");
        this.AnimationTree = this.GetNode<AnimationTree>("AnimationTree");
        this.AnimationStateMachine = (AnimationNodeStateMachinePlayback) this.AnimationTree.Get("parameters/playback");
        this.DisableTimer = this.GetNode<Timer>("DisableTimer");
        this.JumpSound = this.GetNode<AudioStreamPlayer>("Audio/Jump");
        this.PunchSound = this.GetNode<AudioStreamPlayer>("Audio/Punch");
        this.ChompSound = this.GetNode<AudioStreamPlayer>("Audio/Chomp");

        this.Animate(State.Idle);
    }

    public void WakeUp()
    {
        this.Disabled = false;
        this.Immortal = false;
    }

    public void Win()
    {
        this.Immortal = true;
        this.Disable();
        this.Animate(State.Dance);
        this.EmitSignal(nameof(Won));
    }

    public void Squash()
    {
        this.Velocity = new Vector2(0, 200);
        this.Dying(State.Squash);
    }

    public void Chomp()
    {
        this.Velocity = new Vector2(0, 0);
        this.Dying(State.Chomp);
        this.ChompSound.Play();
    }

    private void Dying(State state)
    {
        if (!this.CanDie())
        {
            // You can't die twice
            return;
        }

        this.Animate(state);
        this.Disable();
        this.IsDying = true;
    }

    public void Punch(Vector2 direction, float strength)
    {
        if (this.IsDying)
        {
            return;
        }

        this.Velocity = this.MoveAndSlide(direction * strength, Vector2.Up);
        this.Animate(State.Fall);
        this.DisableFor(0.4f);

        this.PunchSound.Play();

        this.EmitSignal(nameof(Punched));
    }

    public void Dodge(EnemyProperties.Type enemyType)
    {
        if (this.IsDying)
        {
            return;
        }

        this.EmitSignal(nameof(Dodged), enemyType);
    }

    public void Smashed()
    {
        this.Kill(State.Squash);
    }

    public void Chomped()
    {
        this.Kill(State.Chomp);
    }

    public void Fell()
    {
        this.Kill(State.Fall);
    }

    private void Kill(State state)
    {
        this.EmitSignal(nameof(Killed), state);
    }

    private void DisableFor(float seconds)
    {
        this.Disable();
        this.DisableTimer.Start(seconds);
    }

    private void Disable()
    {
        this.Disabled = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (this.IsJumping())
        {
            this.JumpSound.Play();
        }

        var originalVelocity = this.Velocity;

        Vector2 inputVelocity = this.Disabled ? Vector2.Zero : ControlsUtil.DirectionFromInput();

        this.Velocity = this.MoveAndSlide(this.GetRealVelocityFromInputVelocity(inputVelocity, delta), Vector2.Up);
        this.Bounce(originalVelocity);

        if (!this.Disabled)
        {
            this.Animate(this.GetStateFromVelocity(inputVelocity));
            if (inputVelocity != Vector2.Zero)
            {
                this.Sprite.FlipH = inputVelocity.x < 0;
            }
        }
    }

    private Vector2 GetRealVelocityFromInputVelocity(Vector2 inputVelocity, float delta)
    {
        float frictionFactor = this.IsOnFloor() ? 1f : 0.01f;
        float x = !inputVelocity.Equals(Vector2.Zero) ?
            inputVelocity.x * this.Speed :
            Mathf.Lerp(this.Velocity.x, 0, this.Friction * frictionFactor);

        var gravityFactor = (this.IsSlidingOnWall()) ? 3 : 1;

        float y = this.IsJumping() && !this.Disabled ?
            -this.JumpForce :
            this.Velocity.y + (this.Gravity / gravityFactor);

        return new Vector2(x, y);
    }

    private void Bounce(Vector2 originalVelocity)
    {
        Bouncer bouncer = this.GetBouncerCollider();
        if (bouncer != null)
        {
            float bounceForce = bouncer.BounceForce();
            Vector2 normal = this.GetFloorNormal();
            float strength = Math.Min(originalVelocity.Length() * bounceForce, JumpForce * 1.5f);
            this.Velocity = (normal * strength - originalVelocity).Normalized() * strength; ;
        }
    }

    private Bouncer GetBouncerCollider()
    {
        for (var i = 0; i < this.GetSlideCount(); i++)
        {
            var collider = this.GetSlideCollision(i).Collider;

            if (collider is Bouncer)
            {
                return (Bouncer)collider;
            }
        }

        return null;
    }

    private State GetStateFromVelocity(Vector2 inputVelocity)
    {
        if (!this.IsOnFloor())
        {
            if (this.Velocity.y < 0)
            {
                return State.Jump;
            }

            return State.Fall;
        }
        
        return (Math.Abs(inputVelocity.x) > 0 && this.IsOnFloor()) ? State.Walk : State.Idle;
    }

    private void Animate(State state)
    {
        if (this.CurrentState == state || this.IsDying)
        {
            return;
        }

        this.CurrentState = state;
        this.AnimationStateMachine.Travel(state.ToString("G"));
    }

    private bool IsSlidingOnWall()
    {
        return this.IsOnWall() && !this.IsOnFloor() && this.Velocity.y >= 0;
    }

    private bool IsJumping()
    {
        return !this.Disabled && (this.IsOnFloor() || this.IsSlidingOnWall()) && ControlsUtil.IsJumpJustPressed();
    }

    private bool CanDie()
    {
        return !this.Immortal && !this.IsDying;
    }

    private void OnDisableTimerTimeout()
    {
        if (this.IsDying)
        {
            return;
        }

        this.Disabled = false;
    }
}
