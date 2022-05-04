using Godot;
using MP.AnimatorWrappers;
using MP.Extensions;
using MP.StateMachine;

namespace MP.PlayerKinematicBody
{

    public class TeleportAction : StateAction
    {
        [Export] private Vector2 _temporary_vector_offset;
        [Export] private float _waitTime;
        [Export] private NodePath _pathToSprite;
        private Vector2 _velocity;
        private AnimatedModel _sprite;

        public override void Init(BaseStateMachine stateMachine)
        {
            this.TryGetNodeFromPath(_pathToSprite, out _sprite);
            _sprite.Translate(_temporary_vector_offset);
            _velocity = -_temporary_vector_offset / _waitTime;
        }

        public override void Act(float delta)
        {
            if (delta == -1)
                return;
            _sprite.Translate(_velocity * delta);
        }
    }
}