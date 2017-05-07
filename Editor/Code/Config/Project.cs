using Editor.Code.StateManagement;

namespace Editor.Code.Config
{
    public static class Project
    {
        static Project()
        {
            if (Session.Peek("ProjectProperties") == null)
            {
                var project = new ProjectProperties();
                project.Url = "http://localhost:53910";
                project.Directory = @"F:\HolyWords\HolyWords";
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
            }
        }

        public static string DbPassword { get; set; }

        public static string DbUserName { get; set; }

        public static string Url { get; set; }

        public static string Directory { get; set; }

        public static string DbName { get; set; }
    }

    internal class ProjectProperties
    {
        internal string Url { get; set; }
        internal string Directory { get; set; }

        internal string DbName { get; set; }
        internal string DbUserName { get; set; }
        internal string DbPassword { get; set; }
    }
}