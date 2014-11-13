using TweetinviCore.Interfaces.Parameters;

namespace TweetinviCore.Interfaces.QueryGenerators
{
    public interface ITweetListQueryParameterGenerator
    {
        string GenerateIdentifierParameter(IListIdentifier listIdentifier);
    }
}