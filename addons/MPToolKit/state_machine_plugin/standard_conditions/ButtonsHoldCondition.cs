using Godot;

namespace MP.FiniteStateMachine
{
    public class ButtonsHoldCondition : ButtonCondition
    {
        [Export] private float _time;
        private float _timer;

        protected override bool AdditionalCheck(bool initialResult = true)
        {
            if(initialResult == true)
            {
                _timer += GetProcessDeltaTime();
            }
            else if (initialResult == false)
            {
                _timer = 0;
            }
            
            if(_timer >= _time)
            {
                _timer = 0;
                return true;
            }

            return false;
        }
    }
}