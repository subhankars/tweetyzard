using TweetinviCore.Wrappers;

namespace TweetinviCore.Interfaces.Factories
{
    public interface IUnityFactory
    {
        IParameterOverrideWrapper GenerateParameterOverrideWrapper<T>(string parameterName, T parameterValue);
    }

    public interface IUnityFactory<T> : IUnityFactory
    {
        T Create();
        T Create(params IResolverOverrideWrapper[] resolverOverrideWrappers);
    }
}