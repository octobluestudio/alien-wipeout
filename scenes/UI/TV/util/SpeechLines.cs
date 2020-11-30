using System;
using Godot;

public class SpeechLines : Node
{
    public const float DefaultWaitingTime = 0.5f;
    public const float DefaultInitialWaitingTime = 0.1f;

    public SpeechLine[] Lines { get; }
    public float WaitBefore { get; }
    public float WaitBetween { get; }

    public SpeechLines(SpeechLine[] lines, float waitBefore, float waitBetween)
    {
        this.Lines = lines;
        this.WaitBefore = waitBefore;
        this.WaitBetween = waitBetween;
    }

    public SpeechLines(SpeechLine[] lines)
    {
        this.Lines = lines;
        this.WaitBefore = DefaultInitialWaitingTime;
        this.WaitBetween = DefaultWaitingTime;
    }

    public static SpeechLines Simple(SpeechLine line)
    {
        SpeechLine[] lines = { line };
        return new SpeechLines(lines);
    }

    public override string ToString()
    {
        return String.Join(" // ", (object[]) this.Lines);
    }
}
