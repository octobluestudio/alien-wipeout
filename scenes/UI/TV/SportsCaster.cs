using Godot;
using System;

public class Sportscaster : Node
{
    [Signal] public delegate void Transmitted(SpeechLines lines);

    private bool speaking = false;
    
    public void ReactTo(EarthWorld.Event gameEvent)
    {
        if (this.speaking)
        {
            return;
        }

        switch (gameEvent)
        {
            case EarthWorld.Event.Started:
                this.Greet();
                break;
            case EarthWorld.Event.DodgedBoulder:
                this.DodgedBoulder();
                break;
        }
    }

    private void Greet()
    {
        SpeechLine[] greetings = {
            new SpeechLine("Hello Terrans! Welcome to this new Episode of Alien Wipeout!", TV.Mood.Impressed),
            new SpeechLine("Please Welcome our new contestant: Poulpinette!", TV.Mood.Amused)
        };

        this.Say(new SpeechLines(greetings));
    }

    private void DodgedBoulder()
    {
        this.Say(SpeechLines.Simple(new SpeechLine("Oh! That was close!", TV.Mood.Impressed)));
    }

    private void Say(SpeechLines lines)
    {
        this.speaking = true;
        this.EmitSignal(nameof(Transmitted), lines);
    }
    
    private void OnSpeechMessageComplete()
    {
        this.speaking = false;
    }
}
