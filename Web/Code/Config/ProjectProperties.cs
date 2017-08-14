// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectProperties.cs" company="">
//   
// </copyright>
// <summary>
//   The project properties.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Config
{
    /// <summary>
    /// The project properties.
    /// </summary>
    internal class ProjectProperties
    {
        /// <summary>
        /// Gets or sets the database server.
        /// </summary>
        internal string DbServer { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        internal string Url { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        internal string WebDirectory { get; set; }

        /// <summary>
        /// Gets or sets the DB name.
        /// </summary>
        internal string DbName { get; set; }

        /// <summary>
        /// Gets or sets the DB user name.
        /// </summary>
        internal string DbUserName { get; set; }

        /// <summary>
        /// Gets or sets the DB password.
        /// </summary>
        internal string DbPassword { get; set; }

        /// <summary>
        /// Gets or sets the business logic code directory.
        /// </summary>
        internal string BlDirectory { get; set; }

        /// <summary>
        /// Gets or sets the database configurations directory.
        /// </summary>
        internal string DbDirectory { get; set; }

        /// <summary>
        /// Gets or sets the view side model properties directory.
        /// </summary>
        internal string VmDirectory { get; set; }

        /// <summary>
        /// Gets or sets the Data Objects directory.
        /// </summary>
        internal string DtoDirectory { get; set; }

    }
}