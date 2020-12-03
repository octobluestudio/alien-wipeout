using Godot;
using System;

public class Buzzer : StaticBody2D
{
    private void OnBuzzDetectorBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        ((Character)body).Win();
    }
}
