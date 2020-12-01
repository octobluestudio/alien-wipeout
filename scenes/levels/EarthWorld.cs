using Godot;
using System;

public class EarthWorld : Node2D
{
    public enum Event { Started, DodgedBoulder, DodgedGlove, DodgedWorm, Punched, Fell, Eaten, Smashed };

    [Signal] public delegate void GameEvent(Event gameEvent);

    private HUD HUD;
    private BoulderGenerator BoulderGenerator;
    private ImpactLocator ImpactLocator;

    public override void _Ready()
    {
        this.HUD = this.GetNode<HUD>("HUD");
        this.BoulderGenerator = this.GetNode<BoulderGenerator>("BoulderGenerator");
        this.ImpactLocator = this.GetNode<ImpactLocator>("ImpactLocator");

        this.ActivateTerrain();
        this.EmitSignal(nameof(GameEvent), Event.Started);
    }

    private void ActivateTerrain()
    {
        this.BoulderGenerator.Start();
    }

    private void DeactivateTerrain()
    {
        this.BoulderGenerator.Stop();
    }

    private async void OnCharacterKilled(Character.State state)
    {
        this.HUD.StopStopWatch();

        this.DeactivateTerrain();

        this.EmitSignal(nameof(GameEvent), CharacterStateToEvent(state));

        await this.ToSignal(this.GetTree().CreateTimer(2), "timeout");

        this.GetTree().ChangeScene("res://scenes/menus/GameOverMenu.tscn");
    }

    private static Event CharacterStateToEvent(Character.State state)
    {
        switch (state)
        {
            case Character.State.Chomp:
                return Event.Eaten;
            case Character.State.Squash:
                return Event.Smashed;
            default:
                return Event.Fell;
        }
    }
    
    private void OnBoulderGenerated(Boulder boulder)
    {
        this.ImpactLocator.RegisterBoulder(boulder);
    }
}
