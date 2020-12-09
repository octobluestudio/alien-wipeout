using Godot;
using System;

public class DeathZone : Area2D
{
    private void OnDeathZoneBodyEntered(PhysicsBody2D body)
    {
        if (body is Character)
        {
            ((Character)body).Fell();
        }

        if (body is Boulder)
        {
            ((Boulder)body).Explode();
        }
        
    }
}
