// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace HamedStack.AspNetCore.Assistant.Attributes;

/// <summary>
/// Represents an action filter attribute that checks the validity of the model state.
/// If the model state is invalid, the user is redirected to a specified action, controller, or Razor page.
/// </summary>
/// <example>
/// <code>
/// [HttpGet]
/// [IfModelIsInvalid(RedirectToAction = "Index", RedirectToController = "Account")]
/// public IActionResult ConfirmEmail(CodeViewModel model)
/// {
///     return View();
/// }
/// </code>
/// </example>
public class IfModelIsInvalidAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Gets or sets the name of the controller to redirect to.
    /// </summary>
    public string? RedirectToController { get; set; }

    /// <summary>
    /// Gets or sets the name of the action to redirect to.
    /// </summary>
    public string? RedirectToAction { get; set; }

    /// <summary>
    /// Gets or sets the name of the Razor page to redirect to.
    /// </summary>
    public string? RedirectToPage { get; set; }

    /// <summary>
    /// Called before the action method is invoked. Checks the validity of the model state
    /// and redirects to the specified action, controller, or Razor page if it's invalid.
    /// </summary>
    /// <param name="context">The context for the current request.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new RedirectToRouteResult(ConstructRouteValueDictionary());
        }
    }

    /// <summary>
    /// Constructs the route value dictionary based on set properties to determine the redirection target.
    /// </summary>
    /// <returns>A dictionary containing route values for the redirection.</returns>
    private RouteValueDictionary ConstructRouteValueDictionary()
    {
        var dict = new RouteValueDictionary();

        if (!string.IsNullOrWhiteSpace(RedirectToPage))
        {
            dict.Add("page", RedirectToPage);
        }
        else if (!string.IsNullOrWhiteSpace(RedirectToController) && !string.IsNullOrWhiteSpace(RedirectToAction))
        {
            dict.Add("controller", RedirectToController);
            dict.Add("action", RedirectToAction);
        }
        else
        {
            throw new InvalidOperationException("Either RedirectToPage or both RedirectToController and RedirectToAction must be set.");
        }

        return dict;
    }
}