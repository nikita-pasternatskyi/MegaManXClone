using Godot;
using MP.FiniteStateMachine;
using MP.InputWrapper;
using System;

namespace MP.Player
{
    public class Movement2D : InstantStateAction
    {
        [Export(PropertyHint.Range, "0.1, 1")] private float _slowDown;
        [Export(PropertyHint.Range, "0.1, 1")] private float _speedUp;
        [Export] private float _speed;

        private Player2D _player;
        private PlayerInput2D _playerInput;

        public override void Init(StateMachine stateMachine)
        {
            _player = stateMachine.GetCachedNode<Player2D>();
            _playerInput = stateMachine.GetCachedNode<PlayerInput2D>();
        }

        public override void Act(float delta)
        {
            if (_playerInput.MovementInput.x == 0)
            {
                _player.Velocity.x = Mathf.Lerp(_player.Velocity.x, 0, _slowDown);
                return;
            }

            var motion = _playerInput.MovementInput.x;
            motion *= _speed / delta;
            var currentSpeed = Mathf.Lerp(_player.Velocity.x, motion, _speedUp);
            _player.Velocity.x = currentSpeed;
        }
    }
}