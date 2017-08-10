using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class BaseRequest
    {
        [JsonProperty(PropertyName = "component_appid")]
        public string ComponentAppId { set; get; }
    }
}
