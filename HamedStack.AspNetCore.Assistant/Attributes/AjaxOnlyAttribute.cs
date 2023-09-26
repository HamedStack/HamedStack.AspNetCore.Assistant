// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HamedStack.AspNetCore.Assistant.Attributes;

/// <summary>
/// An attribute to ensure that an action method or controller is only accessible via Ajax requests.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AjaxOnlyAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Occurs before the action method is invoked.
    /// </summary>
    /// <param name="filterContext">The filter context which encapsulates information about the current request and action.</param>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (IsAjaxRequest(filterContext.HttpContext.Request))
        {
            base.OnActionExecuting(filterContext);
        }
        else
        {
            throw new InvalidOperationException("This operation can only be accessed via Ajax requests");
        }
    }

    /// <summary>
    /// Determines if the specified HTTP request is an Ajax request.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <returns><c>true</c> if the request is an Ajax request; otherwise, <c>false</c>.</returns>
    private static bool IsAjaxRequest(HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}