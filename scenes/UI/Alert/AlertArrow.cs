using Godot;
using System;

public class AlertArrow : Node2D
{
    private bool Disabled = false;

    public void UpdateFollowedObjectGlobalPosition(Vector2 globalPosition)
    {
        if (this.Disabled)
        {
            return;
        }

        this.GlobalPosition = new Vector2(
            globalPosition.x,
            UIUtils.GetViewportGlobalPosition(this).y
        );
    }

    public void Disable()
    {
        this.Disabled = true;
        this.Hide();
    }
}
