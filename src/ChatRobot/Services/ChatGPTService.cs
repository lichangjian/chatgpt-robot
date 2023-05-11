
using Rystem.OpenAi;
using Rystem.OpenAi.Chat;

namespace ChatRobot.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private IOpenAiChat openAiApi;
        private ILogger logger;

        public ChatGPTService(string secret, ILogger loggger)
        {
            Check.IsNotNull(secret, nameof(secret));
            this.logger = loggger;
            OpenAiService.Instance.AddOpenAi(setting => setting.ApiKey = secret, "NoDI");
            openAiApi = OpenAiService.Factory.CreateChat("chat");
            Check.IsNotNull(openAiApi, "openAIAPI");
        }

        public async Task<string> Send(string chat, string message)
        {
            this.logger.LogInformation("sendMessage to ai:" + message);
            var results = await openAiApi
                .Request(new ChatMessage { Role = ChatRole.User, Content = message })
                .WithModel(ChatModelType.Gpt35Turbo)
                .WithTemperature(1)
                .ExecuteAsync();

            var content = results.Choices[0].Message.Content;
            this.logger.LogInformation("sendMessage to ai response:" + content);
            return content;
        }
    }
}
