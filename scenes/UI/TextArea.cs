using Godot;
using System;

public class TextArea : Control
{
    private AnimationPlayer AnimationPlayer;
    private Label Label;
    private Timer CompleteDisplayTimer;

    [Signal] public delegate void DisplayComplete();

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.Label = this.GetNode<Label>("Label");
        this.CompleteDisplayTimer = this.GetNode<Timer>("CompleteDisplayTimer");

        this.Label.PercentVisible = 0;
    }

    public void Display(string text, float duration)
    {
        this.Label.Text = text;
        this.AnimationPlayer.PlaybackSpeed = duration;
        this.AnimationPlayer.Play("TypeWriter");
    }

    private void Erase()
    {
        this.Label.Text = "";
        this.Label.PercentVisible = 0;
        this.EmitSignal(nameof(DisplayComplete));
    }

    public void DisplayTermination()
    {
        this.CompleteDisplayTimer.Start();
    }

    private void OnCompleteDisplayTimerTimeout()
    {
        this.Erase();
    }
}
