using Godot;
using System;

public class HUD : CanvasLayer
{
    private StopWatch StopWatch;

    public override void _Ready()
    {
        this.StopWatch = this.GetNode<StopWatch>("StopWatch");

        this.StopWatch.Start();
    }

    public void StopStopWatch()
    {
        this.StopWatch.Stop();
    }
}
