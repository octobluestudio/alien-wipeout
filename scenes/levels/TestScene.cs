using Godot;
using System;

public class TestScene : Node2D
{
    public override void _Ready()
    {
        this.GetNode<Character>("Character").WakeUp();
    }

    private void _on_Character_Punched()
    {
        GD.Print("punched");
    }
    
    private void _on_Character_Dodged(EnemyProperties.Type enemyType)
    {
        GD.Print("dodged " + enemyType);
    }
}
