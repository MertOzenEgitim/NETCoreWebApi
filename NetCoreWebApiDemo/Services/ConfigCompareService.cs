using Microsoft.Extensions.Options;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Services
{
    public class ConfigCompareService : IConfigCompareService
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AppSettings> _options;
        private readonly IOptionsSnapshot<AppSettings> _optionsSnapshot;
        private readonly IOptionsMonitor<AppSettings> _optionsMonitor;
        public ConfigCompareService(IConfiguration configuration, IOptions<AppSettings> options, IOptionsSnapshot<AppSettings> optionsSnapshot, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _configuration = configuration;
            _options = options;
            _optionsSnapshot = optionsSnapshot;
            _optionsMonitor = optionsMonitor;
        }
        public object GetConfigCompare()
        {
            return new {
                Configuration = new { version = _configuration["AppSettings:Version"], appName = _configuration["AppSettings:ApplicationName"] },
                Options=new { version = _options.Value.Version, appName = _options.Value.ApplicationName }, 
                OptionsSnapshot=new { version = _optionsSnapshot.Value.Version, appName = _optionsSnapshot.Value.ApplicationName },
                OptionsMonitor = new { version = _optionsMonitor.CurrentValue.Version, appName = _optionsMonitor.CurrentValue.ApplicationName }
            };
        }
    }
}
