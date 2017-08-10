using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxPlatformAuthorize.WxSDK.Models
{
    public class GetComponentTokenRequest
    {
        public string ComponentAppId { set; get; }
        public string ComponentAppSecret { set; get; }
        public string ComponentVerifyTicket { set; get; }
    }
}
