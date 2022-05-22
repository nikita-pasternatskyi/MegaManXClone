using Godot;
using MP.Extensions;
using MP.InputWrapper;

namespace MP.FiniteStateMachine
{
    public class ReplicateSpriteFrameToParticle2D : InstantStateAction
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
            GD.Print(_sprite);
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