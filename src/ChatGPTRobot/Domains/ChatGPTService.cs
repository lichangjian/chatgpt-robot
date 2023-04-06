
namespace ChatGPTRobot.Domains
{
    internal class ChatGPTService : IChatGPTService
    {
        private OpenAI_API.OpenAIAPI api;
        public ChatGPTService(string apiKey)
        {
            api = new OpenAI_API.OpenAIAPI(apiKey);
        }

        public async Task<string> Send(string message)
        {
            return await api.Completions.GetCompletion(message);
        }
    }
}