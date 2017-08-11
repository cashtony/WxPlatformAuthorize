using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class PreAuthCodeResponse : BaseResponse
    {
        /// <summary>
        /// 预授权码
        /// </summary>
        [JsonProperty(PropertyName = "pre_auth_code")]
        public string PreAuthCode { set; get; }
    }
}
