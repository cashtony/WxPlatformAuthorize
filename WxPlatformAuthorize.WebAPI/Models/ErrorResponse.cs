using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxPlatformAuthorize.WebAPI.Models
{
    public class ErrorResponse: BaseResponse
    {
        public string ErrMsg { set; get; }
        public ErrorResponse(string errorMsg)
        {
            Success = false;
            ErrMsg = errorMsg;
        }
    }
}