namespace ChatRobot.Services
{
    public interface IClient
    {
        Task<HttpResponseMessage> Post(string url, string data);
        Task<SendResponse> SendChatMessage(string chatId, string message);
    }
}
