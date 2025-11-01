using Microsoft.Extensions.Options;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo.Services
{
    public class ConfigMonitorService
    {
        private readonly IOptionsMonitor<AppSettings> _optionsMonitor;
        public ConfigMonitorService(IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _optionsMonitor=optionsMonitor;

            Console.WriteLine($"Montor Başlatıldı: {_optionsMonitor.CurrentValue.Version}");

            _optionsMonitor.OnChange(settings =>
            {
                Console.WriteLine($"Yeni versiyon algılandı: {settings.Version}");
            });
        }
    }
}
