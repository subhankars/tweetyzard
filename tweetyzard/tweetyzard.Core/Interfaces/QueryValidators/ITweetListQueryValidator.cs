using TweetinviCore.Interfaces.Parameters;

namespace TweetinviCore.Interfaces.QueryValidators
{
    public interface ITweetListQueryValidator
    {
        // ListParameter
        bool IsListUpdateParametersValid(IListUpdateParameters parameters);
        
        // Parameters
        bool IsDescriptionParameterValid(string description);
        bool IsNameParameterValid(string name);
        
        // Identifiers
        bool IsListIdentifierValid(IListIdentifier parameters);
        bool IsOwnerScreenNameValid(string ownerScreeName);
        bool IsOwnerIdValid(long ownerId);
    }
}