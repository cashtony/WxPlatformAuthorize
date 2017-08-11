using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class BaseResponse
    {
        /// <summary>
        /// 有效期，单位秒
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { set; get; }
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public string ErrCode { set; get; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { set; get; }
    }
}
