using Newtonsoft.Json.Linq;

namespace ChatRobot.Controllers.Feishu
{
    public enum EventType
    {
        RecieveChat
    }

    public class ChatEventData : EventData
    {
        public string ChatId;
        public string Content;
    }

    public class Event
    {
        public EventType EventType;
        public object Data;
    }

    public class FeishuService
    {
        private readonly HttpClient client;
        private readonly string accessToken;

        public FeishuService(string token)
        {
            this.accessToken = token;
            client = new HttpClient();
        }

        public Event Parse(JObject data)
        {

        }

        public async Task SendMessageAsync(string message)
        {
            var content = new StringContent($"{{\"text\":\"{message}\"}}");
            var url = "";
            var rep = await this.client.PostAsync(url, content);
            rep.EnsureSuccessStatusCode();
        }
    }
}
