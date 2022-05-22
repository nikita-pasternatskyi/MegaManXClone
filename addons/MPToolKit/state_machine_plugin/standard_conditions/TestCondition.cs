using Godot;
namespace MP.FiniteStateMachine
{
    public class TestCondition : Condition
    {
        [Export] private bool _condition;

        protected override bool ConditionCheck()
        {
            return _condition;
        }
    }
}