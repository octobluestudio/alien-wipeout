using System;
using Godot;
using Godot.Collections;

public class GameState : Node
{
    const string SAVE_PATH = "user://highscores.json";

    public enum Level { One, Two, Three, Four, End };

    private Dictionary<string, float> HighScores;

    private Level currentLevel = Level.One;
    public Level CurrentLevel {  get { return this.currentLevel;  } }

    private float duration;

    public override void _Ready()
    {
        this.HighScores = this.Load();
    }

    public void SetCurrentLevel(Level level)
    {
        this.currentLevel = level;
        this.duration = 0;
    }

    public Level NextLevel()
    {
        switch (this.currentLevel)
        {
            case Level.One: return Level.Two;
            case Level.Two: return Level.Three;
            case Level.Three: return Level.Four;
            case Level.Four: return Level.End;
            default: return Level.One;
        }
    }

    public void RecordTime(float duration)
    {
        this.duration = duration;

        if (!this.HasHighScore(this.currentLevel) || this.duration < this.GetHighScoreFor(this.currentLevel))
        {
            this.HighScores[StringifyLevel(this.currentLevel)] = duration;
            this.Save();
        }
    }

    public float GetCurrentDuration()
    {
        return this.duration;
    }

    public float GetHighScoreFor(Level level)
    {
        if (!this.HasHighScore(level))
        {
            return 0;
        }

        return this.HighScores[StringifyLevel(level)];
    }

    private bool HasHighScore(Level level)
    {
        return this.HighScores.ContainsKey(StringifyLevel(level));
    }

    private void Save()
    {
        var saveFile = new File();
        saveFile.Open(SAVE_PATH, File.ModeFlags.Write);
        saveFile.StoreLine(JSON.Print(HighScores));
        saveFile.Close();
    }

    private Dictionary<string, float> Load()
    {
        var saveFile = new File();
        if (!saveFile.FileExists(SAVE_PATH))
            return new Dictionary<string, float>();

        saveFile.Open(SAVE_PATH, File.ModeFlags.Read);
        string fileContent = saveFile.GetAsText();
        saveFile.Close();

        return new Dictionary<string, float>((Dictionary)JSON.Parse(fileContent).Result);
    }

    private static string StringifyLevel(Level level)
    {
        return level.ToString("G");
    }
}
