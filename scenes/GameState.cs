﻿using System;
using Godot;
using Godot.Collections;

public class GameState : Node
{
    const string SAVE_PATH = "user://highscores.json";

    public enum Level { One, Two, Three, Four, None };

    private Dictionary<string, float> HighScores;

    private Level currentLevel = Level.One;
    public Level CurrentLevel {  get { return this.currentLevel;  } }

    public float InitialTime = 0;
    public string CheckPoint = null;

    private float duration;

    public override void _Ready()
    {
        this.HighScores = this.Load();
    }

    public void SetCurrentLevel(Level level, float initialTime)
    {
        this.currentLevel = level;
        this.duration = initialTime;
        this.InitialTime = initialTime;
    }

    public Level NextLevel()
    {
        return this.GetNextLevel(this.currentLevel);
    }

    public Level GetNextLevel(Level level)
    {
        switch (level)
        {
            case Level.One: return Level.Two;
            case Level.Two: return Level.Three;
            case Level.Three: return Level.Four;
            case Level.Four: return Level.None;
            default: return Level.One;
        }
    }

    public Level GetPreviousLevel(Level level)
    {
        switch (level)
        {
            case Level.One: return Level.None;
            case Level.Two: return Level.One;
            case Level.Three: return Level.Two;
            case Level.Four: return Level.Three;
            default: return Level.One;
        }
    }

    public void ResetCheckPoint()
    {
        this.CheckPoint = null;
        this.InitialTime = 0;
    }

    public void TrackTime(float duration)
    {
        this.duration = duration;
    }

    public void RecordTime(float duration)
    {
        this.TrackTime(duration);

        if (!this.HasHighScore(this.currentLevel) || this.duration < this.GetHighScoreFor(this.currentLevel))
        {
            this.HighScores[StringifyLevel(this.currentLevel)] = duration;
            this.Save();
        }
    }

    internal bool IsCurrentLevelRecord()
    {
        return this.duration == this.GetHighScoreFor(this.currentLevel);
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

    private void OnCheckPointValidated(string id)
    {
        this.CheckPoint = id;
    }
}
