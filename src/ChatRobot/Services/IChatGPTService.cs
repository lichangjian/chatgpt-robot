namespace ChatRobot.Services
{
    public interface IChatGPTService
    {
        Task<string> Send(string chatId, string message);
    }
}
