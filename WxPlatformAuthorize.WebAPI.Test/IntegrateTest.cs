using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Test
{
    [TestClass]
    public class IntegrateTest : IntegrateTestBase
    {
        private const string BaseAddress = "http://localhost:57212";

        [TestMethod]
        public void TestGetPreAuthCode()
        {
            //Arrange
            var verifyTicket = @"<xml>
<AppId></AppId>
<CreateTime>1413192605 </CreateTime>
<InfoType>component_verify_ticket</InfoType>
<ComponentVerifyTicket>_TestComponentVerifyTicket_</ComponentVerifyTicket>
</xml>";

            OnMockWxServerRequest = (url, body) => {
                throw new Exception();
            };
            //Act
            UpdateVerifyTicket(verifyTicket);
            GetPreAuthCode();
            //Assert
        }

        [TestMethod]
        public void TestGetPreAuthCodeWithoutVerifyTicket()
        {
            //Arrange
            //Act
            var response = GetPreAuthCode();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(responseContent.Contains("未收到component_verify_ticket推送，10分钟后再试"));

            response.Dispose();
        }

        private HttpResponseMessage UpdateVerifyTicket(string verifyTicket)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{BaseAddress}/api/notify/event"),
                Method = HttpMethod.Post,
                Content = new StringContent(verifyTicket, Encoding.UTF8, "application/xml")
            };
            return SendInMemoryHttpRequest(request);
        }

        private HttpResponseMessage GetPreAuthCode()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{BaseAddress}/api/authorize/pre_auth_code"),
                Method = HttpMethod.Get,
            };
            return SendInMemoryHttpRequest(request);
        }
    }
}
