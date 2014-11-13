using TweetinviCore.Enum;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Parameters;

namespace TweetinviFactories.Lists
{
    public interface ITweetListFactoryQueryExecutor
    {
        ITweetListDTO CreateList(string name, PrivacyMode privacyMode, string description);
        ITweetListDTO GetExistingTweetList(IListIdentifier identifier);
    }

    public class TweetListFactoryQueryExecutor : ITweetListFactoryQueryExecutor
    {
        private readonly ITweetListFactoryQueryGenerator _tweetListFactoryQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public TweetListFactoryQueryExecutor(
            ITweetListFactoryQueryGenerator tweetListFactoryQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _tweetListFactoryQueryGenerator = tweetListFactoryQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        public ITweetListDTO CreateList(string name, PrivacyMode privacyMode, string description)
        {
            var query = _tweetListFactoryQueryGenerator.GetCreateListQuery(name, privacyMode, description);
            return _twitterAccessor.ExecutePOSTQuery<ITweetListDTO>(query);
        }

        // Get existing list
        public ITweetListDTO GetExistingTweetList(IListIdentifier identifier)
        {
            string query = _tweetListFactoryQueryGenerator.GetListByIdQuery(identifier);
            return _twitterAccessor.ExecuteGETQuery<ITweetListDTO>(query);
        }
    }
}