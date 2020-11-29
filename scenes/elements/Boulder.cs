using Godot;
using System;

public class Boulder : RigidBody2D
{
    private Area2D CollisionDetector;
    private RayCast2D ImpactLocator;

    private string identifier;

    [Signal] public delegate void ImminentImpact(string Identifier, Vector2 position);
    [Signal] public delegate void Impact(string Identifier);

    public override void _Ready()
    {
        this.identifier = Guid.NewGuid().ToString();

        this.CollisionDetector = this.GetNode<Area2D>("CollisionDetector");
        this.ImpactLocator = this.GetNode<RayCast2D>("ImpactLocator");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (this.TouchedGround())
        {
            this.EmitSignal(nameof(Impact), this.identifier);
            this.QueueFree();
        }

        if (this.ImpactLocator.IsColliding())
        {
            var impactPointGlobalPosition = this.ImpactLocator.GetCollisionPoint();
            this.EmitSignal(nameof(ImminentImpact), this.identifier, impactPointGlobalPosition);
            this.ImpactLocator.Enabled = false;
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
}

