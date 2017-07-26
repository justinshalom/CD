namespace Web.Code.Config
{
    public class DbConnection
    {
        public static string DefaultString
        {
            get
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings.Count >= 3)
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["DbConnect"].ConnectionString;
                }
                else
                {
                    return System.Configuration.ConfigurationManager.AppSettings["DbConnect"];
                }
            }
        }
    }
}