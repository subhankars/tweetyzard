﻿using System.Collections.Generic;
using System.Linq;
using TweetinviCore;
using TweetinviCore.Helpers;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.DTO;
using TweetinviCore.Interfaces.Factories;

namespace TweetinviFactories.Direct_Messages
{
    public class MessageFactory : IMessageFactory
    {
        private readonly IMessageFactoryQueryExecutor _messageFactoryQueryExecutor;
        private readonly IUnityFactory<IMessage> _messageUnityFactory;
        private readonly IJsonObjectConverter _jsonObjectConverter;

        public MessageFactory(
            IMessageFactoryQueryExecutor messageFactoryQueryExecutor,
            IUnityFactory<IMessage> messageUnityFactory,
            IJsonObjectConverter jsonObjectConverter)
        {
            _messageFactoryQueryExecutor = messageFactoryQueryExecutor;
            _messageUnityFactory = messageUnityFactory;
            _jsonObjectConverter = jsonObjectConverter;
        }

        // Get existing message
        public IMessage GetExistingMessage(long messageId)
        {
            var messageDTO = _messageFactoryQueryExecutor.GetExistingMessage(messageId);
            return GenerateMessageFromMessageDTO(messageDTO);
        }
        
        // Create Message
        public IMessage CreateMessage(string text, IUser recipient = null)
        {
            var messageDTO = _messageFactoryQueryExecutor.CreateMessage(text, recipient != null ? recipient.UserDTO : null);
            return GenerateMessageFromMessageDTO(messageDTO);
        }

        // Generate Message from DTO
        public IMessage GenerateMessageFromMessageDTO(IMessageDTO messageDTO)
        {
            var messageParameter = _messageUnityFactory.GenerateParameterOverrideWrapper("messageDTO", messageDTO);
            return _messageUnityFactory.Create(messageParameter);
        }

        public IEnumerable<IMessage> GenerateMessagesFromMessagesDTO(IEnumerable<IMessageDTO> messagesDTO)
        {
            if (messagesDTO == null)
            {
                return null;
            }

            return messagesDTO.Select(GenerateMessageFromMessageDTO);
        }

        // Generate Message from Json
        public IMessage GenerateMessageFromJson(string jsonMessage)
        {
            var messageDTO = _jsonObjectConverter.DeserializeObject<IMessageDTO>(jsonMessage);
            if (messageDTO.Id == TweetinviConstants.DEFAULT_ID)
            {
                return null;
            }

            return GenerateMessageFromMessageDTO(messageDTO);
        }
    }
}