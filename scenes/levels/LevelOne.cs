using Godot;
using System;

public class LevelOne : Node2D
{
    public enum Event { Started, DodgedBoulder, DodgedGlove, DodgedWorm, Punched, Fell, Eaten, Smashed };

    private HUD HUD;
    private Terrain Terrain;
    private ImpactLocator ImpactLocator;
    private RemoteTransform2D BoulderGeneratorFollow;

    public override void _Ready()
    {
        this.HUD = this.GetNode<HUD>("HUD");
        this.Terrain = this.GetNode<Terrain>("Terrain");
        this.ImpactLocator = this.GetNode<ImpactLocator>("ImpactLocator");
        this.BoulderGeneratorFollow = this.GetNode<RemoteTransform2D>("Character/BoulderGeneratorFollow");

        this.BoulderGeneratorFollow.RemotePath = new NodePath("../../Terrain/BoulderGenerator");
        this.Terrain.Activate();
        this.HUD.ReactTo(Event.Started);
    }

    private async void OnCharacterKilled(Character.State state)
    {
        this.HUD.StopStopWatch();
        this.HUD.ReactTo(CharacterStateToEvent(state));

        this.Terrain.Deactivate();

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
