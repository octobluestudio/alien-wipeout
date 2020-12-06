using Godot;

public class HighScoresMenu : BaseMenu
{
    public override void _Ready()
    {
        this.AddButton(this.GetNode<Button>("Level1/Button"));
        this.AddButton(this.GetNode<Button>("Level2/Button"));
        this.AddButton(this.GetNode<Button>("Level3/Button"));
        this.AddButton(this.GetNode<Button>("Level4/Button"));

        this.InitButtons();
        this.DefaultFocus();

        this.InitLevel("Level1", GameState.Level.One);
        this.InitLevel("Level2", GameState.Level.Two);
        this.InitLevel("Level3", GameState.Level.Three);
        this.InitLevel("Level4", GameState.Level.Four);
    }

    private void InitLevel(string levelNodeGroupName, GameState.Level level)
    {
        var highScore = this.GameState.GetHighScoreFor(level);
        var previousLevelHighScore = this.GameState.GetHighScoreFor(this.GameState.GetPreviousLevel(level));

        if (level != GameState.Level.One)
        {
            this.GetNode<Button>(levelNodeGroupName + "/Button").Disabled = (previousLevelHighScore == 0);
        }
        
        this.GetNode<Label>(levelNodeGroupName+"/Score").Text = StopWatch.TimeElapsedAsString(highScore);
    }
}
