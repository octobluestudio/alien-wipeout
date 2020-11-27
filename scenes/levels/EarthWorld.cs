using Godot;
using System;

public class EarthWorld : Node2D
{
    private void OnCharacterKilled()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");
    }
}
