using Godot;
using System;

public class BoulderGenerator : Path2D
{
    static private Random random = new Random();

    [Export] public PackedScene Boulder;

    [Signal] public delegate void BoulderGenerated(Boulder boulder);

    private PathFollow2D SpawnLocation;
    private Timer Timer;

    public override void _Ready()
    {
        this.SpawnLocation = this.GetNode<PathFollow2D>("SpawnLocation");
        this.Timer = this.GetNode<Timer>("Timer");
    }

    public void Start()
    {
        this.Timer.Start();
    }

    public void Stop()
    {
        this.Timer.Stop();
    }

    private void OnTimerTimeout()
    {
        this.SpawnLocation.UnitOffset = (float) random.Next(0, 100) / 100;

        Boulder boulder = (Boulder) Boulder.Instance();
        boulder.GlobalPosition = this.SpawnLocation.GlobalPosition;
        this.GetTree().CurrentScene.AddChild(boulder);

        this.EmitSignal(nameof(BoulderGenerated), boulder);
    }
}
