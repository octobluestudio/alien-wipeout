using Godot;
using System;

public class CameraZone : Area2D
{
    [Signal] public delegate void CameraZoneModified(int newTop, int newBottom);
    [Signal] public delegate void CameraZoneRestored();

    private Position2D TopPosition;
    private Position2D BottomPosition;

    public override void _Ready()
    {
        this.TopPosition = this.GetNode<Position2D>("TopPosition");
        this.BottomPosition = this.GetNode<Position2D>("BottomPosition");
    }

    private void OnCameraZoneBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        var top = this.TopPosition.GlobalPosition.y;
        var bottom = this.BottomPosition.GlobalPosition.y;

        this.EmitSignal(nameof(CameraZoneModified), top, bottom);
    }
    
    private void OnCameraZoneBodyExited(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        this.EmitSignal(nameof(CameraZoneRestored));
    }
}
