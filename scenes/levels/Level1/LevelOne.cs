
public class LevelOne : BaseLevel
{
    public override void _Ready()
    {
        this.InitNodes(GameState.Level.One);

        this.Start();
    }
}
