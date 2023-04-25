using Newtonsoft.Json;

namespace ChatRobot.Services
{
    class SendRequest
    {
        public string receive_id;
        public string msg_type;
        public string content;
    }

    public class FeishuClient : IClient
    {
        private HttpClient client;
        private ITokenProvider tokenProvider;
        private ILogger logger;

        public FeishuClient(ITokenProvider tokenProvider, ILogger logger)
        {
            this.logger = logger;
            this.client = new HttpClient();
            this.tokenProvider = tokenProvider;
        }

        public async Task Post(string url, string data)
        {
            var token = await tokenProvider.GetToken();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Content-Type", "application/json; charset=utf-8");
            request.Headers.Add("Authorization:Bearer", token);
            request.Content = new StringContent(data);
            logger.LogInformation("Post, url:" + url + ", data:" + data);
            await client.SendAsync(request);
        }

        public async Task SendMessageAsync(string chatId, string message)
        {
            var sendRequest = new SendRequest();
            sendRequest.receive_id = chatId;
            sendRequest.msg_type = "text";
            sendRequest.content = $"{{\"text\":\"{message}\"}}";
            var content = new StringContent(JsonConvert.SerializeObject(sendRequest));
            var url = "";
            var rep = await this.client.PostAsync(url, content);
            rep.EnsureSuccessStatusCode();
        }
    }
}
