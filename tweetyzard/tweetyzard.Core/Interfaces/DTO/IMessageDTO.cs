﻿using System;
using TweetinviCore.Interfaces.Models;

namespace TweetinviCore.Interfaces.DTO
{
    public interface IMessageDTO
    {
        bool IsMessagePublished { get; set; }
        bool IsMessageDestroyed { get; set; }

        long Id { get; set; }
        string IdStr { get; set; }

        string Text { get; set; }
        DateTime CreatedAt { get; set; }
        ITweetEntities Entities { get; set; }

        long SenderId { get; set; }
        string SenderScreenName { get; set; }
        IUserDTO Sender { get; set; }

        long RecipientId { get; set; }
        string RecipientScreenName { get; set; }
        IUserDTO Recipient { get; set; }
    }
}