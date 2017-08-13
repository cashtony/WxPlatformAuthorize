namespace WxPlatformAuthorize.WxSDK
{
    public class WxApiClientConfig
    {
        /// <summary>
        /// 第三方平台AppID
        /// </summary>
        public string ComponentAppId { set; get; }
        /// <summary>
        /// 第三方平台Secret
        /// </summary>
        public string ComponentAppSecret { set; get; }
        /// <summary>
        /// 公众号消息校验Token
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// 公众号消息加解密Key
        /// </summary>
        public string EncodingAESKey { set; get; }
    }
}
