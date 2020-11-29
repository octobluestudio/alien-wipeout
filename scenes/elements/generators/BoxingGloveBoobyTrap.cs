using Godot;
using System;

public class BoxingGloveBoobyTrap : Node2D
{
    private PackedScene BoxingGlove;
    private Position2D LaunchPoint;

    public override void _Ready()
    {
        this.BoxingGlove = ResourceLoader.Load<PackedScene>("res://scenes/elements/enemies/BoxingGlove.tscn");
        this.LaunchPoint = this.GetNode<Position2D>("LaunchPoint");
    }

    private void OnDetectorBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        var boxingGlove = (BoxingGlove) this.BoxingGlove.Instance();
        boxingGlove.GlobalPosition = this.LaunchPoint.GlobalPosition;
        this.GetTree().CurrentScene.CallDeferred("add_child", boxingGlove);
    }
}
