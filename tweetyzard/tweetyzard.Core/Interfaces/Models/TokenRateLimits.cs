using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetinviCore.Interfaces.Credentials;

namespace TweetinviCore.Interfaces.Models
{
    public class TokenRateLimits : ITokenRateLimits
    {
        public string AccessToken
        {
            get;
            set;
        }

        public ITokenRateLimit AccountSettingsLimit
        {
            get { return _resources.AccountRateLimits["/account/settings"]; }
        }

        public ITokenRateLimit AccountVerifyCredentialsLimit
        {
            get { return _resources.AccountRateLimits["/account/verify_credentials"]; }
        }

        public ITokenRateLimit ApplicationRateLimitStatusLimit
        {
            get { return _resources.ApplicationRateLimits["/application/rate_limit_status"]; }
        }

        public ITokenRateLimit BlocksIdsLimit
        {
            get { return _resources.BlocksRateLimits["/blocks/ids"]; }
        }

        public ITokenRateLimit BlocksListLimit
        {
            get { return _resources.BlocksRateLimits["/blocks/list"]; }
        }

        public ITokenRateLimit DirectMessagesLimit
        {
            get { return _resources.DirectMessagesRateLimits["/direct_messages"]; }
        }

        public ITokenRateLimit DirectMessagesSentLimit
        {
            get { return _resources.DirectMessagesRateLimits["/direct_messages/sent"]; }
        }

        public ITokenRateLimit DirectMessagesSentAndReceivedLimit
        {
            get { return _resources.DirectMessagesRateLimits["/direct_messages/sent_and_received"]; }
        }

        public ITokenRateLimit DirectMessagesShowLimit
        {
            get { return _resources.DirectMessagesRateLimits["/direct_messages/show"]; }
        }

        public ITokenRateLimit FavoritesListLimit
        {
            get { return _resources.FavoritesRateLimits["/favorites/list"]; }
        }

        public ITokenRateLimit FollowersIdsLimit
        {
            get { return _resources.FollowersRateLimits["/followers/ids"]; }
        }

        public ITokenRateLimit FollowersListLimit
        {
            get { return _resources.FollowersRateLimits["/followers/list"]; }
        }

        public ITokenRateLimit FriendsIdsLimit
        {
            get { return _resources.FriendsRateLimits["/friends/ids"]; }
        }

        public ITokenRateLimit FriendsListLimit
        {
            get { return _resources.FriendsRateLimits["/friends/list"]; }
        }

        public ITokenRateLimit FriendshipsIncomingLimit
        {
            get { return _resources.FriendshipsRateLimits["/friendships/incoming"]; }
        }

        public ITokenRateLimit FriendshipsLookupLimit
        {
            get { return _resources.FriendshipsRateLimits["/friendships/lookup"]; }
        }

        public ITokenRateLimit FriendshipsNoRetweetsIdsLimit
        {
            get { return _resources.FriendshipsRateLimits["/friendships/no_retweets/ids"]; }
        }

        public ITokenRateLimit FriendshipsOutgoingLimit
        {
            get { return _resources.FriendshipsRateLimits["/friendships/outgoing"]; }
        }

        public ITokenRateLimit FriendshipsShowLimit
        {
            get { return _resources.FriendshipsRateLimits["/friendships/show"]; }
        }

        public ITokenRateLimit GeoGetPlaceFromIdLimit
        {
            get { return _resources.GeoRateLimits["/geo/id/:place_id"]; }
        }

        public ITokenRateLimit GeoReverseGeoCodeLimit
        {
            get { return _resources.GeoRateLimits["/geo/reverse_geocode"]; }
        }

        public ITokenRateLimit GeoSearchLimit
        {
            get { return _resources.GeoRateLimits["/geo/search"]; }
        }

        public ITokenRateLimit GeoSimilarPlacesLimit
        {
            get { return _resources.GeoRateLimits["/geo/similar_places"]; }
        }

        public ITokenRateLimit HelpConfigurationLimit
        {
            get { return _resources.HelpRateLimits["/help/configuration"]; }
        }

        public ITokenRateLimit HelpLanguagesLimit
        {
            get { return _resources.HelpRateLimits["/help/languages"]; }
        }

        public ITokenRateLimit HelpPrivacyLimit
        {
            get { return _resources.HelpRateLimits["/help/privacy"]; }
        }

        public ITokenRateLimit HelpTosLimit
        {
            get { return _resources.HelpRateLimits["/help/tos"]; }
        }

        public ITokenRateLimit ListsListLimit
        {
            get { return _resources.ListsRateLimits["/lists/list"]; }
        }

        public ITokenRateLimit ListsMembersLimit
        {
            get { return _resources.ListsRateLimits["/lists/members"]; }
        }

        public ITokenRateLimit ListsMembersShowLimit
        {
            get { return _resources.ListsRateLimits["/lists/members/show"]; }
        }

        public ITokenRateLimit ListsMembershipsLimit
        {
            get { return _resources.ListsRateLimits["/lists/memberships"]; }
        }

        public ITokenRateLimit ListsOwnershipsLimit
        {
            get { return _resources.ListsRateLimits["/lists/ownerships"]; }
        }

        public ITokenRateLimit ListsShowLimit
        {
            get { return _resources.ListsRateLimits["/lists/show"]; }
        }

        public ITokenRateLimit ListsStatusesLimit
        {
            get { return _resources.ListsRateLimits["/lists/statuses"]; }
        }

        public ITokenRateLimit ListsSubscribersLimit
        {
            get { return _resources.ListsRateLimits["/lists/subscribers"]; }
        }

