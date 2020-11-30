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

        this.StopWatch.Start();
    }

    public void StopStopWatch()
    {
        this.StopWatch.Stop();
    }
    
    private void OnGameEvent(EarthWorld.Event gameEvent)
    {
        this.Sportscaster.ReactTo(gameEvent);
    }
}

