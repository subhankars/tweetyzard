using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviLogic
{
    public class Mention : Tweet, IMention
    {
        public Mention(
            ITweetDTO tweetDTO,
            ITweetController tweetController,
            ITweetFactory tweetFactory,
            IUserFactory userFactory) 
                
                : base(tweetDTO,
                       tweetController,
                       tweetFactory,
                       userFactory)
        {
            // Default constructor inheriting from the default Tweet constructor
        }

        public string Annotations { get; set; }
    }
}