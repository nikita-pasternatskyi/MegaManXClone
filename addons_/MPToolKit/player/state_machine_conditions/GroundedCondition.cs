using MP.FiniteStateMachine;

namespace MP.Player
{
    public class GroundedCondition : Condition
    {
        private Player2D _player2D;

        public override void Init(StateMachine baseStateMachine)
        {
            _player2D = baseStateMachine.GetCachedNode<Player2D>();
        }

        protected override bool ConditionCheck()
        {
            return _player2D.Grounded;
        }
    }
}