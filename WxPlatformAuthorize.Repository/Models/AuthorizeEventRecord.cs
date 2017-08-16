using System;

namespace WxPlatformAuthorize.Repository.Models
{
    public class AuthorizeEventRecord
    {
        public int Id { set; get; }
        public string EventType { set; get; }
        public string EventXml { set; get; }
        public DateTime CreateTime { set; get; }
    }
}
