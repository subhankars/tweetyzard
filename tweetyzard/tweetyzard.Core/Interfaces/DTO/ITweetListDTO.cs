using System;
using TweetinviCore.Enum;

namespace TweetinviCore.Interfaces.DTO
{
    public interface ITweetListDTO
    {
        long Id { get; set; }
        string IdStr { get; set; }

        string Slug { get; set; }
        string Name { get; set; }
        string FullName { get; set; }

        IUserDTO Creator { get; set; }
        DateTime CreatedAt { get; set; }
        string Uri { get; set; }
        string Description { get; set; }
        bool Following { get; set; }
        PrivacyMode PrivacyMode { get; set; }

        int MemberCount { get; set; }        
        int SubscriberCount { get; set; }
    }
}