using Godot;
using System;

public class BoxingGlove : Area2D
{
    [Export] private Vector2 Direction = Vector2.Left;
    [Export] private float Strength = 450f;
    [Export] private float Speed = 300f;

    public override void _PhysicsProcess(float delta)
    {
        this.Position -= new Vector2(this.Speed * delta, 0);
    }

    private void Destroy()
    {
        this.QueueFree();
    }

    private void OnBoxingGloveBodyEntered(PhysicsBody2D body)
    {
        if (body is Character) {
            ((Character)body).BounceBack(this.Direction + new Vector2(0, -0.5f), this.Strength);
        }

        this.Destroy();
    }

    private void OnscreenExited()
    {
        this.QueueFree();
    }
}
