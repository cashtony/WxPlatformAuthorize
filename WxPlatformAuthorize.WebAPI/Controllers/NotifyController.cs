using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.Service.Models;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    public class NotifyController : ApiController
    {
        private AuthorizeService _authorizeService;
        public NotifyController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        [HttpPost]
        public void UpdateComponentVerifyTicket(HttpRequestMessage request)
        {
            using (var xml = request.Content.ReadAsStreamAsync().Result)
            {
                var serializer = new XmlSerializer(typeof(ComponentVerifyTicketDto));
                var ticket = serializer.Deserialize(xml) as ComponentVerifyTicketDto;
                _authorizeService.RefreshComponentVerifyTicket(ticket);
            }
        }
    }
}
