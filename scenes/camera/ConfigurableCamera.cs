using Godot;

public class ConfigurableCamera : Camera2D
{
    private const float SmoothingMovementWeight = 0.3f;

    private int OriginalTopLimit;
    private int OriginalBottomLimit;

    private int TopLimitTarget;
    private int BottomLimitTarget;
    
    public override void _Ready()
    {
        this.OriginalTopLimit = this.LimitTop;
        this.OriginalBottomLimit = this.LimitBottom;

        this.TopLimitTarget = this.OriginalTopLimit;
        this.BottomLimitTarget = this.OriginalBottomLimit;
    }

    public override void _Process(float delta)
    {
        // smooth the limit changes process

        if (this.LimitTop != this.TopLimitTarget)
        {
            this.LimitTop = LinearApproach(this.LimitTop, this.TopLimitTarget);
        }

        if (this.LimitBottom != this.BottomLimitTarget)
        {
            this.LimitBottom = LinearApproach(this.LimitBottom, this.BottomLimitTarget);
        }
    }

    private static int LinearApproach(int from, int to)
    {
        var value = Mathf.RoundToInt(Mathf.Lerp(from, to, SmoothingMovementWeight));

        // if the linear interpolation didn't change the from value we return the to value, otherwise, the calculated value
        return (value != from) ? value : to;
    }

    public void ChangeTopBottomLimits(int top, int bottom)
    {
        this.TopLimitTarget = top;
        this.BottomLimitTarget = bottom;
    }

    public void RestoreTopBottomLimits()
    {
        this.TopLimitTarget = this.OriginalTopLimit;
        this.BottomLimitTarget = this.OriginalBottomLimit;
    }
}
