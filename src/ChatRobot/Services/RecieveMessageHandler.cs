﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public class RecieveMessageHandler : IHandler
    {
        private IChatGPTService chatGPTService;
        private IClient client;
        private string sendUrl = "https://open.feishu.cn/open-apis/im/v1/messages";

        public RecieveMessageHandler(IClient client, IChatGPTService chatGPTService)
        {
            Check.IsNotNull(chatGPTService, nameof(chatGPTService));
            Check.IsNotNull(client, nameof(client));

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
            var messageContent = JsonConvert.DeserializeObject<MessageContent>(message);
            Send(chatId, messageContent.text);
            return true;
        }

        private async void Send(string chatId, string message)
        {
            var rep = await this.chatGPTService.Send(chatId, message);

            var sendRequest = new SendRequest();
            sendRequest.receive_id = chatId;
            sendRequest.msg_type = "text";
            sendRequest.content = $"{{\"text\":\"{message}\"}}";
            var content = JsonConvert.SerializeObject(sendRequest);

            await client.Post(this.sendUrl, content);
        }
    }
}
