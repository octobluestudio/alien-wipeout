using Godot;
using System;

public class Sportscaster : Node
{
    [Signal] public delegate void Transmitted(SpeechLines lines);

    private Timer BlankTimer;
    private Timer InterruptionTimer;

    private bool Speaking = false;
    private bool InterruptionAllowed = false;

    public override void _Ready()
    {
        this.BlankTimer = this.GetNode<Timer>("BlankTimer");
        this.InterruptionTimer = this.GetNode<Timer>("InterruptionTimer");

        this.BlankTimer.Start();
        this.ForbidNextInterruptions();
    }

    public void ReactTo(EarthWorld.Event gameEvent)
    {
        switch (gameEvent)
        {
            case EarthWorld.Event.Started:
            case EarthWorld.Event.DodgedBoulder:
            case EarthWorld.Event.DodgedWorm:
            case EarthWorld.Event.DodgedGlove:
            case EarthWorld.Event.Punched:
                this.Say(Phrases.Random(gameEvent.ToString("G")));
                break;
            case EarthWorld.Event.Fell:
            case EarthWorld.Event.Eaten:
            case EarthWorld.Event.Smashed:
                this.ForceInterrupt(Phrases.Random(gameEvent.ToString("G")));
                break;
        }
    }
    
    public void Greet()
    {
        this.Say(Phrases.Random(Phrases.Greetings));
    }

    private void FillBlank()
    {
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
