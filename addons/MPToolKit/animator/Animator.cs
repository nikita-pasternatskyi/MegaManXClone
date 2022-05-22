using Godot;

namespace MP.AnimatorWrappers
{
    public class Animator : IAnimatorWrapper
    {
        private Node _owner;
        private AnimationPlayer _animationPlayer;
        private AnimationTree _tree;

        private const string RootName = "parameters/";
        private const string BlendParameterName = "/blend_position";
        private const string EnumParameterName = "/current";
        private const string OneShotParameterName = "/active";
        private const string TimeScaleParameterName = "/scale";

        public Animator(AnimationPlayer animationPlayer, AnimationTree tree, Node owner)
        {
            _animationPlayer = animationPlayer;
            _tree = tree;
            _owner = owner;
        }

        public void PlayAnimation(in string name, float transitionDuration, float playbackSpeed)
        {
            if (_animationPlayer.CurrentAnimation != name)
                _animationPlayer.Play(name, transitionDuration, playbackSpeed);
        }

        public void SetBlendPosition(in string path, object value)
        {
            _tree.Set(RootName + path + BlendParameterName, value);
        }

        public void SetAnimatorEnum(string propertyName, object value)
        {
            _tree.Set(RootName + propertyName + EnumParameterName, value);
        }

        public void SetOneShotBool(string propertyName, object value)
        {
            _tree.Set(RootName + propertyName + OneShotParameterName, value);
        }

        public void SetAnimatorTimeScale(string propertyName, object value)
        {
            _tree.Set(RootName + propertyName + TimeScaleParameterName, value);
        }

        public void SetParameter(AnimParametersType parameterType, in string name, object value)
        {
            switch (parameterType)
            {
                case AnimParametersType.BlendSpace1D:
                    SetBlendPosition(name, value);
                    break;
                case AnimParametersType.AnimationEnum:
                    SetAnimatorEnum(name, value);
                    break;
                case AnimParametersType.TimeScale:
                    SetAnimatorTimeScale(name, value);
                    break;
                case AnimParametersType.OneShotBoolean:
                    SetOneShotBool(name, value);
                    break;
            }
        }
    }
}
