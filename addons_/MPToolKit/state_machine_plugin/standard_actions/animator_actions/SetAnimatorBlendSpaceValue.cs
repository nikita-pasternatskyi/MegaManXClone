using MP.AnimatorWrappers;

namespace MP.FiniteStateMachine.Actions
{
    public class SetAnimatorBlendSpaceValue : SetAnimatorValue<float>
    {
        public override void Act(float delta)
        {
            Animator.SetBlendPosition(PropertyName, Value);
        }
    }
}
