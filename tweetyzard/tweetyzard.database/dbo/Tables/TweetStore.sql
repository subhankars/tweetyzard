﻿CREATE TABLE [dbo].[TweetStore]
(
    [TweetId] INT IDENTITY,
	[Id] NUMERIC NOT NULL,
	[SearchPhrase] VARCHAR(255) NOT NULL,
    [CreatorName] VARCHAR(255) NOT NULL,
    [IdStr] VARCHAR(255) NOT NULL,
	[Text] VARCHAR(MAX) NOT NULL,
	[Favorited] BIT NULL,
	[CreatedAt] DATETIME NULL,
	[Truncated] BIT NULL,
	[InReplyToStatusId] NUMERIC,
    [InReplyToStatusIdStr] VARCHAR(255) NULL,
    [InReplyToUserId] NUMERIC,
	[InReplyToUserIdStr] VARCHAR(255) NULL,
	[InReplyToScreenName] VARCHAR(255) NULL,
	[RetweetCount] INT NULL,
    [Retweeted] BIT NULL,
    [Description] VARCHAR(MAX) NULL,
	[Location] VARCHAR(255) NULL,
	[GeoEnabled] BIT NULL,
	[Url] VARCHAR(MAX) NULL,
	[StatusesCount] INT NULL,
    [FollowersCount] INT NULL,
	[FriendsCount] INT NULL,
    [Following] BIT NULL,
	[Protected] BIT NULL,
	[Verified] BIT NULL,
	[Notifications] BIT NULL,
	[ProfileImageUrl] VARCHAR(MAX) NULL,
	[ProfileImageUrlHttps] VARCHAR(MAX) NULL,
	[FollowRequestSent] BIT NULL,
	[DefaultProfile] BIT NULL,
	[DefaultProfileImage] BIT NULL,
	[FavouritesCount] INT NULL,
	[ListedCount] INT NULL,
	[ProfileSidebarFillColor] VARCHAR(255) NULL,
	[ProfileSidebarBorderColor] VARCHAR(255) NULL,
	[ProfileBackgroundTitle] VARCHAR(255) NULL,
	[ProfileBackgroundColor] VARCHAR(255) NULL,
	[ProfileBackgroundImageUrl] VARCHAR(MAX) NULL,
	[ProfileBackgroundImageUrlHttps] VARCHAR(MAX) NULL,
	[ProfileTextColor] VARCHAR(255) NULL,
	[ProfileLinkColor] VARCHAR(255) NULL,
	[ProfileUseBackgroundImage] BIT NULL,
	[IsTranslator] BIT NULL,
	[ShowAllInlineMedia] BIT NULL,
	[ContributorsEnabled] BIT NULL,
	[UtcOffset] INT NULL,
	[TimeZone] VARCHAR(255) NULL,
    [Longitude] FLOAT NULL,
	[Latitude] FLOAT NULL, 
    CONSTRAINT [PK_TweetStore] PRIMARY KEY ([TweetId])
)
