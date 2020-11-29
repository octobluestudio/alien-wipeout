using Godot;
using System;

public class EarthWorld : Node2D
{
    private HUD HUD;
    private ImpactLocator ImpactLocator;

    public override void _Ready()
    {
        this.HUD = this.GetNode<HUD>("HUD");
        this.ImpactLocator = this.GetNode<ImpactLocator>("ImpactLocator");
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
