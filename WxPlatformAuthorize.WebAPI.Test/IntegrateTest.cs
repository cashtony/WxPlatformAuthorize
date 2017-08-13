using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Test
{
    [TestClass]
    public class IntegrateTest : IntegrateTestBase
    {
        private const string BaseAddress = "http://localhost:57212";
        private const string VerifyTicket = "_TestComponentVerifyTicket_";
        private const string VerifyTicketXMLTemplate =
@"<xml>
<AppId></AppId>
<CreateTime>1413192605 </CreateTime>
<InfoType>component_verify_ticket</InfoType>
<ComponentVerifyTicket>{0}</ComponentVerifyTicket>
</xml>";

        private readonly string VerifyTicketXML = String.Format(VerifyTicketXMLTemplate, VerifyTicket);
        [TestMethod]
        public void TestUpdateVerifyTicket()
        {
            //Arrange
            //Act
            var response = UpdateVerifyTicket(VerifyTicketXML);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual("success", responseContent);
            response.Dispose();

        }
        [TestMethod]
        public void TestGetAccessToken()
        {
            //Arrange
            OnMockWxServerRequest = (url, body) => {
                if (url.StartsWith("https://api.weixin.qq.com/cgi-bin/component/api_component_token"))
                {
                    return
@"{
""component_access_token"":""61W3mEpU66027wgNZ_MhGHNQDHnFATkDa9-2llqrMBjUwxRSNPbVsMmyD-yq8wZETSoE5NQgecigDrSHkPtIYA"", 
""expires_in"":7200
}";
                }
                throw new Exception();
            };
            //Act
            UpdateVerifyTicket(VerifyTicketXML);
            var response = GetAccessToken();
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(responseContent.Success);
            Assert.AreEqual("61W3mEpU66027wgNZ_MhGHNQDHnFATkDa9-2llqrMBjUwxRSNPbVsMmyD-yq8wZETSoE5NQgecigDrSHkPtIYA", responseContent.AccessToken);
        }

        [TestMethod]
        public void TestGetPreAuthCode()
        {
            //Arrange
            OnMockWxServerRequest = (url, body) => {
                if(url.StartsWith("https://api.weixin.qq.com/cgi-bin/component/api_component_token"))
                {
                    return
@"{
""component_access_token"":""61W3mEpU66027wgNZ_MhGHNQDHnFATkDa9-2llqrMBjUwxRSNPbVsMmyD-yq8wZETSoE5NQgecigDrSHkPtIYA"", 
""expires_in"":7200
}";
                }
                else if (url.StartsWith("https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode"))
                {
                    return
@"{
""pre_auth_code"":""Cx_Dk6qiBE0Dmx4EmlT3oRfArPvwSQ-oa3NL_fwHM7VI08r52wazoZX2Rhpz1dEw"",
""expires_in"":600
}";
                }
                throw new Exception();
            };
            //Act
            UpdateVerifyTicket(VerifyTicketXML);
            var response = GetPreAuthCode();
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JsonConvert.DeserializeObject<PreAuthCodeResponse>(response.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(responseContent.Success);
            Assert.AreEqual("Cx_Dk6qiBE0Dmx4EmlT3oRfArPvwSQ-oa3NL_fwHM7VI08r52wazoZX2Rhpz1dEw", responseContent.PreAuthCode);
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

        private HttpResponseMessage GetAccessToken()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{BaseAddress}/api/authorize/access_token"),
                Method = HttpMethod.Get,
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
