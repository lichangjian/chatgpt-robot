using ChatRobot.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ChatRobot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService chatService;

        public ChatController(IConfiguration configuration, ILogger<ChatController> logger)
        {
            _logger = logger;
            this.chatService = new ChatServiceFactory().Create(configuration, logger);
        }

        [HttpGet]
        public string Test()
        {
            _logger.LogInformation("Test");
            return "TestSuccess";
        }

        [HttpPost]
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