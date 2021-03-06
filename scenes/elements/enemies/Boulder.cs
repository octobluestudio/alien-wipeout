using Godot;
using System;

public class Boulder : RigidBody2D
{
    private Area2D CollisionDetector;
    private DodgeDetector DodgeDetector;
    private RayCast2D ImpactLocator;
    private AnimationPlayer AnimationPlayer;

    private string identifier;

    [Signal] public delegate void ImminentImpact(string Identifier, Vector2 position);
    [Signal] public delegate void Impact(string Identifier);

    public EnemyProperties.Type Type = EnemyProperties.Type.Boulder;

    public override void _Ready()
    {
        this.identifier = Guid.NewGuid().ToString();

        this.CollisionDetector = this.GetNode<Area2D>("CollisionDetector");
        this.DodgeDetector = this.GetNode<DodgeDetector>("DodgeDetector");
        this.ImpactLocator = this.GetNode<RayCast2D>("ImpactLocator");
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (this.TouchedGround())
        {
            this.Explode();
        }

        if (this.ImpactLocator.IsColliding())
        {
            var impactPointGlobalPosition = this.ImpactLocator.GetCollisionPoint();
            this.EmitSignal(nameof(ImminentImpact), this.identifier, impactPointGlobalPosition);
            this.ImpactLocator.Enabled = false;
        }
    }

    public void Explode()
    {
        this.EmitSignal(nameof(Impact), this.identifier);
        this.SetDeferred("mode", ModeEnum.Static);
        this.Rotation = 0;
        this.AnimationPlayer.Play("Explode");
    }

    private bool TouchedGround()
    {
        return this.GetCollidingBodies().Count > 0;
    }

    private void OnCollisionDetectorBodyEntered(PhysicsBody2D body)
    {
        this.DodgeDetector.Cancel();
        ((Character)body).Squash();
        this.CollisionDetector.SetDeferred("monitoring", false);
    }
    
    private void OnCharacterDodged(Character character)
    {
        character.Dodge(this.Type);
    }
}
