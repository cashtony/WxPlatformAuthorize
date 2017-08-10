using System.Net.Http;
using System.Text;

namespace WxPlatformAuthorize.WxSDK
{
    public class HttpClient : IHttpClient
    {
        public string Post(string url, string requestBody)
        {
            var client = new System.Net.Http.HttpClient();
            var responseContent = client.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, "application/json")).Result.Content;
            var responseBody = responseContent.ReadAsStringAsync().Result;
            return responseBody;
        }
    }
}
