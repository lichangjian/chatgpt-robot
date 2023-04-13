namespace ChatRobot.Services
{
    public interface IClient
    {
        Task SendMessageAsync(string chatId, string message);
        Task Post(string url, string data);
    }
}
