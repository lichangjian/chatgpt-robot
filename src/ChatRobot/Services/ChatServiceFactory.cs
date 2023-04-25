namespace ChatRobot.Services
{
    public class ChatServiceFactory : IChatServiceFactory
    {
        public IChatService Create(IConfiguration configuration, ILogger logger)
        {
            var chatService = new ChatService(logger);

            var chatGPTService = new ChatGPTService(configuration["openai_secret"]);

            var tokenProvider = new FeishuTokenProvider(configuration["appId"], configuration["appSecret"], logger);
            var client = new FeishuClient(tokenProvider, logger);
            var recieveMessageHandler = new RecieveMessageHandler(client, chatGPTService, logger);

            chatService.RegisterHandler("im.message.receive_v1", recieveMessageHandler);

            return chatService;
        }
    }
}
