// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Project type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Config
{
    using Web.Code.StateManagement;
    using Web.Code.Utilities;

    /// <summary>
    /// The project.
    /// </summary>
    public static class Project
    {
        /// <summary>
        /// Initializes static members of the <see cref="Project"/> class.
        /// </summary>
        static Project()
        {
            if (Session.Peek("ProjectProperties") == null)
            {
                
                var project = new ProjectProperties();
                if (SystemRel.GetLocalIPAddress() == "10.10.3.141")
                {
                    project.Url = "http://localhost:63517";
                    project.Directory = @"D:\WorkOfJustin\Replica\Main\Replika\Replika.Web";
                    project.DbName = "Replika_Dev";
                    project.DbServer = "FTS-DSK-178.ftsindia.in";
                    project.DbUserName = "monnieuser";
                    project.DbPassword = "Monnie@123";
                }
                else
                {
                    project.Url = "http://localhost:53910";
                    project.Directory = @"F:\HolyWords\HolyWords";
                    project.DbName = "Replika_Dev";
                    project.DbServer = "FTS-DSK-178.ftsindia.in";
                    project.DbUserName = "monnieuser";
                    project.DbPassword = "Monnie@123";
                }
                Session.Keep("ProjectProperties", project);
            }
            if (Session.Peek("ProjectProperties") != null)
            {
                var project = Session.Peek("ProjectProperties") as ProjectProperties;
                Url = project.Url;
                Directory = project.Directory;
                DbName = project.DbName;
                DbUserName = project.DbUserName;
                DbPassword = project.DbPassword;
                DbServer = project.DbServer;
            }
        }

        /// <summary>
        /// Gets or sets the db server.
        /// </summary>
        public static string DbServer { get; set; }

        /// <summary>
        /// Gets or sets the DB password.
        /// </summary>
        public static string DbPassword { get; set; }

        /// <summary>
        /// Gets or sets the DB user name.
        /// </summary>
        public static string DbUserName { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public static string Url { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public static string Directory { get; set; }

        /// <summary>
        /// Gets or sets the DB name.
        /// </summary>
        public static string DbName { get; set; }
    }
}