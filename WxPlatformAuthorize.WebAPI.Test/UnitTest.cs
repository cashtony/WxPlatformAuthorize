using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WxPlatformAuthorize.WebAPI.Test
{
    [TestClass]
    public class UnitTest: UnitTestBase
    {
        private const string BaseAddress = "http://localhost:57212";

        [TestMethod]
        public void TestGetPreAuthCode()
        {
            //Arrange
            var verifyTicket = @"<xml>
<AppId></AppId>
<CreateTime>1413192605 </CreateTime>
<InfoType> </InfoType>
<ComponentVerifyTicket>_TestComponentVerifyTicket_</ComponentVerifyTicket>
</xml>";

            OnMockWxServerRequest = (url, body) => {
                throw new System.Exception();
            };
            //Act
            UpdateVerifyTicket(verifyTicket);
            GetPreAuthCode();
            //Assert
        }

        private void UpdateVerifyTicket(string verifyTicket)
        {
            var url = $"{BaseAddress}/api/notify/component_verify_ticket";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(verifyTicket, Encoding.UTF8, "application/xml")
            };

            using (HttpResponseMessage response = SendInMemoryHttpRequest(request))
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            }
        }

        private void GetPreAuthCode()
        {
            var url = $"{BaseAddress}/api/authorize/pre_auth_code";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            using (HttpResponseMessage response = SendInMemoryHttpRequest(request))
            {
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            }
        }
    }
}
