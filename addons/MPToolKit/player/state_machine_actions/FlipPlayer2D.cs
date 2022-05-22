using MP.FiniteStateMachine;

namespace MP.Player
{
    public class FlipPlayer2D:InstantStateAction
    {
        private Player2D _player2D;

        public override void Init(StateMachine stateMachine)
        {
            _player2D = stateMachine.GetCachedNode<Player2D>();
        }

        public override void Act(float delta)
        {
            _player2D.Flip();
        }
    }
}