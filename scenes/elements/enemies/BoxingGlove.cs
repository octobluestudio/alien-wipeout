using Godot;
using System;

public class BoxingGlove : Area2D
{
    [Export] private Vector2 Direction = Vector2.Left;
    [Export] private float Strength = 450f;
    [Export] private float Speed = 300f;

    private DodgeDetector DodgeDetector;

    public EnemyProperties.Type Type = EnemyProperties.Type.BoxingGlove;

    public override void _Ready()
    {
        this.DodgeDetector = this.GetNode<DodgeDetector>("DodgeDetector");
    }

    public override void _PhysicsProcess(float delta)
    {
        this.Position += this.Direction * new Vector2(this.Speed * delta, 0);
    }

    private void Destroy()
    {
        this.QueueFree();
    }

    private void OnBoxingGloveBodyEntered(PhysicsBody2D body)
    {
        if (body is Character) {
            this.DodgeDetector.Cancel();
            ((Character)body).Punch(this.Direction + new Vector2(0, -0.5f), this.Strength);
        }
        
        this.Destroy();
    }
    
    private void OnCharacterDodged(Character character)
    {
        character.Dodge(this.Type);
    }

    private void OnscreenExited()
    {
        this.Destroy();
    }
}



