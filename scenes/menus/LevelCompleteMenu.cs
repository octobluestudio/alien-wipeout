using Godot;

public class LevelCompleteMenu : BaseMenu
{
    private GameState GameState;

    private Label Time;

    public override void _Ready()
    {
        this.GameState = (GameState)GetNode("/root/GameState");

        this.Time = this.GetNode<Label>("Time");
        this.Time.Text = StopWatch.TimeElapsedAsString(this.GameState.GetCurrentDuration());

        this.InitButtons();

        this.DefaultFocus();
    }
}
