// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryJsonDto.cs" company="Code Editor">
//   Code Editor
// </copyright>
// <summary>
//   The dictionary objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Api.Objects
{
    using System.Collections.Generic;

    /// <summary>
    /// The dictionary objects.
    /// </summary>
    public class DictionaryJsonDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether is true.
        /// </summary>
        public bool IsTrue { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<Dictionary<string, object>> Data { get; set; }
    }
}