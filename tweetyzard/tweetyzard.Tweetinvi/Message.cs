using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Controllers;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace Tweetinvi
{
    public static class Message
    {
        [ThreadStatic]
        private static IMessageFactory _messageFactory;
        public static IMessageFactory MessageFactory
        {
            get
            {
                if (_messageFactory == null)
                {
                    Initialize();
                }

                return _messageFactory;
            }
        }

        [ThreadStatic]
        private static IMessageController _messageController;
        public static IMessageController MessageController
        {
            get
            {
                if (_messageController == null)
                {
                    Initialize();
                }

                return _messageController;
            }
        }

        static Message()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _messageFactory = TweetinviContainer.Resolve<IMessageFactory>();
            _messageController = TweetinviContainer.Resolve<IMessageController>();
        }

        public static IMessage GetExistingMessage(long messageId)
        {
            return MessageFactory.GetExistingMessage(messageId);
        }

        public static IMessage CreateMessage(string text, IUser recipient = null)
        {
            return MessageFactory.CreateMessage(text, recipient);
        }

        // Controller
        public static IEnumerable<IMessage> GetLatestMessagesReceived(int maximumMessages = 40)
        {
            return MessageController.GetLatestMessagesReceived(maximumMessages);
        }

        public static IEnumerable<IMessage> GetLatestMessagesSent(int maximumMessages = 40)
        {
            return MessageController.GetLatestMessagesReceived(maximumMessages);
        }

        public static IMessage PublishMessage(IMessage message)
        {
            return MessageController.PublishMessage(message);
        }

        public static IMessage PublishMessage(IMessageDTO messageDTO)
        {
            return MessageController.PublishMessage(messageDTO);
        }

        public static IMessage PublishMessage(string text, IUser targetUser)
        {
            return MessageController.PublishMessage(text, targetUser);
        }

        public static IMessage PublishMessage(string text, IUserIdDTO targetUserDTO)
        {
            return MessageController.PublishMessage(text, targetUserDTO);
        }

        public static IMessage PublishMessage(string text, long targetUserId)
        {
            return MessageController.PublishMessage(text, targetUserId);
        }

        public static IMessage PublishMessage(string text, string targetUserScreenName)
        {
            return MessageController.PublishMessage(text, targetUserScreenName);
        }

        public static bool DestroyMessage(IMessage message)
        {
            return MessageController.DestroyMessage(message);
        }

        public static bool DestroyMessage(IMessageDTO messageDTO)
        {
            return MessageController.DestroyMessage(messageDTO);
        }

        public static bool DestroyMessage(long messageId)
        {
            return MessageController.DestroyMessage(messageId);
        }
    }
}