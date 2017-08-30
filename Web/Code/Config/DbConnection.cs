// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbConnection.cs" company="CodeEditor">
//   Code Editor
// </copyright>
// <summary>
//   The db connection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Config
{
    using System.Xml;

    using Web.Models;

    /// <summary>
    /// The database connection.
    /// </summary>
    public class DbConnection
    {
        /// <summary>
        /// Gets the default string.
        /// </summary>
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

        /// <summary>
        /// The generate string.
        /// </summary>
        /// <param name="databaseConnectionModel">
        /// The database connection model.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateString(DatabaseConnectionModel databaseConnectionModel)
        {
            var connStr = string.Empty;
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
            connStr += ConcatenateString("MultiSubnetFailover", databaseConnectionModel.MultiSubNetFailOver);

            return connStr;
        }

        /// <summary>
        /// The concatenate string.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ConcatenateString(string key = null, string value = null)
        {
            var connStr = string.Empty;
            if (key != null && value != null)
            {
                connStr = key + "=" + value + ";";
            }

            return connStr;
        }
    }
}