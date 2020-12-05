using Godot;
using System;

public class HUD : CanvasLayer
{
    private Speech Speech;
    private Sportscaster Sportscaster;
    private StopWatch StopWatch;

    public override void _Ready()
    {
        this.Speech = this.GetNode<Speech>("Speech");
        this.Sportscaster = this.GetNode<Sportscaster>("Sportscaster");
        this.StopWatch = this.GetNode<StopWatch>("StopWatch");
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
    
    public void ReactTo(Level.Event gameEvent)
    {
        this.Sportscaster.ReactTo(gameEvent);
    }
}

