using System;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;
using TweetinviFactories.Properties;

namespace TweetinviFactories.Direct_Messages
{
    public interface IMessageFactoryQueryExecutor
    {
        // Get Existing Message
        IMessageDTO GetExistingMessage(long messageId);

        // Create Message
        IMessageDTO CreateMessage(string text, IUserDTO recipientDTO);
    }

    public class MessageFactoryQueryExecutor : IMessageFactoryQueryExecutor
    {
        private readonly ITwitterAccessor _twitterAccessor;
        private readonly IUnityFactory<IMessageDTO> _messageDTOUnityFactory;

        public MessageFactoryQueryExecutor(
            ITwitterAccessor twitterAccessor,
            IUnityFactory<IMessageDTO> messageDTOUnityFactory)
        {
            _twitterAccessor = twitterAccessor;
            _messageDTOUnityFactory = messageDTOUnityFactory;
        }

        // Get existing message
        public IMessageDTO GetExistingMessage(long messageId)
        {
            string query = String.Format(Resources.Message_GetMessageFromId, messageId);
            return _twitterAccessor.ExecuteGETQuery<IMessageDTO>(query);
        }

        // Create Message
        public IMessageDTO CreateMessage(string text, IUserDTO recipientDTO)
        {
            var messageDTO = _messageDTOUnityFactory.Create();
            messageDTO.Text = text;
            messageDTO.Recipient = recipientDTO;
            return messageDTO;
        }
    }
}