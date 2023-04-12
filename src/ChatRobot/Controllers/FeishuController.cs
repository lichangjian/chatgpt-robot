using ChatRobot.Controllers.Feishu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ChatRobot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeishuController : ControllerBase
    {
        private readonly ILogger<FeishuController> _logger;
        private readonly FeishuService feishuService;
        private readonly ChatGPTService chatGPTService;

        public FeishuController(ILogger<FeishuController> logger)
        {
            _logger = logger;
            var token = "";
            this.feishuService = new FeishuService(token);
            this.chatGPTService = new ChatGPTService();
        }

        [HttpPost(Name = "Chat")]
        public async Task<HttpResponseMessage> PostChat([FromBody]JObject parameter)
        {
            
        }
    }
}