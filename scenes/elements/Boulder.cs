using Godot;
using System;

public class Boulder : RigidBody2D
{
    private Area2D CollisionDetector;
    private AlertArrow AlertArrow;

    public override void _Ready()
    {
        this.CollisionDetector = this.GetNode<Area2D>("CollisionDetector");
        this.AlertArrow = this.GetNode<AlertArrow>("AlertArrow");

        this.AlertArrow.UpdateFollowedObjectGlobalPosition(this.GlobalPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        this.AlertArrow.UpdateFollowedObjectGlobalPosition(this.GlobalPosition);

        if (this.ContactMonitor && this.TouchedGround())
        {
            this.ContactMonitor = false;
            this.ContactsReported = 0;

            this.QueueFree();
        }
    }

    private bool TouchedGround()
    {
        return this.GetCollidingBodies().Count > 0;
    }

    private void OnCollisionDetectorBodyEntered(PhysicsBody2D body)
    {
        ((Character)body).Squash();
    }
    
    private void OnVisibilityNotifier2DScreenEntered()
    {
        this.AlertArrow.Disable();
    }
}
