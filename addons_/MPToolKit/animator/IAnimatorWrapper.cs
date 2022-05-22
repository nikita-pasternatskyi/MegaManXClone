namespace MP.AnimatorWrappers
{
    public interface IAnimatorWrapper
    {
        void PlayAnimation(in string name, float transitionDuration, float playbackSpeed);
        void SetBlendPosition(in string path, object value);
        void SetAnimatorEnum(string propertyName, object value);
        void SetOneShotBool(string propertyName, object value);
        void SetAnimatorTimeScale(string propertyName, object value);
        void SetParameter(AnimParametersType parameterType, in string name, object value);
    }
}
