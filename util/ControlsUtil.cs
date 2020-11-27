using System;
using Godot;

public class ControlsUtil
{
    private const string MoveLeft = "ui_player1_move_left";
    private const string MoveRight = "ui_player1_move_right";
    private const string MoveUp = "ui_player1_move_up";
    private const string MoveDown = "ui_player1_move_down";
    private const string Jump = "ui_player1_jump";

    /// <summary>
    /// Returns the normalized vector corresponding to the input
    /// </summary>
    /// <returns>The raw direction vector from input</returns>
    public static Vector2 DirectionFromInput()
    {
        return (new Vector2(
            Input.GetActionStrength(MoveRight) - Input.GetActionStrength(MoveLeft),
            0
        )).Normalized();
    }

    public static bool IsJumpJustPressed()
    {
        return Input.IsActionJustPressed(Jump);
    }

    public static void ReleaseAll()
    {
        Input.ActionRelease(MoveLeft);
        Input.ActionRelease(MoveRight);
        Input.ActionRelease(MoveUp);
        Input.ActionRelease(MoveDown);
        Input.ActionRelease(Jump);
    }
}
