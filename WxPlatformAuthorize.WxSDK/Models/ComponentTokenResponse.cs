using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class ComponentTokenResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "component_access_token")]
        public string ComponentAccessToken { set; get; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { set; get; }
    }
}
