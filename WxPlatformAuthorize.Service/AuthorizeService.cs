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
        private WxSDK.IApiClient _wxApi;
        private string _verifyTicket;
        public AuthorizeService(WxSDK.IApiClient wxApi)
        {
            _wxApi = wxApi;
        }

        public void RefreshComponentVerifyTicket(ComponentVerifyTicketDto ticket)
        {
            _verifyTicket = ticket.ComponentVerifyTicket;
        }

        public string GetAccessToken()
        {
            var componentToken = _wxApi.GetComponentToken(new WxSDK.Models.GetComponentTokenRequest() {
                ComponentVerifyTicket = _verifyTicket
            });

            return componentToken.ComponentAccessToken;
        }

        public string GetPreAuthCode()
        {
            var preAuthCode = _wxApi.GetPreAuthCode(GetAccessToken(), new WxSDK.Models.GetPreAuthCodeRequest()
            {

            });
            return preAuthCode.PreAuthCode;
        }
    }
}
