using System;
using System.IO;
using System.Xml.Serialization;
using WxPlatformAuthorize.Repository;
using WxPlatformAuthorize.Repository.Models;
using WxPlatformAuthorize.Service.Models;

namespace WxPlatformAuthorize.Service
{
    public class AuthorizeService
    {
        private WxSDK.WxApiClient _wxApi;
        private IRepository _repository;
        public AuthorizeService(WxSDK.WxApiClient wxApi, IRepository repository)
        {
            _wxApi = wxApi;
            _repository = repository;
        }
        public void HandleNotifyEvent(string msgSignature, string timeStamp, string nonce, string postData)
        {
            var xml = _wxApi.DecryptMessage(msgSignature, timeStamp, nonce, postData);

            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(EventParameter));
                var parameter = serializer.Deserialize(reader) as EventParameter;
                switch (parameter.InfoType)
                {
                    case "component_verify_ticket":
                        //推送component_verify_ticket
                        _repository.Insert(new ComponentVerifyTicket() {
                            VerifyTicket = parameter.ComponentVerifyTicket,
                            CreateTime = parameter.CreateTime
                        });
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
            var ticket = _repository.QueryFirst<ComponentVerifyTicket>("select * from ComponentVerifyTickets order by CreateTime desc");
            //if (String.IsNullOrEmpty(_verifyTicket))
            //{
            //    throw new Exception("未收到component_verify_ticket推送，10分钟后再试");
            //}
            return _wxApi.GetComponentToken(ticket.VerifyTicket);
        }

        public string GetPreAuthCode()
        {
            return _wxApi.GetPreAuthCode(GetAccessToken());
        }
    }
}
