using Godot;

public class PlayerInput : Node
{
    public Vector2 MovementInput => _movementInput;

    private Vector2 _movementInput;

    public override void _Process(float delta)
    {
        _movementInput.y = Input.GetActionStrength(InputBindings.MOVE_UP) - Input.GetActionStrength(InputBindings.MOVE_DOWN);
        _movementInput.x = Input.GetActionStrength(InputBindings.MOVE_RIGHT) - Input.GetActionStrength(InputBindings.MOVE_LEFT);
        _movementInput = MovementInput.Normalized();
    }
}
