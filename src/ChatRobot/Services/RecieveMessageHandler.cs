﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public class RecieveMessageHandler : IHandler
    {
        private IChatGPTService chatGPTService;
        private IClient client;
        private ILogger logger;

        public RecieveMessageHandler(IClient client, IChatGPTService chatGPTService, ILogger logger)
        {
            Check.IsNotNull(chatGPTService, nameof(chatGPTService));
            Check.IsNotNull(client, nameof(client));
            this.logger = logger;
            this.client = client;
            this.chatGPTService = chatGPTService;
        }

        class MessageContent
        {
            public string text;
        }

        public bool Handle(JObject data)
        {
            var chatId = data["event"]["message"]["chat_id"].Value<string>();
            var message = data["event"]["message"]["content"].Value<string>();

            logger.LogInformation("RecieveMessageHandler, chatId:" + chatId + ", message:" + message);
            var messageContent = JsonConvert.DeserializeObject<MessageContent>(message);
            Send(chatId, messageContent.text);
            return true;
        }

        private async void Send(string chatId, string message)
        {
            var rep = await this.chatGPTService.Send(chatId, message);
            await client.SendChatMessage(chatId, rep);
        }
    }
}