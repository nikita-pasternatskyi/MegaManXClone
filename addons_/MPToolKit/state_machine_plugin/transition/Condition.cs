using Godot;

namespace MP.FiniteStateMachine
{
    public abstract class Condition : Node
    {
        [Export] private ConditionOperator _conditionOperator;
        [Export] private bool _awaitedCondition = true;

        public ConditionOperator ConditionOperator => _conditionOperator;

        public virtual void Init(StateMachine baseStateMachine) 
        {
        
        }

        public bool Check()
        {
            return ConditionCheck() == _awaitedCondition;
        }

        protected abstract bool ConditionCheck();
    }
}