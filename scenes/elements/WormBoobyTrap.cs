using Godot;
using System;

public class WormBoobyTrap : Area2D
{
    private SpaceWorm SpaceWorm;

    public override void _Ready()
    {
        this.SpaceWorm = this.GetNode<SpaceWorm>("SpaceWorm");
    }

    private void OnWormBoobyTrapBodyEntered(PhysicsBody2D body)
    {
        this.SpaceWorm.Attack();
    }
}
