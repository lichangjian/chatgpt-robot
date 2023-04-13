using ChatRobot.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ChatRobot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeishuController : ControllerBase
    {
        private readonly ILogger<FeishuController> _logger;
        private readonly IChatService chatService;

        public FeishuController(IConfigurationRoot configuration, ILogger<FeishuController> logger)
        {
            _logger = logger;
            this.chatService = new ChatServiceFactory().Create(configuration);
        }

        [HttpPost(Name = "Chat")]
        public HttpResponseMessage Chat([FromBody]JObject parameter)
        {
            var succ = this.chatService.OnRecieve(parameter);
            if (succ)
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}