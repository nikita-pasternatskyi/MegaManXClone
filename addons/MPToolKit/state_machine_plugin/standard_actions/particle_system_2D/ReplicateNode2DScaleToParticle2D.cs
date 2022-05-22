using Godot;
using MP.Extensions;

namespace MP.FiniteStateMachine
{
    public class ReplicateNode2DScaleToParticle2D : InstantStateAction
    {
        [Export] private NodePath _pathToObject;
        [Export] private NodePath _pathToParticleSystem;

        private Node2D _objectScale;
        private Particles2D _particles2D;

        private const string ScaleParameter = "shader_param/scale";

        public override void Init(StateMachine stateMachine)
        {
            this.TryGetNodeFromPath(_pathToObject, out _objectScale);
            this.TryGetNodeFromPath(_pathToParticleSystem, out _particles2D);
        }

        public override void Act(float delta)
        {
            var scale = _objectScale.Scale;
            var y = scale.y;
            scale.y = scale.x;
            scale.x = y;
            _particles2D.Material.Set(ScaleParameter, scale);
        }
    }
}