using Godot;
using System;

public class TV : Control
{
    private AnimationPlayer AnimationPlayer;
    private Timer Timer;

    public enum Mood{ Neutral, Amused, Angry, Impressed };

    private Mood CurrentMood = Mood.Neutral;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.Timer = this.GetNode<Timer>("Timer");

        this.Idle();
    }

    public void ChangeMood(Mood mood)
    {
        this.CurrentMood = mood;
    }

    public void SpeakFor(float seconds)
    {
        this.AnimationPlayer.Play("Talk" + this.CurrentMood.ToString("G"));

        this.Timer.Start(seconds);
    }

    private void Idle()
    {
        this.AnimationPlayer.Play(this.CurrentMood.ToString("G"));
    }

    private void OnTimerTimeout()
    {
        this.Idle();
    }
}
