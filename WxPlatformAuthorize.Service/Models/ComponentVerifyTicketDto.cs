using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WxPlatformAuthorize.Service.Models
{
    [XmlRoot("xml")]
    public class ComponentVerifyTicketDto
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
        public string CreateTime { set; get; }
        /// <summary>
        /// component_verify_ticket
        /// </summary>
        [DataMember]
        public string InfoType { set; get; }
        /// <summary>
        /// Ticket内容
        /// </summary>
        [DataMember]
        public string ComponentVerifyTicket { set; get; }
    }
}
