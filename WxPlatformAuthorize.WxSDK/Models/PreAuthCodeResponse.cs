using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class PreAuthCodeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "pre_auth_code")]
        public string PreAuthCode { set; get; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { set; get; }
    }
}
