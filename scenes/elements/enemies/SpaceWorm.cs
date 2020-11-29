using Godot;
using System;

public class SpaceWorm : Area2D
{
    private AnimationPlayer AnimationPlayer;
    private Timer WaitTimer;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.WaitTimer = this.GetNode<Timer>("WaitTimer");
    }

    public void Attack()
    {
        if (!this.AnimationPlayer.IsPlaying() && this.WaitTimer.TimeLeft == 0)
        {
            this.WaitTimer.Start();
        }
    }
    
    private void OnSpaceWormBodyEntered(PhysicsBody2D body)
    {
        ((Character)body).Squash();
    }

    private void OnWaitTimertimeout()
    {
        this.AnimationPlayer.Play("Attack");
    }
}

