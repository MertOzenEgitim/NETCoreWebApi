using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreWebApiDemo.Filters
{
    public class ResourceLogFilter : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resource Filter: İstek başladı");
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resource Filter: Yanıt gönderildi");
        }        
    }
}
