using Godot;
using System;

public class BaseLevel : Node2D
{
    public enum Event { Greetings, Started, DodgedBoulder, DodgedGlove, DodgedWorm, Punched, Fell, Eaten, Smashed, Win };

    private Character Character;
    private HUD HUD;
    private Terrain Terrain;
    private ImpactLocator ImpactLocator;
    private RemoteTransform2D BoulderGeneratorFollow;
    private RemoteTransform2D CameraFollow;
    private AudioStreamPlayer Music;
    private AudioStreamPlayer LostSound;

    private GameState GameState;

    protected void InitNodes(GameState.Level level)
    {
        this.GameState = (GameState)GetNode("/root/GameState");
        this.GameState.SetCurrentLevel(level);

        this.Character = this.GetNode<Character>("Character");
        this.HUD = this.GetNode<HUD>("HUD");
        this.Terrain = this.GetNode<Terrain>("Terrain");
        this.ImpactLocator = this.GetNode<ImpactLocator>("ImpactLocator");
        this.BoulderGeneratorFollow = this.GetNode<RemoteTransform2D>("Character/BoulderGeneratorFollow");
        this.CameraFollow = this.GetNode<RemoteTransform2D>("Character/CameraFollow");
        this.Music = this.GetNode<AudioStreamPlayer>("Audio/Music");
        this.LostSound = this.GetNode<AudioStreamPlayer>("Audio/LostSound");

        this.BoulderGeneratorFollow.RemotePath = new NodePath("../../Terrain/BoulderGenerator");
    }

    public void Start()
    {
        this.Terrain.Present();
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

    private void OnTerrainPresentationStarted()
    {
        this.HUD.ReactTo(Event.Greetings);
    }

    private void OnTerrainPresentationEnded()
    {
        this.CameraFollow.RemotePath = new NodePath("../../Terrain/Camera");

        this.Terrain.Activate();
        this.Character.WakeUp();
        this.HUD.StartStopWatch();

        this.HUD.ReactTo(Event.Started);
    }

    private async void OnCharacterWon()
    {
        this.HUD.StopStopWatch();
        this.HUD.ReactTo(Event.Win);

        this.Terrain.Deactivate();
        this.Music.Stop();

        this.GameState.RecordTime(this.HUD.GetTime());

        await this.ToSignal(this.GetTree().CreateTimer(2), "timeout");

        this.GetTree().ChangeScene("res://scenes/menus/LevelCompleteMenu.tscn");
    }

    private async void OnCharacterKilled(Character.State state)
    {
        this.HUD.StopStopWatch();
        this.HUD.ReactTo(CharacterStateToEvent(state));

        this.Music.Stop();
        this.LostSound.Play();

        this.Terrain.Deactivate();

        await this.ToSignal(this.GetTree().CreateTimer(2.5f), "timeout");

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
