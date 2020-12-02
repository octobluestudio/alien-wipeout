using Godot;

public class DodgeDetector : Node2D
{
    [Signal] public delegate void CharacterDodged(Character character);

    private bool Active = true;

    public void Cancel()
    {
        this.Active = false;
    }

    private void OnBodyExited(PhysicsBody2D body)
    {
        if (!(body is Character) || !this.Active)
        {
            return;
        }

        this.EmitSignal(nameof(CharacterDodged), (Character) body);
    }
}
