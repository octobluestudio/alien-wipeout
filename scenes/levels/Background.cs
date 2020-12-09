using Godot;
using System;

public class Background : ParallaxBackground
{
    public const float LevelReferenceLength = 3744;

    private ParallaxLayer Stand;

    public override void _Ready()
    {
        this.Stand = this.GetNode<ParallaxLayer>("Stand");
    }

    public void AdjustMotion(float levelLength)
    {
        this.Stand.MotionScale = new Vector2(
            this.Stand.MotionScale.x * (LevelReferenceLength / levelLength),
            this.Stand.MotionScale.y
        );
    }
}
