using Godot;

public class CheckPoint : Area2D
{
    [Export] public string ID = "MID";

    [Signal] public delegate void CheckPointValidated(string id);

    private AnimationPlayer AnimationPlayer;
    private Position2D StartPosition;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.StartPosition = this.GetNode<Position2D>("StartPosition");

        this.AnimationPlayer.Play("Idle");
        
        this.Connect(nameof(CheckPointValidated), this.GetNode<GameState>("/root/GameState"), "OnCheckPointValidated");
    }

    public Vector2 StartingPoint()
    {
        return this.StartPosition.GlobalPosition;
    }

    public void Validate()
    {
        this.AnimationPlayer.Play("Validated");
        this.EmitSignal(nameof(CheckPointValidated), this.ID);
    }

    public void Remove()
    {
        this.QueueFree();
    }
    
    private void OnCheckPointBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        this.Validate();
    }
}
