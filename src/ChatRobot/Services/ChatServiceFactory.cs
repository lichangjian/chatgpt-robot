namespace ChatRobot.Services
{
    public class ChatServiceFactory : IChatServiceFactory
    {
        public IChatService Create(IConfigurationRoot configuration)
        {
            var chatService = new ChatService("");

            var chatGPTService = new ChatGPTService(configuration["openai_secret"]);

            var tokenProvider = new FeishuTokenProvider(configuration["appId"], configuration["appSecret"]);
            var client = new FeishuClient(tokenProvider);
            var recieveMessageHandler = new RecieveMessageHandler(client, chatGPTService);

            chatService.RegisterHandler("im.message.receive_v1", recieveMessageHandler);

            return chatService;
        }
    }
}