        public ITokenRateLimit ListsSubscribersShowLimit
        {
            get { return _resources.ListsRateLimits["/lists/subscribers/show"]; }
        }

        public ITokenRateLimit ListsSubscriptionsLimit
        {
            get { return _resources.ListsRateLimits["/lists/subscriptions"]; }
        }

        public ITokenRateLimit SavedSearchesListLimit
        {
            get { return _resources.SavedSearchesRateLimits["/saved_searches/list"]; }
        }

        public ITokenRateLimit SavedSearchesShowIdLimit
        {
            get { return _resources.SavedSearchesRateLimits["/saved_searches/show/:id"]; }
        }

        public ITokenRateLimit SearchTweetsLimit
        {
            get { return _resources.SearchRateLimits["/search/tweets"]; }
        }

        public ITokenRateLimit StatusesHomeTimelineLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/home_timeline"]; }
        }

        public ITokenRateLimit StatusesMentionsTimelineLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/mentions_timeline"]; }
        }

        public ITokenRateLimit StatusesOembedLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/oembed"]; }
        }

        public ITokenRateLimit StatusesRetweetersIdsLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/retweeters/ids"]; }
        }

        public ITokenRateLimit StatusesRetweetsIdLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/retweets/:id"]; }
        }

        public ITokenRateLimit StatusesRetweetsOfMeLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/retweets_of_me"]; }
        }

        public ITokenRateLimit StatusesShowIdLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/show/:id"]; }
        }

        public ITokenRateLimit StatusesUserTimelineLimit
        {
            get { return _resources.StatusesRateLimits["/statuses/user_timeline"]; }
        }

        public ITokenRateLimit TrendsAvailableLimit
        {
            get { return _resources.TrendsRateLimits["/trends/available"]; }
        }

        public ITokenRateLimit TrendsClosestLimit
        {
            get { return _resources.TrendsRateLimits["/trends/closest"]; }
        }

        public ITokenRateLimit TrendsPlaceLimit
        {
            get { return _resources.TrendsRateLimits["/trends/place"]; }
        }

        public ITokenRateLimit UsersContributeesLimit
        {
            get { return _resources.UsersRateLimits["/users/contributees"]; }
        }

        public ITokenRateLimit UsersContributorsLimit
        {
            get { return _resources.UsersRateLimits["/users/contributors"]; }
        }

        public ITokenRateLimit UsersLookupLimit
        {
            get { return _resources.UsersRateLimits["/users/lookup"]; }
        }

        public ITokenRateLimit UsersProfileBannerLimit
        {
            get { return _resources.UsersRateLimits["/users/profile_banner"]; }
        }

        public ITokenRateLimit UsersSearchLimit
        {
            get { return _resources.UsersRateLimits["/users/search"]; }
        }

        public ITokenRateLimit UsersShowIdLimit
        {
            get { return _resources.UsersRateLimits["/users/show/:id"]; }
        }

        public ITokenRateLimit UsersSuggestionsLimit
        {
            get { return _resources.UsersRateLimits["/users/suggestions"]; }
        }

        public ITokenRateLimit UsersSuggestionsSlugLimit
        {
            get { return _resources.UsersRateLimits["/users/suggestions/:slug"]; }
        }

        public ITokenRateLimit UsersSuggestionsSlugMembersLimit
        {
            get { return _resources.UsersRateLimits["/users/suggestions/:slug/members"]; }
        }


        private class RateLimitResources
        {
            [JsonProperty("account")]
            public Dictionary<string, ITokenRateLimit> AccountRateLimits { get; set; }

            [JsonProperty("application")]
            public Dictionary<string, ITokenRateLimit> ApplicationRateLimits { get; set; }

            [JsonProperty("blocks")]
            public Dictionary<string, ITokenRateLimit> BlocksRateLimits { get; set; }

            [JsonProperty("direct_messages")]
            public Dictionary<string, ITokenRateLimit> DirectMessagesRateLimits { get; set; }

            [JsonProperty("favorites")]
            public Dictionary<string, ITokenRateLimit> FavoritesRateLimits { get; set; }

            [JsonProperty("followers")]
            public Dictionary<string, ITokenRateLimit> FollowersRateLimits { get; set; }

            [JsonProperty("friends")]
            public Dictionary<string, ITokenRateLimit> FriendsRateLimits { get; set; }

            [JsonProperty("friendships")]
            public Dictionary<string, ITokenRateLimit> FriendshipsRateLimits { get; set; }

            [JsonProperty("geo")]
            public Dictionary<string, ITokenRateLimit> GeoRateLimits { get; set; }

            [JsonProperty("help")]
            public Dictionary<string, ITokenRateLimit> HelpRateLimits { get; set; }

            [JsonProperty("lists")]
            public Dictionary<string, ITokenRateLimit> ListsRateLimits { get; set; }

            [JsonProperty("saved_searches")]
            public Dictionary<string, ITokenRateLimit> SavedSearchesRateLimits { get; set; }

            [JsonProperty("search")]
            public Dictionary<string, ITokenRateLimit> SearchRateLimits { get; set; }

            [JsonProperty("statuses")]
            public Dictionary<string, ITokenRateLimit> StatusesRateLimits { get; set; }

            [JsonProperty("trends")]
            public Dictionary<string, ITokenRateLimit> TrendsRateLimits { get; set; }

            [JsonProperty("users")]
            public Dictionary<string, ITokenRateLimit> UsersRateLimits { get; set; }
        }

        [JsonProperty("rate_limit_context")]
        private JObject _rateLimitContext
        {
            set
            {
                AccessToken = value["access_token"].ToObject<string>();
            }
        }

        [JsonProperty("resources")]
        private RateLimitResources _resources { get; set; }
    }
}