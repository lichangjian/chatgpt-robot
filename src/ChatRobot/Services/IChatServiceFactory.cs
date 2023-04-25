namespace ChatRobot.Services
{
    public interface IChatServiceFactory
    {
        IChatService Create(IConfiguration configuration, ILogger logger);
    }
}
