namespace HamedStack.AspNetCore.Assistant.Implementations;

/// <summary>
/// Represents information about an MVC action.
/// </summary>
public class ActionInfo
{
    /// <summary>
    /// Gets or sets the name of the action.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the return type of the action.
    /// </summary>
    public Type? ActionReturnType { get; set; }

    /// <summary>
    /// Gets or sets the collection of attributes associated with the action.
    /// </summary>
    public ICollection<Attribute>? Attributes { get; set; }
}