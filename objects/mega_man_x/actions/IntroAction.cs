using Godot;
using MP.FiniteStateMachine;
using MP.Extensions;
using MP.AnimatorWrappers;

namespace XClone
{
    public class IntroAction : StateAction
    {
        [Export] private NodePath _pathToSprite;
        [Export] private Curve _speedCurve;
        [Export] private float _duration;
        [Export] private Vector2 _offset;

        private Animator2D _animator;
        private float _currentTime;
        private Vector2 _modifiedPosition;
        private Vector2 _originPosition;
        private Node2D _sprite;

        public override void Init(StateMachine stateMachine)
        {
            _animator = stateMachine.GetNodeOfType<Animator2D>();
            this.TryGetNodeFromPath(_pathToSprite, out _sprite);
            _originPosition = _sprite.Position;
            _modifiedPosition = _originPosition + _offset;
        }

        public override void OnStateEnter()
        {
            _sprite.Translate(_offset);
        }

        public override void OnStateExit()
        {
            _currentTime = 0;
        }

        public override void Act(float delta)
        {
            if (delta < 0)
                return;

            if (_currentTime > _duration)
            {
                return;
            }

            _currentTime += delta;
            _sprite.Position = _modifiedPosition.LinearInterpolate(_originPosition, _speedCurve.Interpolate(_currentTime / _duration));
        }
    }
}