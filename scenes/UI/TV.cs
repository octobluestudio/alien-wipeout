using Godot;
using System;

public class TV : Control
{
    private AnimationPlayer AnimationPlayer;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void SpeakFor(float seconds)
    {

    }
}
