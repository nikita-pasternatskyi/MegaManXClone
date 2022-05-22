using MP.AnimatorWrappers;

namespace MP.FiniteStateMachine.Actions
{
    public class SetAnimatorOneShotBoolean : SetAnimatorValue<bool>
    {
        public override void Act(float delta)
        {
            Animator.SetOneShotBool(PropertyName, Value);
        }
    }
}
