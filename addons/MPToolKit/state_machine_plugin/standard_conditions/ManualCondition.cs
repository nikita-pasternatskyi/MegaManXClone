namespace MP.FiniteStateMachine
{
    public class ManualCondition : Condition
    {
        private bool _result;

        public void CheckTrue() => _result = true;
        public void CheckFalse() => _result = false;

        protected override bool ConditionCheck()
        {
            if(_result == true)
            {
                _result = false;
                return true;
            }
            return false;
        }
    }
}