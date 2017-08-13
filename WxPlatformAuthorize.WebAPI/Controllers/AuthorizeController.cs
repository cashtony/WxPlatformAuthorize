using System.Web.Http;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/authorize")]
    public class AuthorizeController : ApiController
    {
        private AuthorizeService _authorizeService;
        public AuthorizeController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet, Route("access_token")]
        public AccessTokenResponse GetAccessToken()
        {
            return new AccessTokenResponse() { AccessToken = _authorizeService.GetAccessToken() };
        }

        [HttpGet, Route("pre_auth_code")]
        public PreAuthCodeResponse GetPreAuthCode()
        {
            return new PreAuthCodeResponse() { PreAuthCode = _authorizeService.GetPreAuthCode() };
        }
    }
}
