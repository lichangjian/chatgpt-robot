using ChatRobot.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ChatRobot.Tests
{
    public class TokenProviderTests
    {
        private ITokenProvider tokenProvider;

        [SetUp]
        public void Setup()
        {
            var log = Mock.Of<ILogger>();
            tokenProvider = new FeishuTokenProvider("a", "a", log);
        }

        [Test]
        public async Task GetToken()
        {
            var token = await tokenProvider.GetToken();
            Console.WriteLine("token:" + token);
            Assert.Pass();
        }

        [Test]
        public async Task TestSendChatMessage()
        {
            var client = new FeishuClient(this.tokenProvider, Mock.Of<ILogger>());
            var rep = await client.SendChatMessage("1", "test");
            Console.WriteLine("code:" + rep.code + ", msg:" + rep.msg);
            Assert.Pass();
        }
    }
}