namespace WxPlatformAuthorize.WebAPI.Models
{
    public class BaseResponse
    {
        public bool Success { set; get; }

        public BaseResponse()
        {
            Success = true;
        }
    }
}