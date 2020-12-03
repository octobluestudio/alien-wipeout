using Godot;
using System;

public class LevelOne : Node2D
{
    public enum Event { Started, DodgedBoulder, DodgedGlove, DodgedWorm, Punched, Fell, Eaten, Smashed, Win };

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

    private async void OnCharacterWon()
    {
        this.HUD.StopStopWatch();
        this.HUD.ReactTo(Event.Win);

        this.Terrain.Deactivate();

        await this.ToSignal(this.GetTree().CreateTimer(2), "timeout");

        this.GetTree().ChangeScene("res://scenes/menus/LevelCompleteMenu.tscn");
    }

    private async void OnCharacterKilled(Character.State state)
    {
        this.HUD.StopStopWatch();
        this.HUD.ReactTo(CharacterStateToEvent(state));

        this.Terrain.Deactivate();

        await this.ToSignal(this.GetTree().CreateTimer(2), "timeout");

        this.GetTree().ChangeScene("res://scenes/menus/GameOverMenu.tscn");
    }
    
    private void OnCharacterDodged(EnemyProperties.Type enemyType)
    {
        switch(enemyType)
        {
            case EnemyProperties.Type.SpaceWorm:
                this.HUD.ReactTo(Event.DodgedWorm);
                break;
            case EnemyProperties.Type.Boulder:
                this.HUD.ReactTo(Event.DodgedBoulder);
                break;
            case EnemyProperties.Type.BoxingGlove:
                this.HUD.ReactTo(Event.DodgedGlove);
                break;
        }
    }

    private void OnCharacterPunched()
    {
        this.HUD.ReactTo(Event.Punched);
    }

    private void OnBoulderGenerated(Boulder boulder)
    {
        this.ImpactLocator.RegisterBoulder(boulder);
    }
}
