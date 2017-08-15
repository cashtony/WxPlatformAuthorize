using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    public class ComponentTokenResponse : BaseResponse
    {
        /// <summary>
        /// 第三方平台access_token
        /// </summary>
        [JsonProperty(PropertyName = "component_access_token")]
        public string ComponentAccessToken { set; get; }
    }
}
