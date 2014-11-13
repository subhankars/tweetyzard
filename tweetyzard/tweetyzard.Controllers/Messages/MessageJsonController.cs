using System;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Credentials;
using TweetinviCore.Interfaces.DTO;

namespace TweetinviControllers.Messages
{
    public interface IMessageJsonController
    {
        string GetLatestMessagesReceived(int maximumMessages = 40);
        string GetLatestMessagesSent(int maximumMessages = 40);

        // Publish Message
        string PublishMessage(IMessage message);
        string PublishMessage(IMessageDTO messageDTO);
        string PublishMessage(string text, IUserIdDTO targetUserDTO);
        string PublishMessage(string text, string targetUserScreenName);
        string PublishMessage(string text, long targetUserId);

        // Destroy Message
        string DestroyMessage(IMessage message);
        string DestroyMessage(IMessageDTO messageDTO);
        string DestroyMessage(long messageId);
    }

    public class MessageJsonController : IMessageJsonController
    {
        private readonly IMessageQueryGenerator _messageQueryGenerator;
        private readonly ITwitterAccessor _twitterAccessor;

        public MessageJsonController(
            IMessageQueryGenerator messageQueryGenerator,
            ITwitterAccessor twitterAccessor)
        {
            _messageQueryGenerator = messageQueryGenerator;
            _twitterAccessor = twitterAccessor;
        }

        // Get Messages
        public string GetLatestMessagesReceived(int maximumMessages = 40)
        {
            string query = _messageQueryGenerator.GetLatestMessagesReceivedQuery(maximumMessages);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        public string GetLatestMessagesSent(int maximumMessages = 40)
        {
            string query = _messageQueryGenerator.GetLatestMessagesSentQuery(maximumMessages);
            return _twitterAccessor.ExecuteJsonGETQuery(query);
        }

        // Publish Message
        public string PublishMessage(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentException("Message cannot be null");
            }

            return PublishMessage(message.MessageDTO);
        }

        public string PublishMessage(IMessageDTO messageDTO)
        {
            string query = _messageQueryGenerator.GetPublishMessageQuery(messageDTO);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        public string PublishMessage(string messageText, IUserIdDTO targetUserDTO)
        {
            string query = _messageQueryGenerator.GetPublishMessageQuery(messageText, targetUserDTO);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        public string PublishMessage(string messageText, string targetUserScreenName)
        {
            string query = _messageQueryGenerator.GetPublishMessageQuery(messageText, targetUserScreenName);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        public string PublishMessage(string messageText, long targetUserId)
        {
            string query = _messageQueryGenerator.GetPublishMessageQuery(messageText, targetUserId);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        // Destroy Message
        public string DestroyMessage(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentException("Message cannot be null");
            }

            return DestroyMessage(message.MessageDTO);
        }

        public string DestroyMessage(IMessageDTO messageDTO)
        {
            string query = _messageQueryGenerator.GetDestroyMessageQuery(messageDTO);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }

        public string DestroyMessage(long messageId)
        {
            string query = _messageQueryGenerator.GetDestroyMessageQuery(messageId);
            return _twitterAccessor.ExecuteJsonPOSTQuery(query);
        }
    }
}