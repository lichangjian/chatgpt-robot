using OpenAI_API;

namespace ChatRobot.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private OpenAIAPI api;
        private ILogger logger;

        public ChatGPTService(string secret, ILogger loggger)
        {
            Check.IsNotNull(secret, nameof(secret));
            this.api = new OpenAIAPI(secret);
        }

        public async Task<string> Send(string chat, string message)
        {
            this.logger.LogInformation("sendMessage to ai:" + message);
            var result = await this.api.Completions.GetCompletion(message);
            this.logger.LogInformation("sendMessage to ai response:" + result);
            return result;
        }
    }
}
