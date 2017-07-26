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
        /// The directory.
        /// </summary>
        private string directory;

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        internal string Url { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        internal string Directory
        {
            get
            {
                return this.directory;
            }
            set
            {
                this.directory = value;
            }
        }

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
        /// Gets or sets the db server.
        /// </summary>
        public string DbServer { get; set; }
    }
}