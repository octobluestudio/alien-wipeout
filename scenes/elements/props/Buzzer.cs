using Godot;
using System;

public class Buzzer : StaticBody2D
{
    private AudioStreamPlayer BuzzSound;
    
    public override void _Ready()
    {
        this.BuzzSound = this.GetNode<AudioStreamPlayer>("BuzzSound");
    }
    
    private void OnBuzzDetectorBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        this.BuzzSound.Play();
        ((Character)body).Win();
    }
}
