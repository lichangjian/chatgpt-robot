using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public interface IHandler
    {
        bool Handle(JObject data);
    }
}
