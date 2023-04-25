namespace ChatRobot.Services
{
    public interface IClient
    {
        Task Post(string url, string data);
    }
}
