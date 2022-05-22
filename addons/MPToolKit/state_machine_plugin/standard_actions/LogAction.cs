using Godot;

namespace MP.FiniteStateMachine
{
    public class LogAction : InstantStateAction
    {
        [Export] private string _message;

        public override void Act(float delta)
        {
            GD.Print(_message);
        }
    }
}