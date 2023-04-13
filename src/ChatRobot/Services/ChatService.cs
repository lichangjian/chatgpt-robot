using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public class ChatService : IChatService
    {
        private readonly string accessToken;
        private Dictionary<string, IHandler> handlers = new Dictionary<string, IHandler>();

        public ChatService(string token)
        {
            this.accessToken = token;
        }

        public void RegisterHandler(string eventType, IHandler handler)
        {
            Check.IsNotNull(eventType, nameof(eventType));
            Check.IsNotNull(handler, nameof(handler));

            handlers[eventType] = handler;
        }

        public bool OnRecieve(JObject data)
        {
            Check.IsNotNull(data, nameof(data));

            var eventType = data["header"]["event_type"].Value<string>();
            return this.handlers[eventType].Handle(data);
        }
    }
}
