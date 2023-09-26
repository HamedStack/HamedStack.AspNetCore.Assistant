using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace HamedStack.AspNetCore.Assistant.Attributes;

/// <summary>
/// An attribute to verify session validity and redirect to a specific action if the session is invalid.
/// </summary>
public class SessionTimeoutAttribute : ActionFilterAttribute
{
    private readonly string _sessionKey;
    private readonly string _action;
    private readonly string _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionTimeoutAttribute"/> class.
    /// </summary>
    /// <param name="sessionKey">The session key to verify.</param>
    /// <param name="action">The action to redirect to if the session is invalid.</param>
    /// <param name="controller">The controller containing the action to redirect to.</param>
    public SessionTimeoutAttribute(string sessionKey, string action, string controller)
    {
        _sessionKey = sessionKey;
        _action = action;
        _controller = controller;
    }

    /// <summary>
    /// Occurs before the action method is invoked.
    /// </summary>
    /// <param name="context">The action executing context which encapsulates information about the current request and action.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Session.TryGetValue(_sessionKey, out _) != true)
        {
            context.Result =
                new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = _controller,
                    action = _action
                }));
        }

        base.OnActionExecuting(context);
    }
}