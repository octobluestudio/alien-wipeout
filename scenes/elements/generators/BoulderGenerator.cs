using Godot;
using System;

public class BoulderGenerator : Path2D
{
    static private Random random = new Random();

    [Export] public bool Active = true;

    [Signal] public delegate void BoulderGenerated(Boulder boulder);

    public PackedScene Boulder;
    private PathFollow2D SpawnLocation;
    private Timer Timer;

    public override void _Ready()
    {
        this.Boulder = ResourceLoader.Load<PackedScene>("res://scenes/elements/enemies/Boulder.tscn");
        this.SpawnLocation = this.GetNode<PathFollow2D>("SpawnLocation");
        this.Timer = this.GetNode<Timer>("Timer");
    }

    public void Start()
    {
        if (!this.Active)
        {
            return;
        }

        this.Timer.Start();
    }

    public void Stop()
    {
        this.Timer.Stop();
    }

    private void OnTimerTimeout()
    {
        if (!this.Active)
        {
            return;
        }

        this.SpawnLocation.UnitOffset = (float) random.Next(0, 100) / 100;

        Boulder boulder = (Boulder) Boulder.Instance();
        boulder.GlobalPosition = this.SpawnLocation.GlobalPosition;
        this.GetTree().CurrentScene.AddChild(boulder);

        this.EmitSignal(nameof(BoulderGenerated), boulder);
    }
}
