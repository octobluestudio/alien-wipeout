using Godot;
using System;

public class LevelCompleteMenu : BaseMenu
{
    public override void _Ready()
    {
        this.InitButtons();

        this.DefaultFocus();
    }
}
