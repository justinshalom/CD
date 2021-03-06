﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiJsonDto.cs" company="Code Editor">
//   Code Editor
// </copyright>
// <summary>
//   The api objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Api.Objects
{
    /// <summary>
    /// The API objects.
    /// </summary>
    public class ApiJsonDto
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
        public object Data { get; set; }
    }
}