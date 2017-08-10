using WxPlatformAuthorize.WxSDK.Models;

namespace WxPlatformAuthorize.WxSDK
{
    public interface IApiClient
    {
        GetComponentTokenResponse GetComponentToken(GetComponentTokenRequest request);
        GetPreAuthCodeResponse GetPreAuthCode(string accessToken, GetPreAuthCodeRequest request);
    }
}