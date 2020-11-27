using Godot;
using System;

public class Boulder : RigidBody2D
{
    private Area2D CollisionDetector;

    public override void _Ready()
    {
        this.CollisionDetector = this.GetNode<Area2D>("CollisionDetector");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (this.ContactMonitor && this.TouchedGround())
        {
            this.ContactMonitor = false;
            this.ContactsReported = 0;

            this.CollisionDetector.Monitoring = false;
            this.Mode = ModeEnum.Static;
        }
    }

    private bool TouchedGround()
    {
        return this.GetCollidingBodies().Count > 0;
    }

    private void OnCollisionDetectorBodyEntered(PhysicsBody2D body)
    {
        ((Character)body).Kill();
    }
}
