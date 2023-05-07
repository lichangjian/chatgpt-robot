using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ChatRobot.Services
{
    public class FeishuTokenProvider : ITokenProvider
    {
        class RequestData
        {
            public string app_id;
            public string app_secret;
        }

        class ResponseData
        {
            public int code;
            public string msg;
            public string tenant_access_token;
            public int expire;
        }

        private string url = "https://open.feishu.cn/open-apis/auth/v3/app_access_token/internal";

        private string token;
        private DateTime expiredTime;
        private HttpClient httpClient;
        private string appId;
        private string appSecret;
        private ILogger logger;

        public FeishuTokenProvider(string appId, string appSecret, ILogger logger)
        {
            Check.IsNotNull(appId, nameof(appId));
            Check.IsNotNull(appSecret, nameof(appSecret));
            Check.IsNotNull(logger, nameof(logger));

            this.logger = logger;
            this.httpClient = new HttpClient();
            this.appId = appId;
            this.appSecret = appSecret;

            this.logger.LogInformation("appId:" + appId + ", secret:" + appSecret);
        }

        public async Task<string> GetToken()
        {
            if (!IsExpired())
                return token;

            logger.LogInformation("GetToken");

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var data = new RequestData() { app_id = appId, app_secret = appSecret };
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var rep = await this.httpClient.SendAsync(request);
            var repContent = await rep.Content.ReadAsStringAsync();
            var repData = JsonConvert.DeserializeObject<ResponseData>(repContent);
            if (repData.code != 0)
                throw new Exception("get token failed:" + repData.code + ", msg:" + repData.msg);

            this.expiredTime = DateTime.Now.AddSeconds(repData.expire);
            this.token = repData.tenant_access_token;
            logger.LogInformation("GetToken success:" + this.token);

            return this.token;
        }

        private bool IsExpired()
        {
            return DateTime.Now > expiredTime;
        }
    }
}
