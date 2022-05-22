using Godot;

namespace MP.FiniteStateMachine
{
    public abstract class PlaySound<T> : InstantStateAction where T : AudioStream
    {
        [Export] private T _soundToPlay;
        [Export] private bool _interrupt = true;
        private AudioStreamPlayer2D _audioPlayer;

        public override void Init(StateMachine stateMachine)
        {
            _audioPlayer = stateMachine.GetCachedNode<AudioStreamPlayer2D>();
        }

        public override void Act(float delta)
        {
            if (_audioPlayer.Playing != true && _interrupt == false)
                return;
            _audioPlayer.Stream = _soundToPlay;
            _audioPlayer.Play();
        }
    }
}