namespace ChatRobot.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private string secret;

        public ChatGPTService(string secret)
        {
            Check.IsNotNull(secret, nameof(secret));
            this.secret = secret;
        }

        public Task<string> Send(string chat, string message)
        {
            return Task.FromResult(message);
        }
    }
}
