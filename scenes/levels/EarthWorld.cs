using Godot;
using System;

public class EarthWorld : Node2D
{
    private HUD HUD;

    public override void _Ready()
    {
        this.HUD = this.GetNode<HUD>("HUD");
    }

    private void OnCharacterKilled()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");

        this.HUD.StopStopWatch();
    }
}
