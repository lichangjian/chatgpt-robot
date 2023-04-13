namespace ChatRobot.Services
{
    public interface ITokenProvider
    {
        Task<string> GetToken();
    }
}
