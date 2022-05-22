using MP.FiniteStateMachine;
using MP.InputWrapper;

namespace MP.Player
{
    public class FaceDirection : InstantStateAction
    {
        private PlayerInput2D _playerInput2D;
        private Player2D _player2D;

        public override void Init(StateMachine stateMachine)
        {
            _playerInput2D = stateMachine.GetCachedNode<PlayerInput2D>();
            _player2D = stateMachine.GetCachedNode<Player2D>();
        }

        public override void Act(float delta)
        {
            _player2D.FaceDirectionX(_playerInput2D.MovementInput.x);
        }
    }
}