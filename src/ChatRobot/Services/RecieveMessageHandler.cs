using Newtonsoft.Json;
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
            messageContent.text = messageContent.text.Replace("@_user_1", "");
            messageContent.text = messageContent.text.Trim();
            Send(chatId, messageContent.text);
            return true;
        }

        private async void Send(string chatId, string message)
        {
            var rep = await this.chatGPTService.Send(chatId, message);
            var feiShuRep = await client.SendChatMessage(chatId, rep);
            logger.LogInformation("send code:" + feiShuRep.code + ", msg:" + feiShuRep.msg);
        }
    }
}
