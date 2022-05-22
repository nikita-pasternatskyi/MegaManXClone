using Godot;

namespace MP.InputWrapper
{
    public class PlayerInput2D : Node
    {
        public Vector2 MovementInput => _movementInput;
        protected Vector2 _movementInput;

        public override void _Process(float delta)
        {
            _movementInput.x = Input.GetActionStrength(InputBindings.RIGHT) - Input.GetActionStrength(InputBindings.LEFT);
            _movementInput.y = Input.GetActionStrength(InputBindings.UP) - Input.GetActionStrength(InputBindings.DOWN);

            _movementInput = _movementInput.Normalized();
            OnProcess();
        }

        protected virtual void OnProcess()
        {

        }
    }
}
