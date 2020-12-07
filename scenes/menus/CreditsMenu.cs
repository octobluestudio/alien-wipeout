using Godot;

public class CreditsMenu : BaseMenu
{
    private AnimationPlayer AnimationPlayer;

    public override void _Ready()
    {
        this.Init();
        this.DefaultFocus();

        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.AnimationPlayer.Play("Credits");
    }
}
