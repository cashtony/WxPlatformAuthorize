using Newtonsoft.Json;

namespace WxPlatformAuthorize.WxSDK.Models
{
    internal class BaseResponse
    {
        [JsonProperty(PropertyName = "errcode")]
        public string ErrCode { set; get; }
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { set; get; }
    }
}
