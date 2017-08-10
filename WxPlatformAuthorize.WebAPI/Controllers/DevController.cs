using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    public class DevController : ApiController
    {
        [HttpGet]
        public string Test()
        {
            return DateTime.Now.ToString();
        }
    }
}
