using Godot;
using System;

public class Buzzer : StaticBody2D
{
    private AudioStreamPlayer BuzzSound;
    private Particles2D Confetti;

    public override void _Ready()
    {
        this.BuzzSound = this.GetNode<AudioStreamPlayer>("BuzzSound");
        this.Confetti = this.GetNode<Particles2D>("Confetti");
    }
    
    private void OnBuzzDetectorBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        this.BuzzSound.Play();
        this.Confetti.Emitting = true;
        ((Character)body).Win();
    }
}
