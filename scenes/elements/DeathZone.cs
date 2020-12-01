using Godot;
using System;

public class DeathZone : Area2D
{
    private void OnDeathZoneBodyEntered(PhysicsBody2D body)
    {
        ((Character)body).Fell();
    }
}
