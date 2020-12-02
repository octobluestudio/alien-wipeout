using System;

public class SpeechLines : Godot.Object
{
    public const float DefaultInitialWaitingTime = 0.1f;
    public const float DefaultWaitingTime = 0.5f;

    public SpeechLine[] Lines { get; }
    public float WaitBefore { get; }
    public float WaitBetween { get; }
    public bool Interruption { get; }

    public SpeechLines(SpeechLine[] lines, float waitBefore, float waitBetween, bool interruption)
    {
        this.Lines = lines;
        this.WaitBefore = waitBefore;
        this.WaitBetween = waitBetween;
        this.Interruption = interruption;
    }

    public SpeechLines(SpeechLine[] lines, bool interruption) : this(lines, DefaultInitialWaitingTime, DefaultWaitingTime, interruption)
    {
    }

    public SpeechLines(SpeechLine[] lines) : this(lines, false)
    {
    }

    public static SpeechLines Simple(SpeechLine line)
    {
        SpeechLine[] lines = { line };
        return new SpeechLines(lines);
    }

    public static SpeechLines Interrupt(SpeechLine line)
    {
        SpeechLine[] lines = { line };
        return new SpeechLines(lines, true);
    }

    public override string ToString()
    {
        return String.Join(" // ", (object[]) this.Lines);
    }
}
