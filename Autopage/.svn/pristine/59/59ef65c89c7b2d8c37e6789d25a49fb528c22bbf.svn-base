

namespace B2Bauto
{
   public static class DBConfigSettings
    {
      
       public static string ConnectionString
       {
           get
           {
               if (System.Configuration.ConfigurationManager.ConnectionStrings.Count >= 3)
               {
                   return System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
               }
               else
               {
                   return System.Configuration.ConfigurationManager.AppSettings["ConString"];
               }
           }
       }
       public static string ConnectionStringOffline
       {
           get
           {
               if (System.Configuration.ConfigurationManager.ConnectionStrings.Count >= 3)
               {
                   return System.Configuration.ConfigurationManager.ConnectionStrings["ConOfflineDataString"].ConnectionString;
               }
               else
               {
                   return System.Configuration.ConfigurationManager.AppSettings["ConOfflineDataString"];
               }
           }
       }
       public static string MetaSearchConnectionString
       {
           get
           {
               if (System.Configuration.ConfigurationManager.ConnectionStrings.Count >= 3)
               {
                   return System.Configuration.ConfigurationManager.ConnectionStrings["MetaSearchConString"].ConnectionString;
               }
               else
               {
                   return System.Configuration.ConfigurationManager.AppSettings["MetaSearchConString"];
               }
           }
       }
    }
}
