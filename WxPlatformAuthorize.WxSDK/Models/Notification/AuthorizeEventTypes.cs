namespace WxPlatformAuthorize.WxSDK.Models.Notification
{
    public sealed class AuthorizeEventTypes
    {
        /// <summary>
        /// 推送component_verify_ticket
        /// </summary>
        public const string ComponentVerifyTicket = "component_verify_ticket";

        /// <summary>
        /// 授权成功
        /// </summary>
        public const string Autorized = "authorized";

        /// <summary>
        /// 取消授权
        /// </summary>
        public const string Unauthorized = "unauthorized";

        /// <summary>
        /// 更新授权
        /// </summary>
        public const string UpdateAutorized = "updateauthorized";
    }
}
