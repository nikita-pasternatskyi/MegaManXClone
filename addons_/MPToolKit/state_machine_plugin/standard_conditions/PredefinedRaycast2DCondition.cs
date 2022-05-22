using Godot;
using MP.Extensions;
using MP.Player;

namespace MP.FiniteStateMachine
{
    public class PredefinedRaycast2DCondition : Condition
    {
        [Export] private NodePath _pathToRaycast;

        private RayCast2D _rayCast2D;

        public override void Init(StateMachine baseStateMachine)
        {
            this.TryGetNodeFromPath(_pathToRaycast, out _rayCast2D);
        }

        protected override bool ConditionCheck()
        {
            _rayCast2D.ForceRaycastUpdate();
            var res = _rayCast2D.IsColliding();
            return res;
        }
    }
}