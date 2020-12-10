
using Godot;

public class GameOverMenu : BaseMenu
{
    public override void _Ready()
    {
        this.Init();
    }

    public new void Init()
    {
        this.InitMouse();

        if (this.GameState.CheckPoint == null)
        {
            this.GetNode<Button>("CheckPointButton").QueueFree();
            this.GetNode<TextureRect>("BackToCheckPointButtonBackground").QueueFree();
        }
        else
        {
            this.AddButton(this.GetNode<Button>("CheckPointButton"));
        }

        this.AddButton(this.GetNode<Button>("TryAgainButton"));
        this.AddButton(this.GetNode<Button>("MainMenuButton"));
        this.AddButton(this.GetNode<Button>("QuitButton"));
    }
}
