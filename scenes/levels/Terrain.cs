using Godot;

public class Terrain : Node2D
{
    [Signal] public delegate void BoulderGenerated(Boulder boulder);

    private BoulderGenerator BoulderGenerator;
    
    public override void _Ready()
    {
        this.BoulderGenerator = this.GetNode<BoulderGenerator>("BoulderGenerator");

        this.Activate();
    }

    public void Activate()
    {
        this.BoulderGenerator.Start();
    }

    public void Deactivate()
    {
        this.BoulderGenerator.Stop();
    }

    private void OnBoulderGenerated(Boulder boulder)
    {
        this.EmitSignal(nameof(BoulderGenerated), boulder);
    }
}
