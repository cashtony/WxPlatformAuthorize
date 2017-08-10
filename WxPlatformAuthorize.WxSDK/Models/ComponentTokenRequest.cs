using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class ComponentTokenRequest : BaseRequest
    {
        [JsonProperty(PropertyName = "component_appsecret")]
        public string ComponentAppSecret { set; get; }
        [JsonProperty(PropertyName = "component_verify_ticket")]
        public string ComponentVerifyTicket { set; get; }
    }
}
