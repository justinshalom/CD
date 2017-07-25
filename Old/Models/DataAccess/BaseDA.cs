// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseDA.cs" company="Code Editor">
//   
// </copyright>
// <summary>
//   The base DA.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Editor.Models.DataAccess
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The base DA.
    /// </summary>
    public class BaseDA
    {
        /// <summary>
        /// The get attribute.
        /// </summary>
        /// <param name="t">The Type
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static Dictionary<string, string> GetAttribute(Type t)
        {
            var dict = new Dictionary<string, string>();

            var props = t.GetProperties();
            foreach (var prop in props)
            {
                var attributes = prop.GetCustomAttributes(true);
                foreach (var attr in attributes)
                {
                    var authAttr = attr as FieldNameAttribute;
                    if (authAttr != null)
                    {
                        var propName = prop.Name;
                        var auth = authAttr.Name;

                        dict.Add(propName, auth);
                    }
                }
            }

            return dict;
        }
    }
}