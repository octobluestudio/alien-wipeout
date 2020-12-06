using Godot;
using System;

public class Sportscaster : Node
{
    [Signal] public delegate void Transmitted(SpeechLines lines);

    private Timer BlankTimer;
    private Timer InterruptionTimer;

    private bool Speaking = false;
    private bool InterruptionAllowed = false;

    private bool CanFeelBlank = true;

    public override void _Ready()
    {
        this.BlankTimer = this.GetNode<Timer>("BlankTimer");
        this.InterruptionTimer = this.GetNode<Timer>("InterruptionTimer");

        this.BlankTimer.Start();
        this.ForbidNextInterruptions();
    }

    public void ReactTo(Level.Event gameEvent)
    {
        switch (gameEvent)
        {
            case Level.Event.Greetings:
            case Level.Event.DodgedBoulder:
            case Level.Event.DodgedWorm:
            case Level.Event.DodgedGlove:
            case Level.Event.Punched:
                this.Say(Phrases.Random(gameEvent.ToString("G")));
                break;
            case Level.Event.Started:
                this.ForceInterrupt(Phrases.Random(gameEvent.ToString("G")));
                break;
            case Level.Event.Fell:
            case Level.Event.Eaten:
            case Level.Event.Smashed:
            case Level.Event.Win:
                this.ForceInterrupt(Phrases.Random(gameEvent.ToString("G")));
                this.CanFeelBlank = false;
                break;
        }
    }

    private void FillBlank()
    {
        if (!this.CanFeelBlank)
        {
            return;
        }

        this.Say(Phrases.Random(Phrases.FillBlank));
    }

    private void Say(SpeechLines lines)
    {
        if (this.Speaking && !(lines.Interruption && this.CanInterrupt()))
        {
            // if they're already speaking and this is not an allowed interruption
            return;
        }

        if (this.Speaking && lines.Interruption)
        {
            // if this is an interruption
            ForbidNextInterruptions();
        }

        this.Speaking = true;
        this.BlankTimer.Stop();

        this.EmitSignal(nameof(Transmitted), lines);
    }

    private void ForceInterrupt(SpeechLines lines)
    {
        this.InterruptionAllowed = true;
        this.InterruptionTimer.Stop();

        this.Say(lines);
    }

    private void ForbidNextInterruptions()
    {
        this.InterruptionAllowed = false;
        this.InterruptionTimer.Start();
    }

    private bool CanInterrupt()
    {
        return this.InterruptionAllowed;
    }

    private void OnSpeechMessageComplete()
    {
        this.Speaking = false;

        this.BlankTimer.Start();
    }

    private void OnBlankTimerTimeout()
    {
        this.FillBlank();
    }

    private void OnInterruptionTimerTimeout()
    {
        this.InterruptionAllowed = true;
    }
}
