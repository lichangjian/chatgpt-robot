using Newtonsoft.Json.Linq;

namespace ChatRobot.Services
{
    public class ChatService : IChatService
    {
        private Dictionary<string, IHandler> handlers = new Dictionary<string, IHandler>();
        private ILogger logger;

        public ChatService(ILogger logger)
        {
            Check.IsNotNull(logger, nameof(logger));
            this.logger = logger;
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
            logger.LogInformation("OnRecieve, eventType:" + eventType);
            return this.handlers[eventType].Handle(data);
        }
    }
}
