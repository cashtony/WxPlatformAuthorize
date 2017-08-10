using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxPlatformAuthorize.WxSDK.Models
{
    public class GetPreAuthCodeResponse
    {
        public string PreAuthCode { set; get; }
        public int ExpiresIn { set; get; }
    }
}
