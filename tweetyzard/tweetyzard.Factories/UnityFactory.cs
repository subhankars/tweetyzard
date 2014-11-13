using System.Linq;
using Microsoft.Practices.Unity;
using TweetinviCore.Interfaces.Factories;
using TweetinviCore.Wrappers;

namespace TweetinviFactories
{
    public class UnityFactory<T> : IUnityFactory<T>
    {
        private readonly IUnityContainer _container;

        public UnityFactory(IUnityContainer container)
        {
            _container = container;
        }

        public T Create()
        {
            return _container.Resolve<T>();
        }

        public T Create(params IResolverOverrideWrapper[] resolverOverrideWrappers)
        {
            var resolverOverrides = resolverOverrideWrappers.Select(w => w.ResolverOverride as ResolverOverride).ToArray();
            return _container.Resolve<T>(resolverOverrides);
        }

        public IParameterOverrideWrapper GenerateParameterOverrideWrapper<T1>(string parameterName, T1 parameterValue)
        {
            var parameterOverrideWrapper = _container.Resolve<IParameterOverrideWrapper>();

            parameterOverrideWrapper.ParameterName = parameterName;
            parameterOverrideWrapper.ParameterValue = parameterValue;

            return parameterOverrideWrapper;
        }
    }
}