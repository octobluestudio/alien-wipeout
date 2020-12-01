using Godot;
using System.Collections.Generic;

public abstract class BaseMenu : Control
{
    private List<Button> Buttons = new List<Button>();

    protected Button SelectedButton;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey && !this.AnyButtonHasFocus())
        {
            this.DefaultFocus();
        }
    }

    public void InitButtons()
    {
        foreach (Node childNode in this.GetChildren())
        {
            if (childNode is Button)
            {
                this.AddButton((Button)childNode);
                this.InitButtonNavigation((Button)childNode);
            }
        }
    }

    protected void AddButton(Button button)
    {
        this.Buttons.Add(button);
    }

    protected bool AnyButtonHasFocus()
    {
        return this.SelectedButton != null;
    }

    protected void DefaultFocus()
    {
        if (this.Buttons.Count == 0)
        {
            return;
        }

        this.GrabFocusForButton(Buttons[0]);
    }

    public void StartGame()
    {
        this.GetTree().ChangeScene("res://scenes/levels/EarthWorld.tscn");
    }

    public void MainMenu()
    {
        this.GetTree().ChangeScene("res://scenes/menus/WelcomeMenu.tscn");
    }

    public void DisplayCredits()
    {
        this.GetTree().ChangeScene("res://scenes/menus/CreditsMenu.tscn");
    }

    public void QuitGame()
    {
        this.GetTree().Quit();
    }

    protected void InitButtonNavigation(Button button)
    {
        button.Connect("mouse_entered", this, "GrabFocusForButton", new Godot.Collections.Array() { button });
        button.Connect("mouse_exited", this, "ReleaseFocusForButton", new Godot.Collections.Array() { button });
        button.Connect("focus_entered", this, "ButtonFocusEntered", new Godot.Collections.Array() { button });
        button.Connect("focus_exited", this, "ButtonFocusExited", new Godot.Collections.Array() { button });
    }

    protected void GrabFocusForButton(Button button)
    {
        button.GrabFocus();
    }

    protected void ReleaseFocusForButton(Button button)
    {
        button.ReleaseFocus();
    }

    protected void ButtonFocusEntered(Button button)
    {
        this.SelectedButton = button;
    }

    protected void ButtonFocusExited(Button button)
    {
        this.SelectedButton = null;
    }
}