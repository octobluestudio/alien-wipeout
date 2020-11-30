using Godot;
using System;

public class WelcomeMenu : Control
{
    private Button StartButton;
    private Button QuitButton;

    private Button SelectedButton;

    public override void _Ready()
    {
        this.StartButton = this.GetNode<Button>("StartButton");
        this.SetButtonNavigation(this.StartButton);

        this.QuitButton = this.GetNode<Button>("QuitButton");
        this.SetButtonNavigation(this.QuitButton);

        this.DefaultFocus();
    }

    public override void _Process(float delta)
    {
        if (!AnyButtonHasFocus())
        {
            this.DefaultFocus();
        }
    }

    private bool AnyButtonHasFocus()
    {
        return this.StartButton.HasFocus() || this.QuitButton.HasFocus();
    }

    private void DefaultFocus()
    {
        this.StartButton.GrabFocus();
    }

    private void StartGame()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");
    }

    private void QuitGame()
    {
        this.GetTree().Quit();
    }

    private void SetButtonNavigation(Button button)
    {
        button.Connect("mouse_entered", this, "GrabFocusForButton", new Godot.Collections.Array() { button });
        button.Connect("mouse_exited", this, "ReleaseFocusForButton", new Godot.Collections.Array() { button });
    }
    
    private void OnStartButtonUp()
    {
        this.StartGame();
    }

    private void OnQuitButtonUp()
    {
        this.QuitGame();
    }

    private void OnButtonFocusEntered(Button button)
    {
        this.SelectedButton = button;
    }

    private void GrabFocusForButton(Button button)
    {
        button.GrabFocus();
    }

    private void ReleaseFocusForButton(Button button)
    {
        button.ReleaseFocus();
    }
}

