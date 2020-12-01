using Godot;
using System;

public class CreditsMenu : Control
{
    private Button BackButton;
    private AnimationPlayer AnimationPlayer;

    public override void _Ready()
    {
        this.BackButton = this.GetNode<Button>("BackButton");
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");

        this.AnimationPlayer.Play("Credits");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey && !this.BackButton.HasFocus())
        {
            this.BackButton.GrabFocus();
        }
    }

    public void Back()
    {
        this.GetTree().ChangeScene("res://scenes/menus/WelcomeMenu.tscn");
    }
}
