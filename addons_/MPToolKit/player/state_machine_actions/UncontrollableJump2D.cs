using Godot;
using MP.FiniteStateMachine;

namespace MP.Player
{
    public class UncontrollableJump2D : InstantStateAction
    {
        [Export] private float _height;
        private Player2D _player;
        private float _force;

        public override void Init(StateMachine stateMachine)
        {
            _player = stateMachine.GetCachedNode<Player2D>();
            _force = -Mathf.Sqrt(_height * _player.Gravity * 2);
        }

        public override void Act(float delta)
        {
            _player.AddForceY(_force);
        }
    }
}