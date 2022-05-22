using Godot;
using MP.Extensions;
using System;

namespace MP.AnimatorWrappers
{
    public class AnimatorSpatial : Spatial, IAnimatorWrapper
    {
        [Export] private NodePath _pathToAnimationPlayer;
        [Export] private NodePath _pathToAnimationTree;
        private Animator _animator;

        public void PlayAnimation(in string name, float transitionDuration, float playbackSpeed)
        {
            (_animator).PlayAnimation(name, transitionDuration, playbackSpeed);
        }

        public void SetAnimatorEnum(string propertyName, object value)
        {
            (_animator).SetAnimatorEnum(propertyName, value);
        }

        public void SetAnimatorTimeScale(string propertyName, object value)
        {
            (_animator).SetAnimatorTimeScale(propertyName, value);
        }

        public void SetBlendPosition(in string path, object value)
        {
            (_animator).SetBlendPosition(path, value);
        }

        public void SetOneShotBool(string propertyName, object value)
        {
            (_animator).SetOneShotBool(propertyName, value);
        }

        public void SetParameter(AnimParametersType parameterType, in string name, object value)
        {
            (_animator).SetParameter(parameterType, name, value);
        }

        public override void _Ready()
        {
            _animator = new Animator(GetNode<AnimationPlayer>(_pathToAnimationPlayer), GetNode<AnimationTree>(_pathToAnimationTree), this);
        }


    }
}
