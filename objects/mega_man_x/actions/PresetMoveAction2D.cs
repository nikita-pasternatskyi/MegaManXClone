using Godot;

public class PresetMoveAction2D : MoveAction2D
{
    [Export] private Vector2 _moveInput;
    [Export] private bool _relative;

    protected override Vector2 GetInput()
    {
        if (_relative == true)
        {
            _moveInput*=KinematicBody.FacingDirection;
        }
        return _moveInput;
    }
}

