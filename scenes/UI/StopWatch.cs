using Godot;
using System;

public class StopWatch : Control
{
    private Label Label;

    private float timeElapsed = 0;
    private bool running = false;

    public void Init(float initialTime)
    {
        this.timeElapsed = initialTime;
        this.DisplayTime();
    }

    public void Start()
    {
        this.running = true;
        this.SetProcess(true);
    }

    public void Stop()
    {
        this.running = false;
        this.SetProcess(true);
    }

    public float TimeElapsed()
    {
        return this.timeElapsed;
    }

    public static string TimeElapsedAsString(float timeElapsed)
    {
        int minutes = (int) Math.Floor(timeElapsed / 60);
        int seconds = (int)Math.Floor(timeElapsed) % 60;

        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    public override void _Ready()
    {
        this.Label = this.GetNode<Label>("Label");
    }

    public override void _Process(float delta)
    {
        if (this.running)
        {
            this.timeElapsed += delta;
            this.DisplayTime();
        }
    }

    private void DisplayTime()
    {
        this.Label.Text = TimeElapsedAsString(this.timeElapsed);
    }
}
