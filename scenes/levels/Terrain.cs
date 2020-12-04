using Godot;

public class Terrain : Node2D
{
    [Signal] public delegate void BoulderGenerated(Boulder boulder);
    [Signal] public delegate void PresentationStarted();
    [Signal] public delegate void PresentationEnded();

    private BoulderGenerator BoulderGenerator;
    private AnimationPlayer CameraMovement;
    private Path2D CameraPath;

    private bool IsPresenting = false;
    
    public override void _Ready()
    {
        this.BoulderGenerator = this.GetNode<BoulderGenerator>("BoulderGenerator");
        this.CameraMovement = this.GetNode<AnimationPlayer>("CameraMovement");
        this.CameraPath = this.GetNode<Path2D>("CameraPath");
    }

    public override void _Input(InputEvent @event)
    {
        if (!this.IsPresenting)
        {
            return;
        }

        if (@event.IsAction("ui_cancel") || @event.IsAction("ui_accept"))
        {
            this.EndPresentation();
        }
    }

    public void Present()
    {
        this.IsPresenting = true;
        this.CameraMovement.Play("Presentation");
        this.EmitSignal(nameof(PresentationStarted));
    }

    public void EndPresentation()
    {
        this.IsPresenting = false;
        this.CameraMovement.Stop();
        this.CameraMovement.QueueFree();
        this.CameraPath.QueueFree();
        this.EmitSignal(nameof(PresentationEnded));
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
