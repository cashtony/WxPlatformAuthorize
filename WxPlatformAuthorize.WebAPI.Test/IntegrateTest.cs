using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Test
{
    [TestClass]
    public class IntegrateTest : IntegrateTestBase
    {
        private const string BaseAddress = "http://localhost:57212";
        private const string VerifyTicket = "TestComponentVerifyTicketValue";

        private const string VerifyTicketXML = "<xml><Encrypt><![CDATA[iB3zzHBwe3zi4K0YdScP3GQIUWOOfyIJlNWS+p0w1/z9NIRRWQvzZW2bm3Fot6xrPKu90OA9ZLRR0/NGao67zl0I3HyQrQFsGpZUJ7iM7RDYn3xqdt+gqDAIXEDYA78bFv6U5afh/rD+b3fEQaSvHaQ0Mn7jHefmE/9DpTzbfgrzxAfj0oAasX09ajnnHz8nmlBA88EfrFVGMLkLFYfs4g0JO5jKBrBTVroOTSxri8P1fO3yHJDLKRGmFt17dVZw4DbncSvyv/eJnnsds4jK/KauWyUjTFXCoJxvoM0yzFipEcFnm2mPGTaChiy3qv5e4CcE+Vu0kAFo/ONFShZECWjxBoy0ZyG9Zq7YG4iZKAAgOCsjHAEjsRKTX20KUCVs]]></Encrypt><MsgSignature><![CDATA[cea132944756ef40abccd2113c318cb9032d5db9]]></MsgSignature><TimeStamp><![CDATA[1409659813]]></TimeStamp><Nonce><![CDATA[1372623149]]></Nonce></xml>";
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
                    var obj = JsonConvert.DeserializeObject(body) as JObject;
                    if (!obj.GetValue("component_verify_ticket").ToString().Equals(VerifyTicket))
                    {
                        Assert.Fail("错误的component_verify_ticket");
                    }
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
                    var obj = JsonConvert.DeserializeObject(body) as JObject;
                    if (!obj.GetValue("component_verify_ticket").ToString().Equals(VerifyTicket))
                    {
                        Assert.Fail("错误的component_verify_ticket");
                    }
                    return
@"{
""component_access_token"":""61W3mEpU66027wgNZ_MhGHNQDHnFATkDa9-2llqrMBjUwxRSNPbVsMmyD-yq8wZETSoE5NQgecigDrSHkPtIYA"", 
""expires_in"":7200
}";
                }
                else if (url.StartsWith("https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode"))
                {
                    var uri = new Uri(url);
                    if (!uri.ParseQueryString()["component_access_token"].Equals("61W3mEpU66027wgNZ_MhGHNQDHnFATkDa9-2llqrMBjUwxRSNPbVsMmyD-yq8wZETSoE5NQgecigDrSHkPtIYA"))
                    {
                        Assert.Fail("错误的component_access_token");
                    }
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
                RequestUri = new Uri($"{BaseAddress}/api/notify/event?msg_signature=cea132944756ef40abccd2113c318cb9032d5db9&timestamp=1409659813&nonce=1372623149"),
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
