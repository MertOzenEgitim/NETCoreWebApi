using NetCoreWebApiDemo.Interfaces;

namespace NetCoreWebApiDemo.Services
{
    public class GuidService : IGuidService
    {
        private readonly string _guid = Guid.NewGuid().ToString();
        public string GetGuid() => _guid;
    }
}
