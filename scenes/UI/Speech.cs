using Godot;
using System;
using System.Collections.Generic;

public class Speech : Control
{
    private const float DefaultWaitingTime = 0.5f;
    private const float DefaultInitialWaitingTime = 0.1f;

    [Export] public int LettersPerSecond = 60;

    private TextArea TextArea;
    private TV TV;
    private Timer WaitTimer;

    private Queue<string> Messages = new Queue<string>();
    
    private float WaitingTime = DefaultWaitingTime;

    public override void _Ready()
    {
        this.TextArea = this.GetNode<TextArea>("TextArea");
        this.TV = this.GetNode<TV>("TV");
        this.WaitTimer = this.GetNode<Timer>("WaitTimer");
        this.WaitTimer.WaitTime = DefaultInitialWaitingTime;

        this.TextArea.Connect(nameof(TextArea.DisplayComplete), this, "OnDisplayComplete");

        string[] greetings = {
            "Hello Terrans! Welcome to this new Episode of Alien Wipeout!",
            "Please Welcome our new contestant: Poulpinette!"
        };
        this.Say(greetings);
    }

    public void Say(string line)
    {
        this.Say(line, DefaultInitialWaitingTime);
    }

    public void Say(string line, float initialWait)
    {
        string[] text = { line };

        this.Say(text, DefaultWaitingTime, initialWait);
    }

    public void Say(string[] text)
    {
        this.Say(text, DefaultWaitingTime, DefaultInitialWaitingTime);
    }

    public void Say(string[] text, float waitBetween)
    {
        this.Say(text, waitBetween, DefaultInitialWaitingTime);
    }

    public void Say(string[] text, float waitBetween, float initialWait)
    {
        foreach(string line in text)
        {
            this.Messages.Enqueue(line);
        }

        this.WaitingTime = waitBetween;
        this.WaitTimer.Start(initialWait);
    }

    private void Display(string text)
    {
        float duration = ((float) text.Length) / ((float) this.LettersPerSecond);

        this.TextArea.Display(text, duration);
        this.TV.SpeakFor(duration + 1f);
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
            this.WaitingTime = DefaultWaitingTime;
        }
    }
    
    private void OnWaitTimerTimeout()
    {
        this.DisplayNext();
    }
}
