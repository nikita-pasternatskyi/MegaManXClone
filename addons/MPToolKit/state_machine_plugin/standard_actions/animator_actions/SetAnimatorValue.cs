using Godot;
using MP.AnimatorWrappers;
using MP.Extensions;

namespace MP.FiniteStateMachine.Actions
{
    public abstract class SetAnimatorValue<T> : InstantStateAction
    {
        [Export] private NodePath _animatorPath;
        [Export] protected string PropertyName;
        [Export] protected T Value;

        protected IAnimatorWrapper Animator => _animator;
        private IAnimatorWrapper _animator;
        
        public override void Init(StateMachine stateMachine)
        {
            _animator = (IAnimatorWrapper)GetNode(_animatorPath);
        }
    }
}
