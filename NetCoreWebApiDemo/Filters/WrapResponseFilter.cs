using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreWebApiDemo.Filters
{
    public class WrapResponseFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
           if(context.Result is ObjectResult result)
            {
                var wrapped = new
                {
                    success = true,
                    data = result.Value
                };
                context.Result=new JsonResult(wrapped);
            }
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Yanıt oluşturuldu");
        }       
    }
}
