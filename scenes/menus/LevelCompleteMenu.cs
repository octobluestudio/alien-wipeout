using Godot;

public class LevelCompleteMenu : BaseMenu
{
    private Label Time;

    public override void _Ready()
    {
        this.Time = this.GetNode<Label>("Time");
        this.Time.Text = StopWatch.TimeElapsedAsString(this.GameState.GetCurrentDuration());

        this.GetNode<TextureRect>("NewHighScoreLabel").Visible = this.GameState.IsCurrentLevelRecord();

        this.Init();

        this.DefaultFocus();
    }
}
