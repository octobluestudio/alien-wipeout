using Godot;
using System;

public class Character : KinematicBody2D
{
    private Sprite Sprite;
    private AnimationTree AnimationTree;
    private AnimationNodeStateMachinePlayback AnimationStateMachine;
    private Timer DisableTimer;

    [Export] public float Speed = 150.0f;
    [Export] public float Friction = 0.6f;
    [Export] public float Gravity = 22.0f;
    [Export] public float JumpForce = 400.0f;

    [Signal] public delegate void CharacterKilled(State state);
    
    public enum State { Idle, Walk, Jump, Fall, Squash, Chomp };

    private Vector2 Velocity = Vector2.Zero;
    private bool Disabled = false;
    private State CurrentState;
    private bool IsDying = false;

    public override void _Ready()
    {
        this.Sprite = this.GetNode<Sprite>("Sprite");
        this.AnimationTree = this.GetNode<AnimationTree>("AnimationTree");
        this.AnimationStateMachine = (AnimationNodeStateMachinePlayback) this.AnimationTree.Get("parameters/playback");
        this.DisableTimer = this.GetNode<Timer>("DisableTimer");

        this.Animate(State.Idle);
    }

    public void Squash()
    {
        this.Dying(State.Squash);
    }

    public void Chomp()
    {
        this.Dying(State.Chomp);
    }

    private void Dying(State state)
    {
        if (this.IsDying)
        {
            // You can't die twice
            return;
        }

        this.Animate(state);
        this.Disable();
        this.IsDying = true;
    }

    public void BounceBack(Vector2 direction, float strength)
    {
        this.Velocity = this.MoveAndSlide(direction * strength, Vector2.Up);
        this.Animate(State.Fall);
        this.DisableFor(0.4f);
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
        this.EmitSignal(nameof(CharacterKilled), state);
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
        Vector2 inputVelocity = this.Disabled ? Vector2.Zero : ControlsUtil.DirectionFromInput();

        this.Velocity = this.MoveAndSlide(this.GetRealVelocityFromInputVelocity(inputVelocity, delta), Vector2.Up);

        if (! this.Disabled)
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
        return (this.IsOnFloor() || this.IsSlidingOnWall()) && ControlsUtil.IsJumpJustPressed();
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
