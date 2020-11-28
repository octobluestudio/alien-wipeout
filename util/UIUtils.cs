using System;
using Godot;

public class UIUtils
{
    public static Vector2 GetViewportGlobalPosition(CanvasItem canvasItem)
    {
        return canvasItem.GetViewportTransform().AffineInverse().Xform(new Vector2(0, 0));
    }
}
