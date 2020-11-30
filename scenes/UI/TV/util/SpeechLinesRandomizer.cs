using System;
using Godot;

public class SpeechLinesRandomizer : Node
{
    private readonly SpeechLines[] Lines;

    public SpeechLinesRandomizer(SpeechLines[] lines)
    {
        if (lines.Length == 0)
        {
            throw new ArgumentException("Must provide at least one line!");
        }

        this.Lines = lines;
    }

    public SpeechLines Random()
    {
        Random random = new Random();
        var randomIndex = random.Next(0, this.Lines.Length);
        return this.Lines[randomIndex];
    }

    public override string ToString()
    {
        return String.Join(" // ", (object[]) this.Lines);
    }
}
