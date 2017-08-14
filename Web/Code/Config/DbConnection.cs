namespace Web.Code.Config
{
    using System.Xml;

    public class DbConnection
    {
        public static string DefaultString
        {
            get
            {
                var webConfigPath = Project.DbDirectory + "web.config";
                string connStr = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(webConfigPath);
                XmlNode xnodes = xdoc.SelectSingleNode("/configuration/connectionStrings");

                foreach (XmlNode xnn in xnodes.ChildNodes)
                {
                    if (xnn.NodeType == XmlNodeType.Comment)
                    {
                    }
                    else
                    {
                        connStr = xnn.Attributes["connectionString"].Value.ToString();
                    }
                }

                return connStr;
                ////if (System.Configuration.ConfigurationManager.ConnectionStrings.Count >= 3)
                ////{
                ////    return System.Configuration.ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString;
                ////}
                ////else
                ////{
                ////    return System.Configuration.ConfigurationManager.AppSettings["DbConnect"];
                ////}
            }
        }
    }
}