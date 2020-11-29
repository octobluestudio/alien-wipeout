using Godot;
using System;

public class Character : KinematicBody2D
{
    [Export] public float Speed = 150.0f;
    [Export] public float Friction = 0.6f;
    [Export] public float Gravity = 22.0f;
    [Export] public float JumpForce = 400.0f;

    [Signal] public delegate void CharacterKilled();

    private Sprite Sprite;
    private AnimationPlayer AnimationPlayer;
    private Timer DisableTimer;

    private Vector2 Velocity = Vector2.Zero;
    private bool Disabled = false;

    public void Squash()
    {
        this.AnimationPlayer.Play("Squash");
        this.Disable();
    }

    internal void BounceBack(Vector2 direction, float strength)
    {
        this.Velocity = this.MoveAndSlide(direction * strength, Vector2.Up);
        this.AnimationPlayer.Play("Fall");
        this.Disable();
    }

    public void Kill()
    {
        this.EmitSignal(nameof(CharacterKilled));
    }

    private void Disable()
    {
        this.Disabled = true;
        this.DisableTimer.Start();
    }

    public override void _Ready()
    {
        this.Sprite = this.GetNode<Sprite>("Sprite");
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.DisableTimer = this.GetNode<Timer>("DisableTimer");
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVelocity = this.Disabled ? Vector2.Zero : ControlsUtil.DirectionFromInput();

        this.Velocity = this.MoveAndSlide(this.GetRealVelocityFromInputVelocity(inputVelocity, delta), Vector2.Up);

        if (! this.Disabled)
        {
            this.AnimationPlayer.Play(this.GetAnimation(inputVelocity));
            if (inputVelocity != Vector2.Zero)
            {
                this.Sprite.FlipH = inputVelocity.x < 0;
            }
        }
    }

    private string GetAnimation(Vector2 inputVelocity)
    {
        if (!this.IsOnFloor())
        {
            if (this.Velocity.y < 0)
            {
                return "Jump";
            }

            return "Fall";
        }
        
        return (Math.Abs(inputVelocity.x) > 0 && this.IsOnFloor()) ? "Walk" : "Idle";
    }

    private Vector2 GetRealVelocityFromInputVelocity(Vector2 inputVelocity, float delta)
    {
        float frictionFactor = this.IsOnFloor() ? 1f : 0.01f;
        float x = !inputVelocity.Equals(Vector2.Zero) ?
            inputVelocity.x * this.Speed :
            Mathf.Lerp(this.Velocity.x, 0, this.Friction * frictionFactor);

        var gravityFactor = (this.IsSlidingOnWall()) ? 3 : 1;

        float y = this.IsJumping() ?
            -this.JumpForce :
            this.Velocity.y + (this.Gravity / gravityFactor);

        return new Vector2(x, y);
    }

    private bool IsSlidingOnWall()
    {
        return this.IsOnWall() && !this.IsOnFloor() && this.Velocity.y >= 0;
    }

    private bool IsJumping()
    {
        return (this.IsOnFloor() || this.IsOnWall()) && ControlsUtil.IsJumpJustPressed();
    }

    private void OnDisableTimerTimeout()
    {
        this.Disabled = false;
    }
}
