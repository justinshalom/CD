// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldName.cs" company="">
//   Code Editor
// </copyright>
// <summary>
//   Defines the FieldNameAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Editor.Models
{
    using System;

    /// <summary>
    /// The field name attribute.
    /// </summary>
    public class FieldNameAttribute : Attribute
    {
        /// <summary>
        /// The name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldNameAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        [DynamicallyInvokable]
        public FieldNameAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [DynamicallyInvokable]
        public string Name
        {
            [DynamicallyInvokable]
            get
            {
                return this.name;
            }
        }
    }
}