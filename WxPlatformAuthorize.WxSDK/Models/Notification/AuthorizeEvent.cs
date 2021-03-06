﻿using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WxPlatformAuthorize.WxSDK.Models.Notification
{
    [XmlRoot("xml")]
    public class AuthorizeEvent
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        [DataMember]
        public string AppId { set; get; }
        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember]
        public long CreateTime { set; get; }
        /// <summary>
        /// 事件类型，取值参考 AuthorizeEventTypes
        /// </summary>
        [DataMember]
        public string InfoType { set; get; }
        /// <summary>
        /// Ticket内容
        /// </summary>
        [DataMember]
        public string ComponentVerifyTicket { set; get; }
        /// <summary>
        /// 公众号或小程序
        /// </summary>
        [DataMember]
        public string AuthorizerAppid { set; get; }
        /// <summary>
        /// 授权码，可用于换取公众号的接口调用凭据
        /// </summary>
        [DataMember]
        public string AuthorizationCode { set; get; }
        /// <summary>
        /// 授权码过期时间
        /// </summary>
        [DataMember]
        public string AuthorizationCodeExpiredTime { set; get; }
    }
}
