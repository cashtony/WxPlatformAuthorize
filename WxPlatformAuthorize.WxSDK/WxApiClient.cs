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

        public string DecryptMessage(string msgSignature, string timeStamp, string nonce, string data)
        {
            string msg = "";
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(_config.Token, _config.EncodingAESKey, _config.ComponentAppId);
            int ret = wxcpt.DecryptMsg(msgSignature, timeStamp, nonce, data, ref msg);
            if(ret != 0)
            {
                throw new Exception($"{nameof(WxApiClient)}.{nameof(DecryptMessage)} ERR: Decrypt fail, ret: {ret}");
            }
            return msg;
        }

        public ComponentTokenResponse GetComponentToken(string verifyTicket)
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

            if (response.HasError)
            {
                throw new Exception($"{response.ErrCode}:{response.ErrMsg}");
            }
            return response;
        }

        public PreAuthCodeResponse GetPreAuthCode(string accessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={accessToken}";
            var requestBody = JsonConvert.SerializeObject(new PreAuthCodeRequest()
            {
                ComponentAppId = _config.ComponentAppId,
            });
            var responseBody = _http.Post(url, requestBody);
            var response = JsonConvert.DeserializeObject<PreAuthCodeResponse>(responseBody);

            if (response.HasError)
            {
                throw new Exception($"{response.ErrCode}:{response.ErrMsg}");
            }
            return response;
        }
    }
}
