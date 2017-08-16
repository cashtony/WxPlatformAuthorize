using System;
using System.IO;
using System.Xml.Serialization;
using WxPlatformAuthorize.Repository;
using WxPlatformAuthorize.Repository.Models;
using WxPlatformAuthorize.WxSDK.Models.Notification;

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

            var authorizeEvent = XmlDeserialize<AuthorizeEvent>(xml);

            _repository.Insert(new AuthorizeEventRecord()
            {
                EventType = authorizeEvent.InfoType,
                EventXml = xml,
                CreateTime = DateTime.Now
            });
        }

        public string GetAccessToken()
        {
            var record = _repository.QueryFirst<AuthorizeEventRecord>("select * from AuthorizeEventRecords where EventType=@EventType order by CreateTime desc", new { EventType = AuthorizeEventTypes.ComponentVerifyTicket });
            if (record == null)
            {
                throw new Exception("未收到component_verify_ticket推送，10分钟后再试");
            }

            var authorizeEvent = XmlDeserialize<AuthorizeEvent>(record.EventXml);
            var token = _wxApi.GetComponentToken(authorizeEvent.ComponentVerifyTicket);
            return token.ComponentAccessToken;
        }

        public string GetPreAuthCode()
        {
            var preAuthCode = _wxApi.GetPreAuthCode(GetAccessToken());
            return preAuthCode.PreAuthCode;
        }

        private T XmlDeserialize<T>(string xml) where T : class
        {
            var result = default(T);

            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(reader) as T;

            }
            return result;
        }
    }
}
