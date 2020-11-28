using Godot;
using System;

public class SpaceWorm : Area2D
{
    private AnimationPlayer AnimationPlayer;

    private bool Attacking;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public async void Attack()
    {
        if (this.Attacking)
        {
            return;
        }

        this.Attacking = true;

        await this.ToSignal(this.GetTree().CreateTimer(1), "timeout");
        this.AnimationPlayer.Play("Attack");

        await this.ToSignal(this.GetTree().CreateTimer(1), "timeout");
        this.AnimationPlayer.Play("Burry");

        this.Attacking = false;
    }
    
    private void OnSpaceWormBodyEntered(PhysicsBody2D body)
    {
        ((Character)body).Squash();
    }
}

