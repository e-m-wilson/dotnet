

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceDemo.API;

public class TimingFilter : IActionFilter
{
    private Stopwatch _sw;

    //When our controller action is triggered
    public void OnActionExecuting(ActionExecutingContext startContext)
    {
        _sw = Stopwatch.StartNew(); // We start counting
    }

    //When our controller action is finished
    public void OnActionExecuted(ActionExecutedContext endContext)
    {
        _sw.Stop(); //We stop counting

        //We want to add a header to our HTTP response on the way out
        endContext.HttpContext.Response.Headers["x-action-ms"] = _sw.ElapsedMilliseconds.ToString();
    }
}