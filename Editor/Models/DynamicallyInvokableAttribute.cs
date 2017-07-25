namespace Editor.Models
{
    using System;

    /// <summary>
    /// The dynamically invokable attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal sealed class DynamicallyInvokableAttribute : Attribute
    {
    }
}