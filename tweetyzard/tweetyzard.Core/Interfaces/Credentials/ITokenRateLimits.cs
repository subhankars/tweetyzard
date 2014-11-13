namespace TweetinviCore.Interfaces.Credentials
{
    /// <summary>
    /// Lists of Rate Limits provided by Twitter API 1.1
    /// https://dev.twitter.com/docs/rate-limiting/1.1/limits
    /// </summary>
    public interface ITokenRateLimits
    {
        string AccessToken { get; }

        ITokenRateLimit AccountSettingsLimit { get; }
        ITokenRateLimit AccountVerifyCredentialsLimit { get; }

        ITokenRateLimit ApplicationRateLimitStatusLimit { get; }

        ITokenRateLimit BlocksIdsLimit { get; }
        ITokenRateLimit BlocksListLimit { get; }

        ITokenRateLimit DirectMessagesLimit { get; }
        ITokenRateLimit DirectMessagesSentLimit { get; }
        ITokenRateLimit DirectMessagesSentAndReceivedLimit { get; }
        ITokenRateLimit DirectMessagesShowLimit { get; }

        ITokenRateLimit FavoritesListLimit { get; }

        ITokenRateLimit FollowersIdsLimit { get; }
        ITokenRateLimit FollowersListLimit { get; }

        ITokenRateLimit FriendsIdsLimit { get; }
        ITokenRateLimit FriendsListLimit { get; }

        ITokenRateLimit FriendshipsIncomingLimit { get; }
        ITokenRateLimit FriendshipsLookupLimit { get; }
        ITokenRateLimit FriendshipsNoRetweetsIdsLimit { get; }
        ITokenRateLimit FriendshipsOutgoingLimit { get; }
        ITokenRateLimit FriendshipsShowLimit { get; }

        ITokenRateLimit GeoGetPlaceFromIdLimit { get; }
        ITokenRateLimit GeoReverseGeoCodeLimit { get; }
        ITokenRateLimit GeoSearchLimit { get; }
        ITokenRateLimit GeoSimilarPlacesLimit { get; }

        ITokenRateLimit HelpConfigurationLimit { get; }
        ITokenRateLimit HelpLanguagesLimit { get; }
        ITokenRateLimit HelpPrivacyLimit { get; }
        ITokenRateLimit HelpTosLimit { get; }

        ITokenRateLimit ListsListLimit { get; }
        ITokenRateLimit ListsMembersLimit { get; }
        ITokenRateLimit ListsMembersShowLimit { get; }
        ITokenRateLimit ListsMembershipsLimit { get; }
        ITokenRateLimit ListsOwnershipsLimit { get; }
        ITokenRateLimit ListsShowLimit { get; }
        ITokenRateLimit ListsStatusesLimit { get; }
        ITokenRateLimit ListsSubscribersLimit { get; }
        ITokenRateLimit ListsSubscribersShowLimit { get; }
        ITokenRateLimit ListsSubscriptionsLimit { get; }

        ITokenRateLimit SavedSearchesListLimit { get; }
        ITokenRateLimit SavedSearchesShowIdLimit { get; }

        ITokenRateLimit SearchTweetsLimit { get; }

        ITokenRateLimit StatusesHomeTimelineLimit { get; }
        ITokenRateLimit StatusesMentionsTimelineLimit { get; }
        ITokenRateLimit StatusesOembedLimit { get; }
        ITokenRateLimit StatusesRetweetersIdsLimit { get; }
        ITokenRateLimit StatusesRetweetsIdLimit { get; }
        ITokenRateLimit StatusesRetweetsOfMeLimit { get; }
        ITokenRateLimit StatusesShowIdLimit { get; }
        ITokenRateLimit StatusesUserTimelineLimit { get; }

        ITokenRateLimit TrendsAvailableLimit { get; }
        ITokenRateLimit TrendsClosestLimit { get; }
        ITokenRateLimit TrendsPlaceLimit { get; }

        ITokenRateLimit UsersContributeesLimit { get; }
        ITokenRateLimit UsersContributorsLimit { get; }
        ITokenRateLimit UsersLookupLimit { get; }
        ITokenRateLimit UsersProfileBannerLimit { get; }
        ITokenRateLimit UsersSearchLimit { get; }
        ITokenRateLimit UsersShowIdLimit { get; }
        ITokenRateLimit UsersSuggestionsLimit { get; }
        ITokenRateLimit UsersSuggestionsSlugLimit { get; }
        ITokenRateLimit UsersSuggestionsSlugMembersLimit { get; }
    }
}