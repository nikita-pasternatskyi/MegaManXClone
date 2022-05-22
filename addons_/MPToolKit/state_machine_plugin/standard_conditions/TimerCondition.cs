using Godot;
using System;

namespace MP.FiniteStateMachine
{
    public class TimerCondition : Condition
    {
        [Export] private float _seconds;
        [Signal] private delegate void TimeOut();
        public event Action Completed;

        private Timer _timer;
        private bool _result;

        public override void Init(StateMachine baseStateMachine)
        {
            _timer = new Timer();
            _timer.Autostart = false;
            _timer.Connect(GodotStandardSignals.TIMER_TIMEOUT, this, nameof(OnTimeOut));
            AddChild(_timer);
        }

        private void OnTimeOut()
        {
            _result = true;
            Completed?.Invoke();
            EmitSignal(nameof(TimeOut));
        }

        protected override bool ConditionCheck()
        {
            if(_result == false)
            {
                if (_timer.IsStopped() == true) 
                {
                    _timer.Start(_seconds);
                }
                return false;
            }

            _result = false;
            _timer.Stop();
            return true;
        }

    }
}