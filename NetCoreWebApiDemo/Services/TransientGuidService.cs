using NetCoreWebApiDemo.Interfaces;

namespace NetCoreWebApiDemo.Services
{
    public class TransientGuidService : IGuidService
    {
        private readonly string _guid=Guid.NewGuid().ToString();
        public string GetGuid()
        {
            return _guid;
        }
    }
}
