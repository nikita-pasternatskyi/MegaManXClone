using Godot;
using MP.StateMachine;

public class PlayerInputMoveAction2D : MoveAction2D
{
    private PlayerInput _playerInput;

    protected override void OnInit(BaseStateMachine baseStateMachine)
    {
        _playerInput = baseStateMachine.GetNodeOfType<PlayerInput>();
    }

    protected override Vector2 GetInput()
    {
        var input = _playerInput.MovementInput;
        input.y = 0;
        return input;
    }
}

