namespace WxPlatformAuthorize.Repository
{
    public interface IRepository
    {
        void Insert<T>(T obj) where T : class;
        T QueryFirst<T>(string sql, object param = null) where T : class;
    }
}