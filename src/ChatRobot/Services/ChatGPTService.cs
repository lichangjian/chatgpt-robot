

using OpenAI_API;
using OpenAI_API.Chat;

namespace ChatRobot.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private IOpenAIAPI openAiApi;
        private ILogger logger;
        private Conversation conversation;

        public ChatGPTService(string secret, ILogger loggger)
        {
            Check.IsNotNull(secret, nameof(secret));
            this.logger = loggger;
            openAiApi = new OpenAIAPI(secret);
            conversation = openAiApi.Chat.CreateConversation();
        }

        public async Task<string> Send(string chat, string message)
        {
            this.logger.LogInformation("sendMessage to ai:" + message);

            conversation.AppendUserInput(message);
            var response = await conversation.GetResponseFromChatbotAsync();
            this.logger.LogInformation("sendMessage to ai response:" + response);
            return response;
        }
    }
}
