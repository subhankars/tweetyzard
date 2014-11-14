﻿CREATE TABLE [dbo].[TweetStore]
(
    [TweetId] INT IDENTITY,
	[Id] NUMERIC NOT NULL,
	[SearchPhrase] VARCHAR(255) NOT NULL,
    [CreatorName] VARCHAR(255) NOT NULL,
    [IdStr] VARCHAR(255) NOT NULL,
	[Text] VARCHAR(MAX) NOT NULL,
	[Favorited] BIT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[Truncated] BIT NOT NULL,
	[InReplyToStatusId] NUMERIC,
    [InReplyToStatusIdStr] VARCHAR NOT NULL,
    [InReplyToUserId] NUMERIC,
	[InReplyToUserIdStr] VARCHAR(255) NOT NULL,
	[InReplyToScreenName] VARCHAR(255) NOT NULL,
	[RetweetCount] INT NOT NULL,
    [Retweeted] BIT NOT NULL,
    [Description] VARCHAR(MAX) NOT NULL,
	[Location] VARCHAR(255) NOT NULL,
	[GeoEnabled] BIT NOT NULL,
	[Url] VARCHAR(MAX) NOT NULL,
	[StatusesCount] INT NOT NULL,
    [FollowersCount] INT NOT NULL,
	[FriendsCount] INT NOT NULL,
    [Following] BIT NOT NULL,
	[Protected] BIT NOT NULL,
	[Verified] BIT NOT NULL,
	[Notifications] BIT NOT NULL,
	[ProfileImageUrl] VARCHAR NOT NULL,
	[ProfileImageUrlHttps] VARCHAR NOT NULL,
	[FollowRequestSent] BIT NOT NULL,
	[DefaultProfile] BIT NOT NULL,
	[DefaultProfileImage] BIT NOT NULL,
	[FavouritesCount] INT NULL,
	[ListedCount] INT NULL,
	[ProfileSidebarFillColor] VARCHAR(255) NOT NULL,
	[ProfileSidebarBorderColor] VARCHAR(255) NOT NULL,
	[ProfileBackgroundTitle] VARCHAR(255) NOT NULL,
	[ProfileBackgroundColor] VARCHAR(255) NOT NULL,
	[ProfileBackgroundImageUrl] VARCHAR(MAX) NOT NULL,
	[ProfileBackgroundImageUrlHttps] VARCHAR(MAX) NOT NULL,
	[ProfileTextColor] VARCHAR(255) NOT NULL,
	[ProfileLinkColor] VARCHAR(255) NOT NULL,
	[ProfileUseBackgroundImage] BIT NOT NULL,
	[IsTranslator] BIT NOT NULL,
	[ShowAllInlineMedia] BIT NOT NULL,
	[ContributorsEnabled] BIT NOT NULL,
	[UtcOffset] INT NULL,
	[TimeZone] VARCHAR(255) NOT NULL,
    [Longitude] FLOAT NOT NULL,
	[Latitude] FLOAT NOT NULL, 
    CONSTRAINT [PK_TweetStore] PRIMARY KEY ([TweetId])
)