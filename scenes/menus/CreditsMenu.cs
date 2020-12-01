using Godot;

public class CreditsMenu : BaseMenu
{
    private AnimationPlayer AnimationPlayer;

    public override void _Ready()
    {
        this.InitButtons();

        this.AnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.AnimationPlayer.Play("Credits");
    }
}
