﻿namespace TweetinviCore.Interfaces.DTO
{
    public interface ITweetDeletedInfo
    {
        long Id { get; }
        string IdStr { get; }
        long UserId { get; }
        string UserIdStr { get; }
    }
}