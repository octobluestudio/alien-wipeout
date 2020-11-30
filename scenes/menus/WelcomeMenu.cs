using Godot;
using System;

public class WelcomeMenu : Control
{
    private void StartGame()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");
    }

    private void OnStartButtonUp()
    {
        StartGame();
    }
}
