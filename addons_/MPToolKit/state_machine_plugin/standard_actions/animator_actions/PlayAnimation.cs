using Godot;
using MP.Extensions;
using MP.AnimatorWrappers;

namespace MP.FiniteStateMachine.Actions
{
    public class PlayAnimation : InstantStateAction
    {
        [Export] private string _name;
        [Export] private float _transitionDuration = .25f;
        [Export] private float _playbackSpeed = 1;
        [Export] private NodePath _pathToAnimatedModel;

        private Animator _animator;

        public override void Init(StateMachine stateMachine)
        {
            _animator = GetNodeOrNull<Animator>(_pathToAnimatedModel);
        }

        public override void Act(float delta)
        {
            _animator.PlayAnimation(_name, _transitionDuration, _playbackSpeed);
        }
    }
}
