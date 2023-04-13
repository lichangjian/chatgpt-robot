using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public interface IChatService
    {
        bool OnRecieve(JObject data);
    }
}
