namespace WxPlatformAuthorize.WxSDK
{
    public interface IHttpClient
    {
        string Post(string url, string body);
    }
}