using System;  
using System.Diagnostics;  
using System.Threading;  
using System.Threading.Tasks;  
using Microsoft.AspNet.Builder;  
using Microsoft.AspNet.Http;

namespace Messenger
{
	public class LoggingMiddleware
	{
	    RequestDelegate _next;
	    int _requestCount;

	    // called once when pipeline is built
	    public LoggingMiddleware(RequestDelegate next)
	    {
	        _next = next;
	    }

	    // called once per request
	    public async Task Invoke(HttpContext context)
	    {
	        var sw = new Stopwatch();
	        sw.Start();
	        var requestNumber = Interlocked.Increment(
	            ref _requestCount);

	        // request is incoming
	        Console.WriteLine(string.Format(
	            "#{0} incoming {1} {2}{3}{4}",
	            requestNumber,
	            context.Request.Method,
	            context.Request.PathBase,
	            context.Request.Path,
	            context.Request.QueryString));

	        // pass control to following components
	        await _next(context);

	        // call is unwinding
	        Console.WriteLine(string.Format(
	            "#{0} outgoing {1} {2}ms", 
	            requestNumber,
	            context.Response.StatusCode,
	            sw.ElapsedMilliseconds));
	    }
	}
}