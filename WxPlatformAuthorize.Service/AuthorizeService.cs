using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxPlatformAuthorize.Service.Models;

namespace WxPlatformAuthorize.Service
{
    public class AuthorizeService
    {
        private WxSDK.WxApiClient _wxApi;
        private string _verifyTicket;
        public AuthorizeService(WxSDK.WxApiClient wxApi)
        {
            _wxApi = wxApi;
        }

        public void RefreshComponentVerifyTicket(ComponentVerifyTicketDto ticket)
        {
            _verifyTicket = ticket.ComponentVerifyTicket;
        }

        public string GetAccessToken()
        {
            return _wxApi.GetComponentToken(_verifyTicket);
        }

        public string GetPreAuthCode()
        {
            return _wxApi.GetPreAuthCode(GetAccessToken());
        }
    }
}
