using Godot;
using System;

public class EarthWorld : Node2D
{
    public enum Event { Started, DodgedBoulder, DodgedGlove, DodgedWorm, Fell, Punched, Eaten, Smashed };

    [Signal] public delegate void GameEvent(Event gameEvent);

    private HUD HUD;
    private ImpactLocator ImpactLocator;

    public override void _Ready()
    {
        this.HUD = this.GetNode<HUD>("HUD");
        this.ImpactLocator = this.GetNode<ImpactLocator>("ImpactLocator");

        this.EmitSignal(nameof(GameEvent), Event.Started);
    }

    private void OnCharacterKilled()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");

        this.HUD.StopStopWatch();
    }
    
    private void OnBoulderGenerated(Boulder boulder)
    {
        this.ImpactLocator.RegisterBoulder(boulder);
    }
}
