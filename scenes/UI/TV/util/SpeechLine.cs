﻿using Godot;

public class SpeechLine : Object
{
    public const float DefaultCompleteDisplayDuration = 3f;

    public string Text { get; }
    public TV.Mood Mood { get; }
    public float CompleteDisplayDuration { get; }

    public SpeechLine(string text, TV.Mood mood, float completeDisplayDuration)
    {
        this.Text = text;
        this.Mood = mood;
        this.CompleteDisplayDuration = completeDisplayDuration;
    }

    public SpeechLine(string text, TV.Mood mood) : this(text, mood, DefaultCompleteDisplayDuration)
    {
    }
    
    public override string ToString()
    {
        return this.Text;
    }
}