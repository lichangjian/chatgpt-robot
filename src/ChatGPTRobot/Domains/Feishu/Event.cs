using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTRobot.Domains.Feishu
{
    public class EventHeader
    {
        public string event_id { get; set; }
        public string token { get; set; }
        public string create_time { get; set; }
        public string event_type { get; set; }
        public string tenant_key { get; set; }
        public string app_id { get; set; }
    }

    public class Event
    {
        public string schema { get; set; }
        public EventHeader header { get; set; }
    }
}
