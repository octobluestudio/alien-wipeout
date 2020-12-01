using Godot;
using System.Collections.Generic;

public class Speech : Control
{
    [Export] public int LettersPerSecond = 60;

    [Signal] public delegate void MessageComplete();

    private TextArea TextArea;
    private TV TV;
    private Timer WaitTimer;

    private Queue<SpeechLine> Messages = new Queue<SpeechLine>();

    private float WaitingTime = SpeechLines.DefaultWaitingTime;

    public override void _Ready()
    {
        this.TextArea = this.GetNode<TextArea>("TextArea");

        this.TV = this.GetNode<TV>("TV");

        this.WaitTimer = this.GetNode<Timer>("WaitTimer");
        this.WaitTimer.WaitTime = SpeechLines.DefaultInitialWaitingTime;
    }

    public void Interrupt()
    {
        this.Messages.Clear();
        this.WaitTimer.Stop();
        this.TV.Idle();
        this.TextArea.Erase();
    }

    public void Say(SpeechLines lines)
    {
        if (lines.Interruption)
        {
            this.Interrupt();
        }

        foreach(SpeechLine line in lines.Lines)
        {
            this.Messages.Enqueue(line);
        }

        this.WaitingTime = lines.WaitBetween;
        this.WaitTimer.Start(lines.WaitBefore);
    }

    private void Display(SpeechLine line)
    {
        float duration = ((float) line.Text.Length) / ((float) this.LettersPerSecond);

        this.TextArea.Display(line.Text, duration, line.CompleteDisplayDuration);
        this.TV.ChangeMood(line.Mood);
        this.TV.SpeakFor(duration + (line.CompleteDisplayDuration / 2));
    }

    private void DisplayNext()
    {
        this.Display(this.Messages.Dequeue());
    }

    private void OnDisplayComplete()
    {
        if (this.Messages.Count > 0)
        {
            this.WaitTimer.Start(this.WaitingTime);
        } else
        {
            this.EmitSignal(nameof(MessageComplete));
        }
    }
    
    private void OnWaitTimerTimeout()
    {
        this.DisplayNext();
    }

    private void OnSportscasterTransmitted(SpeechLines lines)
    {
        this.Say(lines);
    }
}
