using Godot;
using System.Collections.Generic;

public abstract class BaseMenu : Control
{
    private List<Button> Buttons = new List<Button>();

    protected Button SelectedButton;

    private GameState gameState;
    protected GameState GameState { get {
            if (this.gameState == null) {
                this.gameState = (GameState)GetNode("/root/GameState");
            }

            return this.gameState;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey && !this.AnyButtonHasFocus())
        {
            this.DefaultFocus();
        }
    }

    public void InitButtons()
    {
        ControlsUtil.ShowMouse();

        foreach (Node childNode in this.GetChildren())
        {
            if (childNode is Button)
            {
                this.AddButton((Button)childNode);
            }
        }
    }

    protected void AddButton(Button button)
    {
        this.Buttons.Add(button);
        this.InitButtonNavigation(button);
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
        this.StartLevel(this.GameState.CurrentLevel);
    }

    public void LevelOne()
    {
        this.StartLevel(GameState.Level.One);
    }

    public void LevelTwo()
    {
        this.StartLevel(GameState.Level.Two);
    }

    public void LevelThree()
    {
        this.StartLevel(GameState.Level.Three);
    }

    public void LevelFour()
    {
        this.StartLevel(GameState.Level.Four);
    }

    public void NextLevel()
    {
        this.StartLevel(GameState.NextLevel());
    }

    private void StartLevel(GameState.Level level)
    {
        if (level == GameState.Level.None)
        {
            return; // TODO create End
        }

        this.GameState.SetCurrentLevel(level);
        this.GetTree().ChangeScene("res://scenes/levels/BaseLevel.tscn");
    }

    public void MainMenu()
    {
        this.GetTree().ChangeScene("res://scenes/menus/WelcomeMenu.tscn");
    }

    public void DisplayCredits()
    {
        this.GetTree().ChangeScene("res://scenes/menus/CreditsMenu.tscn");
    }

    public void HighScores()
    {
        this.GetTree().ChangeScene("res://scenes/menus/HighScoresMenu.tscn");
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
