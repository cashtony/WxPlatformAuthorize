using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WxPlatformAuthorize.Service.Models;

namespace WxPlatformAuthorize.WebAPI.Test
{
    [TestClass]
    public class UnitTest1: InMemoryHttpTest
    {
        private const string BaseAddress = "http://localhost:57212";
        [TestMethod]
        public void TestMethod1()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddress}/api/dev/test");
            using (HttpResponseMessage response = SendInMemoryHttpRequest(request))
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Assert.IsFalse(String.IsNullOrEmpty(content));
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{BaseAddress}/api/notify/UpdateComponentVerifyTicket");
            request.Content = new StringContent(@"<xml>
<AppId> </AppId>
<CreateTime>1413192605 </CreateTime>
<InfoType> </InfoType>
<ComponentVerifyTicket> </ComponentVerifyTicket>
</xml>", Encoding.UTF8, "application/xml");

            //request.Content = new ObjectContent<ComponentVerifyTicketDto>(new ComponentVerifyTicketDto()
            //{
            //    AppId = "asddfssdc",
            //    CreateTime = "1413192605"
            //}, new XmlMediaTypeFormatter());
            using (HttpResponseMessage response = SendInMemoryHttpRequest(request))
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Assert.IsFalse(String.IsNullOrEmpty(content));
            }
        }
    }
}
