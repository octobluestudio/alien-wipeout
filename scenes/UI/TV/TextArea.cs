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

        this.Erase(false);
    }

    public void Display(string text, float duration, float waitAfterDuration)
    {
        this.Label.Text = text;
        this.AnimationPlayer.PlaybackSpeed = 1 / duration;
        this.AnimationPlayer.Play("TypeWriter");
        this.CompleteDisplayTimer.WaitTime = waitAfterDuration;
    }

    public void DisplayTermination()
    {
        this.CompleteDisplayTimer.Start();
    }

    public void Erase()
    {
        this.Erase(true);
    }

    private void Erase(bool notify)
    {
        this.CompleteDisplayTimer.Stop();
        this.CompleteDisplayTimer.WaitTime = SpeechLine.DefaultCompleteDisplayDuration;

        this.Label.Text = "";
        this.Label.PercentVisible = 0;
        this.AnimationPlayer.Stop();

        if (notify)
        {
            this.EmitSignal(nameof(DisplayComplete));
        }
    }
    
    private void OnCompleteDisplayTimerTimeout()
    {
        this.Erase();
    }
}
