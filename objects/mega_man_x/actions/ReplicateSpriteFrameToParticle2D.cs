using Godot;
using MP.Extensions;
using MP.FiniteStateMachine;
using MP.InputWrapper;

namespace XClone
{

    public class RotateSpriteOnInput2D : Node
    {
        [Export] private NodePath _pathToPlayerInput;

        public bool Facing { get; private set; }

        private Node2D _parent;

        private PlayerInput2D _playerInput2D;
        private bool _isFacingRight;
        private float _originalX;

        public override void _Ready()
        {
            this.TryGetNodeFromPath(_pathToPlayerInput, out _playerInput2D);
            //_parent = GetParent<>
            _originalX = this.GetParent<Node2D>().Scale.x;
        }

        public void ChangeDirection()
        {

        }

        public override void _Process(float delta)
        {
            Facing = _playerInput2D.MovementInput.x < 0;
        }
    }

    public class ReplicateSpriteFrameToParticle2D : StateAction
    {
        [Export] private NodePath _pathToSprite;
        [Export] private NodePath _pathToParticleSystem;

        private Sprite _sprite;
        private Particles2D _particles2D;
        
        private const string FrameParameter = "shader_param/particle_frame_to_display";
        private const string VFramesParameter = "shader_param/particles_anim_v_frames";
        private const string HFramesParameter = "shader_param/particles_anim_h_frames";

        public override void Init(StateMachine stateMachine)
        {
            this.TryGetNodeFromPath(_pathToSprite, out _sprite);
            this.TryGetNodeFromPath(_pathToParticleSystem, out _particles2D);
        }

        public override void Act(float delta)
        {
            var frameH = _sprite.Hframes;
            var frameV = _sprite.Vframes;
            var frame = _sprite.Frame;
            _particles2D.Material.Set(FrameParameter, frame);
            _particles2D.Material.Set(HFramesParameter, frameH);
            _particles2D.Material.Set(VFramesParameter, frameV);
        }
    }
}