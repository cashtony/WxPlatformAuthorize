using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxPlatformAuthorize.WxSDK.Models;

namespace WxPlatformAuthorize.WxSDK
{
    public class ApiClient : IApiClient
    {
        public GetComponentTokenResponse GetComponentToken(GetComponentTokenRequest request)
        {
            return new GetComponentTokenResponse();
        }

        public GetPreAuthCodeResponse GetPreAuthCode(string accessToken, GetPreAuthCodeRequest request)
        {
            return new GetPreAuthCodeResponse();
        }
    }
}
