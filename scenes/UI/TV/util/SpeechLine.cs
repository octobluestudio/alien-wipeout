using System;
using Godot;

public class SpeechLine : Node
{
    public string Text { get; }
    public TV.Mood Mood { get; }

    public SpeechLine(string text, TV.Mood mood)
    {
        this.Text = text;
        this.Mood = mood;
    }
    
    public override string ToString()
    {
        return this.Text;
    }
}