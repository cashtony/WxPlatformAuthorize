using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.Service.Models;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/notify")]
    public class NotifyController : ApiController
    {
        private AuthorizeService _authorizeService;
        public NotifyController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        [HttpPost, Route("component_verify_ticket")]
        public string UpdateComponentVerifyTicket(HttpRequestMessage request)
        {
            using (var xml = request.Content.ReadAsStreamAsync().Result)
            {
                var serializer = new XmlSerializer(typeof(ComponentVerifyTicketDto));
                var ticket = serializer.Deserialize(xml) as ComponentVerifyTicketDto;
                _authorizeService.RefreshComponentVerifyTicket(ticket);
                return ticket.ComponentVerifyTicket;
            }
        }
    }
}
