using System;
using Newtonsoft.Json;
using WxPlatformAuthorize.WxSDK.Models;

namespace WxPlatformAuthorize.WxSDK
{
    public class WxApiClient
    {
        private IHttpClient _http;
        private WxApiClientConfig _config;
        public WxApiClient(IHttpClient http, WxApiClientConfig config)
        {
            _http = http;
            _config = config;
        }
        public string GetComponentToken(string verifyTicket)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";
            var requestBody = JsonConvert.SerializeObject(new ComponentTokenRequest()
            {
                ComponentAppId = _config.ComponentAppId,
                ComponentAppSecret = _config.ComponentAppSecret,
                ComponentVerifyTicket = verifyTicket
            });
            var responseBody = _http.Post(url, requestBody);
            var response = JsonConvert.DeserializeObject<ComponentTokenResponse>(responseBody);

            if (!String.IsNullOrEmpty(response.ErrCode))
            {
                throw new Exception($"{response.ErrCode}:{response.ErrMsg}");
            }
            return response.ComponentAccessToken;
        }

        public string GetPreAuthCode(string accessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={accessToken}";
            var requestBody = JsonConvert.SerializeObject(new PreAuthCodeRequest()
            {
                ComponentAppId = _config.ComponentAppId,
            });
            var responseBody = _http.Post(url, requestBody);
            var response = JsonConvert.DeserializeObject<PreAuthCodeResponse>(responseBody);

            if (!String.IsNullOrEmpty(response.ErrCode))
            {
                throw new Exception($"{response.ErrCode}:{response.ErrMsg}");
            }
            return response.PreAuthCode;
        }
    }
}
