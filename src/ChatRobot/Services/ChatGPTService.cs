using OpenAI_API;

namespace ChatRobot.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private OpenAIAPI api;

        public ChatGPTService(string secret)
        {
            Check.IsNotNull(secret, nameof(secret));
            this.api = new OpenAIAPI(secret);
        }

        public async Task<string> Send(string chat, string message)
        {
            return await this.api.Completions.GetCompletion(message);
        }
    }
}
