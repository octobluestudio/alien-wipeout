using Godot;
using System;

public class SpaceWorm : Area2D
{
    private AnimationPlayer AnimationPlayer;
    private DodgeDetector DodgeDetector;
    private Timer WaitTimer;

    public EnemyProperties.Type Type = EnemyProperties.Type.SpaceWorm;

    public override void _Ready()
    {
        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.DodgeDetector = this.GetNode<DodgeDetector>("DodgeDetector");
        this.WaitTimer = this.GetNode<Timer>("WaitTimer");
    }

    public void Attack()
    {
        if (!this.AnimationPlayer.IsPlaying() && this.WaitTimer.TimeLeft == 0)
        {
            this.WaitTimer.Start();
        }
    }
    
    private void OnCharacterDodged(Character character)
    {
        character.Dodge(this.Type);
    }
    
    private void OnSpaceWormBodyEntered(PhysicsBody2D body)
    {
        if (!(body is Character))
        {
            return;
        }

        this.DodgeDetector.Cancel();
        ((Character)body).Chomp();
    }

    private void OnWaitTimertimeout()
    {
        this.AnimationPlayer.Play("Attack");
    }
}




