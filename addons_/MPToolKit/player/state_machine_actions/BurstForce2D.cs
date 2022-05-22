using Godot;
using MP.Extensions;
using MP.FiniteStateMachine;
using MP.Player;
using System;

namespace MP.Player
{
    public class BurstForce2D : StateAction
    {
        [ExportFlagsEnum(typeof(Vector2Axis))] private int _axis;
        [Export] private float _time;
        [Export] private Curve _speedCurve;
        [Export] private Vector2 _force;
        [Export] private bool _applyRelativeToDirection;

        private Player2D _player;
        private Action<Vector2> SetVelocity;
        private float _currentTime;

        public override void Init(StateMachine stateMachine)
        {
            _player = stateMachine.GetCachedNode<Player2D>();

            var axisToApply = (Vector2Axis)_axis;

            switch (axisToApply)
            {
                case Vector2Axis.X:
                    SetVelocity = (vec2) => _player.Velocity.x = vec2.x;
                    break;
                case Vector2Axis.Y:
                    SetVelocity = (vec2) => _player.Velocity.y = vec2.y;
                    break;
                case Vector2Axis.XY:
                    SetVelocity = (vec2) => _player.Velocity = vec2;
                    break;
            }
        }

        public override void Act(float delta)
        {
            if (delta > 0)
                _currentTime += delta;
            else if (_currentTime >= _time)
            {
                return;
            }

            var direction = _applyRelativeToDirection == true ? _player.GetDirection() : 1; 
            var force = _force * direction;

            var weight = _speedCurve.Interpolate(_currentTime / _time);
            weight = Mathf.Clamp(weight, 0, 1);
            var forceToApply = force.LinearInterpolate(Vector2.Zero, weight) / delta;
            SetVelocity(forceToApply);
        }


        public override void OnStateEnter()
        {
            _currentTime = 0;
        }
    }
}