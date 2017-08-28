using System.ComponentModel.DataAnnotations;
using Web.Models;

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
    
    public static string GenerateString(DatabaseConnectionModel databaseConnectionModel)
    {
        var connStr = "";
        connStr += ConcatenateString("Data Source", databaseConnectionModel.Server);
        connStr += ConcatenateString("Initial Catalog", databaseConnectionModel.InitialCatalog);
        connStr += ConcatenateString("Persist Security Info", databaseConnectionModel.PersistSecurityInfo);
        connStr += ConcatenateString("User ID", databaseConnectionModel.UID);
        connStr += ConcatenateString("Password", databaseConnectionModel.Password);
        connStr += ConcatenateString("Pooling", databaseConnectionModel.Pooling);
        connStr += ConcatenateString("MultipleActiveResultSets", databaseConnectionModel.MultipleActiveResultSets);
        connStr += ConcatenateString("Replication", databaseConnectionModel.Replication);
        connStr += ConcatenateString("Authentication", databaseConnectionModel.Authentication);
               
        connStr += ConcatenateString("Integrated Security", databaseConnectionModel.IntegratedSecurity);
        connStr += ConcatenateString("Encrypt", databaseConnectionModel.Encrypt);
        connStr += ConcatenateString("Connect Timeout", databaseConnectionModel.ConnectTimeout);
        connStr += ConcatenateString("TrustServerCertificate", databaseConnectionModel.TrustServerCertificate);
        connStr += ConcatenateString("ApplicationIntent", databaseConnectionModel.ApplicationIntent);
        connStr += ConcatenateString("MultiSubnetFailover", databaseConnectionModel.MultiSubnetFailover);

        return connStr;
       
    
    }

        private static string ConcatenateString(string key = null,string value= null)
        {
            var connStr = "";
              if (key != null&& value != null)
            {
                connStr = key+"="+value+";";
            }
            return connStr;
        }
    }
}
