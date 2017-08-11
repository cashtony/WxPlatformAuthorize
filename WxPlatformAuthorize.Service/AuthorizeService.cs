using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
        public void HandleNotifyEvent(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(EventParameter));
                var parameter = serializer.Deserialize(reader) as EventParameter;
                switch (parameter.InfoType)
                {
                    case "component_verify_ticket":
                        //推送component_verify_ticket
                        _verifyTicket = parameter.ComponentVerifyTicket;
                        break;
                    case "unauthorized":
                        //取消授权
                        break;
                    case "authorized":
                        //授权成功
                        break;
                    case "updateauthorized":
                        //更新授权
                        break;
                    default:
                        break;
                }
            }
        }

        public string GetAccessToken()
        {
            if (String.IsNullOrEmpty(_verifyTicket))
            {
                throw new Exception("未收到component_verify_ticket推送，10分钟后再试");
            }
            return _wxApi.GetComponentToken(_verifyTicket);
        }

        public string GetPreAuthCode()
        {
            return _wxApi.GetPreAuthCode(GetAccessToken());
        }
    }
}
