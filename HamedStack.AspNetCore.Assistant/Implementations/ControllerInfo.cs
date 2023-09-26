namespace HamedStack.AspNetCore.Assistant.Implementations;

/// <summary>
/// Represents information about an MVC controller.
/// </summary>
public class ControllerInfo
{
    /// <summary>
    /// Gets or sets the namespace of the controller.
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    /// Gets or sets the area name associated with the controller.
    /// </summary>
    public string? AreaName { get; set; }

    /// <summary>
    /// Gets or sets the name of the controller.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the collection of attributes associated with the controller.
    /// </summary>
    public ICollection<Attribute>? Attributes { get; set; }

    /// <summary>
    /// Gets or sets the collection of actions associated with the controller.
    /// </summary>
    public ICollection<ActionInfo>? Actions { get; set; }
}