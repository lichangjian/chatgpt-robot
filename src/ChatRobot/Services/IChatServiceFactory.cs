namespace ChatRobot.Services
{
    public interface IChatServiceFactory
    {
        IChatService Create(IConfigurationRoot configuration);
    }
}
