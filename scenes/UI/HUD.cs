using Godot;
using System;

public class HUD : CanvasLayer
{
    private Label SkipMessage;
    private Speech Speech;
    private Sportscaster Sportscaster;
    private StopWatch StopWatch;

    public override void _Ready()
    {
        this.SkipMessage = this.GetNode<Label>("SkipMessage");
        this.Speech = this.GetNode<Speech>("Speech");
        this.Sportscaster = this.GetNode<Sportscaster>("Sportscaster");
        this.StopWatch = this.GetNode<StopWatch>("StopWatch");

        this.HideSkipMessage();
    }

    public void DisplaySkipMessage()
    {
        this.SkipMessage.Visible = true;
    }

    public void HideSkipMessage()
    {
        this.SkipMessage.Visible = false;
    }

    public void StartStopWatch()
    {
        this.StopWatch.Start();
    }

    public void StopStopWatch()
    {
        this.StopWatch.Stop();
    }

    public float GetTime()
    {
        return this.StopWatch.TimeElapsed();
    }
    
    public void ReactTo(BaseLevel.Event gameEvent)
    {
        this.Sportscaster.ReactTo(gameEvent);
    }
}

