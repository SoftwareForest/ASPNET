using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNET
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomActionFilter : Attribute, IAsyncActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
