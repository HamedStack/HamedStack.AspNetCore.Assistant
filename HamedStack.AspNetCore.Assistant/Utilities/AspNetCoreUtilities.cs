// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System.Reflection;
using HamedStack.AspNetCore.Assistant.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace HamedStack.AspNetCore.Assistant.Utilities;

/// <summary>
/// Provides utility methods related to MVC components such as controllers and actions.
/// </summary>
public static class AspNetCoreUtilities
{
    /// <summary>
    /// Retrieves a collection of <see cref="ControllerInfo"/> instances representing
    /// controllers and their associated actions for a given assembly.
    /// </summary>
    /// <param name="assembly">The assembly from which to retrieve controller information.</param>
    /// <returns>A collection of <see cref="ControllerInfo"/> instances.</returns>
    public static IEnumerable<ControllerInfo> GetControllersInfo(Assembly assembly)
    {
        var info = assembly.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new ControllerActionInfo
                {
                    AreaName = x.DeclaringType?.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(AreaAttribute))?.ConstructorArguments[0].Value?.ToString(),
                    Namespace = x.DeclaringType?.Namespace,
                    ControllerName = x.DeclaringType?.Name,
                    ActionName = x.Name,
                    ActionReturnType = x.ReturnType,
                    ActionAttributes = x.GetCustomAttributes().ToList(),
                    ControllerAttributes = x.DeclaringType?.GetCustomAttributes().ToList()
                })
                .OrderBy(x => x.ControllerName).ThenBy(x => x.ActionName).ToList()
            ;
        return MapToControllerInfos(info);
    }

    /// <summary>
    /// Maps a collection of <see cref="ControllerActionInfo"/> instances to a collection
    /// of <see cref="ControllerInfo"/> instances.
    /// </summary>
    /// <param name="controllerActionInfos">The collection of <see cref="ControllerActionInfo"/> instances to map from.</param>
    /// <returns>A collection of <see cref="ControllerInfo"/> instances.</returns>
    private static IEnumerable<ControllerInfo> MapToControllerInfos(List<ControllerActionInfo> controllerActionInfos)
    {
        var result = new List<ControllerInfo>();

        foreach (var item in controllerActionInfos)
        {
            var current = result.FirstOrDefault(x =>
                x.Name == item.ControllerName && x.AreaName == item.AreaName && x.Namespace == item.Namespace);
            if (current != null)
            {
                if (current.Actions != null && current.Actions.Any())
                {
                    if (item.ActionAttributes != null)
                        current.Actions.Add(new ActionInfo
                        {
                            Attributes = item.ActionAttributes.ToList(),
                            ActionReturnType = item.ActionReturnType,
                            Name = item.ActionName
                        });
                }
                else
                {
                    if (item.ActionAttributes == null) continue;
                    var actions = new List<ActionInfo>
                    {
                        new()
                        {
                            Attributes = item.ActionAttributes.ToList(),
                            ActionReturnType = item.ActionReturnType,
                            Name = item.ActionName
                        }
                    };
                    current.Actions = actions;
                }
            }
            else
            {
                if (item.ControllerAttributes == null) continue;
                var ctrl = new ControllerInfo
                {
                    Name = item.ControllerName,
                    AreaName = item.AreaName,
                    Namespace = item.Namespace,
                    Attributes = item.ControllerAttributes.ToList()
                };

                if (item.ActionAttributes != null)
                {
                    var actions = new List<ActionInfo>
                    {
                        new()
                        {
                            Attributes = item.ActionAttributes.ToList(),
                            ActionReturnType = item.ActionReturnType,
                            Name = item.ActionName
                        }
                    };
                    ctrl.Actions = actions;
                }

                result.Add(ctrl);
            }
        }

        return result;
    }
}