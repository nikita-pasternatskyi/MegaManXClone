using Godot;
using MP.Extensions;

namespace MP.FiniteStateMachine
{
    public class SetParticleSystemEmitting : InstantStateAction
    {
        [Export] private bool _value;
        [Export] private NodePath _pathToParticleSystem;

        private Particles2D _particles2D;

        public override void Init(StateMachine stateMachine)
        {
            this.TryGetNodeFromPath(_pathToParticleSystem, out _particles2D);
        }

        public override void Act(float delta)
        {
            _particles2D.Emitting = _value;
        }

    }
}