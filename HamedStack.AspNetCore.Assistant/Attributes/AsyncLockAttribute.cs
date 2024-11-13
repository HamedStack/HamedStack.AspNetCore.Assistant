using Microsoft.AspNetCore.Mvc.Filters;

namespace HamedStack.AspNetCore.Assistant.Attributes;

/// <summary>
/// An attribute used to apply a lock to an asynchronous action method, ensuring that only
/// one instance of the method executes at a time. This attribute is implemented using a
/// <see cref="SemaphoreSlim"/> with a single slot (1, 1).
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AsyncLockAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// A semaphore to manage exclusive access to the action method.
    /// Only one thread can enter the critical section at a time.
    /// </summary>
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    /// <summary>
    /// Executes the action method asynchronously, acquiring a lock to ensure only one
    /// execution of the method occurs at any time. The lock is released after the method
    /// completes, whether it finishes successfully or throws an exception.
    /// </summary>
    /// <param name="context">The context in which the action is executed, providing information
    /// about the current HTTP request.</param>
    /// <param name="next">A delegate that executes the next filter in the pipeline, which may
    /// be the action method itself.</param>
    /// <returns>A task that represents the asynchronous execution of the action method.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await Semaphore.WaitAsync();
        try
        {
            await next();
        }
        finally
        {
            Semaphore.Release();
        }
    }
}