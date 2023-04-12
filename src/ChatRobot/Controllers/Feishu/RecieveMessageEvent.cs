using System.Text.Json.Serialization;

namespace ChatGPTRobot.Domains.Feishu
{
    // https://open.feishu.cn/open-apis/im/v1/messages
    public class SendMessage
    {
        public string receive_id_type;  // chat_id
        public string msg_type = "text";
        public string content; // "{\"text\":\"test content\"}"
    }

    // Authorization

    public class EventSender
    {
        public UserId sender_id { get; set; }
        public string sender_type { get; set; }
        public string tenant_key { get; set; }
    }

    public class UserId
    {
        public string union_id { get; set; }
        public string user_id { get; set; }
        public string open_id { get; set; }
    }

    public class EventMessage
    {
        public string message_id { get; set; }
        public string root_id { get; set; }
        public string parent_id { get; set; }
        public string chat_id { get; set; }
        public string message_type { get; set; }
        public string content { get; set; }
    }

    public class RecieveMessageEventData
    {
        public EventSender sender { get; set; }
        public EventMessage message { get; set; }
    }

    public class RecieveMessageEvent : Event
    {
        [JsonPropertyName("event")]
        public RecieveMessageEventData eventData { get; set; }
    }
}
