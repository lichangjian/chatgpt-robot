using Newtonsoft.Json;

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

        private string url = "https://open.feishu.cn/open-apis/authen/v1/access_token";

        private string token;
        private DateTime expiredTime;
        private HttpClient httpClient;
        private string appId;
        private string appSecret;

        public FeishuTokenProvider(string appId, string appSecret)
        {
            Check.IsNotNull(appId, nameof(appId));
            Check.IsNotNull(appSecret, nameof(appSecret));

            this.httpClient = new HttpClient();
            this.appId = appId;
            this.appSecret = appSecret;
        }

        public async Task<string> GetToken()
        {
            if (!IsExpired())
                return token;

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var data = new RequestData() { app_id = appId, app_secret = appSecret };
            request.Content = new StringContent(JsonConvert.SerializeObject(data));
            var rep = await this.httpClient.SendAsync(request);
            var repContent = await rep.Content.ReadAsStringAsync();
            var repData = JsonConvert.DeserializeObject<ResponseData>(repContent);
            if (repData.code != 0)
                throw new Exception("get token failed:" + repData.code + ", msg:" + repData.msg);

            this.expiredTime = DateTime.Now.AddSeconds(repData.expire);
            this.token = repData.tenant_access_token;
            return this.token;
        }

        private bool IsExpired()
        {
            return DateTime.Now > expiredTime;
        }
    }
}
