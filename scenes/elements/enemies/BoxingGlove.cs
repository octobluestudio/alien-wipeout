using Godot;
using System;

public class BoxingGlove : Area2D
{
    private const float MaxSpeed = 300f;

    [Export] public Vector2 Direction = Vector2.Left;
    [Export] private float Strength = 450f;
    [Export] private float Speed = 0f;

    private DodgeDetector DodgeDetector;
    private AnimationPlayer AnimationPlayer;

    public EnemyProperties.Type Type = EnemyProperties.Type.BoxingGlove;

    public override void _Ready()
    {
        this.DodgeDetector = this.GetNode<DodgeDetector>("DodgeDetector");
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");

        this.Scale = new Vector2(-this.Direction.x * Math.Abs(this.Scale.x), this.Scale.y);

        this.Appear();
    }

    public override void _PhysicsProcess(float delta)
    {
        this.Position += this.Direction * new Vector2(this.Speed * delta, 0);
    }

    public void Appear()
    {
        this.AnimationPlayer.Play("Appear");
    }

    public void Launch()
    {
        this.Speed = MaxSpeed;
        this.AnimationPlayer.Play("Idle");
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



