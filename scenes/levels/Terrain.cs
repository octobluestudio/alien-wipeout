using Godot;

public class Terrain : Node2D
{
    [Signal] public delegate void BoulderGenerated(Boulder boulder);
    [Signal] public delegate void PresentationStarted();
    [Signal] public delegate void PresentationEnded();

    private BoulderGenerator BoulderGenerator;
    private ConfigurableCamera Camera;
    private AnimationPlayer CameraMovement;
    private Path2D CameraPath;
    private Position2D StartPosition;

    private bool IsPresenting = false;

    public Vector2 StartPointGlobalPosition { get { return this.StartPosition.GlobalPosition; } }
    
    public override void _Ready()
    {
        this.BoulderGenerator = this.GetNode<BoulderGenerator>("BoulderGenerator");
        this.Camera = this.GetNode<ConfigurableCamera>("Camera");
        this.CameraMovement = this.GetNode<AnimationPlayer>("CameraMovement");
        this.CameraPath = this.GetNode<Path2D>("CameraPath");
        this.StartPosition = this.GetNode<Position2D>("StartPosition");

        var cameraZones = this.GetTree().GetNodesInGroup("CameraZone");
        foreach(CameraZone zone in cameraZones)
        {
            zone.Connect(nameof(CameraZone.CameraZoneModified), this, "OnCameraZoneModified");
            zone.Connect(nameof(CameraZone.CameraZoneRestored), this, "OnCameraZoneRestored");
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (!this.IsPresenting)
        {
            return;
        }

        if (@event.IsAction("ui_accept"))
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

    private void OnCameraZoneModified(int newTop, int newBottom)
    {
        this.Camera.ChangeTopBottomLimits(newTop, newBottom);
    }

    private void OnCameraZoneRestored()
    {
        this.Camera.RestoreTopBottomLimits();
    }
}
