using Godot;
using MP.Extensions;
using MP.FiniteStateMachine;

namespace XClone
{
    public class SetVisibility2D : StateAction
    {
        [Export] private NodePath _pathToNode;
        [Export] private bool _visibility;

        private Node2D _node;

        public override void Init(StateMachine stateMachine)
        {
            this.TryGetNodeFromPath(_pathToNode, out _node);
        }

        public override void Act(float delta)
        {
            _node.Visible = _visibility;
        }
    }
}