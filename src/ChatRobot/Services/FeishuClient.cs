using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ChatRobot.Services
{
    class SendRequest
    {
        public string receive_id;
        public string msg_type;
        public string content;
    }

    public class SendResponse
    {
        public int code;
        public string msg;
    }

    public class MessageContent
    {
        public string text;
    }

    public class FeishuClient : IClient
    {
        private HttpClient client;
        private ITokenProvider tokenProvider;
        private ILogger logger;
        private string sendUrl = "https://open.feishu.cn/open-apis/im/v1/messages?receive_id_type=chat_id";

        public FeishuClient(ITokenProvider tokenProvider, ILogger logger)
        {
            this.logger = logger;
            this.client = new HttpClient();
            this.tokenProvider = tokenProvider;
        }

        public async Task<HttpResponseMessage> Post(string url, string data)
        {
            var token = await tokenProvider.GetToken();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Content = new StringContent(data);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            logger.LogInformation("Post, url:" + url + ", data:" + data);
            var rep = await client.SendAsync(request);
            return rep;
        }

        public async Task<SendResponse> SendChatMessage(string chatId, string message)
        {
            var sendRequest = new SendRequest();
            sendRequest.receive_id = chatId;
            sendRequest.msg_type = "text";
            var content = new MessageContent();
            content.text = message;
            sendRequest.content = JsonConvert.SerializeObject(content);
            var result = JsonConvert.SerializeObject(sendRequest);

            var rep = await Post(this.sendUrl, result);
            return await rep.Content.ReadFromJsonAsync<SendResponse>();
        }
    }
}
