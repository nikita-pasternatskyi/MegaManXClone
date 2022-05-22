using MP.AnimatorWrappers;

namespace MP.FiniteStateMachine.Actions
{
    public class SetAnimatorStateEnum : SetAnimatorValue<int>
    {
        public override void Act(float delta)
        {
            Animator.SetAnimatorEnum(PropertyName, Value);
        }
    }
}
