using Godot;
using MP.Extensions;
using System.Linq;

namespace MP.FiniteStateMachine
{
    public enum ConditionOperator
    {
        And, Or
    }

    public sealed class Transition : Node
    {
        [Export] private NodePath _toStatePath;
       
        private Condition[] _conditions;
        private Condition[] _mandatoryConditions;

        public State ToState { get; private set; }

        public void Init(StateMachine stateMachine)
        {
            ToState = GetNode<State>(_toStatePath);
            this.Disable();
            var conditions = this.GetChildren<Condition>();
            _mandatoryConditions = conditions.Where((cond) => cond.ConditionOperator == ConditionOperator.And).ToArray();
            _conditions = conditions.Where((cond) => cond.ConditionOperator == ConditionOperator.Or).ToArray();
            foreach (var condition in _mandatoryConditions)
            {
                condition.Init(stateMachine);
            }

            foreach (var condition in _conditions)
            {
                condition.Init(stateMachine);
            }
        }

        public bool Check()
        {
            foreach(var condition in _mandatoryConditions)
            {
                if (condition.Check() == false)
                    return false;
            }

            if(_conditions.Length == 0)
            {
                return true;
            }

            foreach(var condition in _conditions)
            {
                if (condition.Check() == true)
                    return true;
            }

            return false;
        }
    }
}