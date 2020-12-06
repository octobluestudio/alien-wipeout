using Godot;
using System;

public class BigBall : StaticBody2D, Bouncer
{
    public float BounceForce()
    {
        return 1.2f;
    }
}
