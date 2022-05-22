using MP.FiniteStateMachine;
using MP.InputWrapper;
using Godot;

namespace MP.Player
{
    public class InputDifferentFromDirection : Condition
    {
        private PlayerInput2D _playerInput2D;
        private Player2D _player2D;
        public override void Init(StateMachine baseStateMachine)
        {
            _playerInput2D = baseStateMachine.GetCachedNode<PlayerInput2D>();
            _player2D = baseStateMachine.GetCachedNode<Player2D>();
        }

        protected override bool ConditionCheck()
        {
            return _player2D.Aligned(_playerInput2D.MovementInput.x) == false;
        }
    }
}