namespace Web.Models.DataAccess
{
    /// <summary>
    /// The settings da.
    /// </summary>
    public class SettingsDA : BaseDA
    {
        /// <summary>
        /// The save database connection.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        public static void SaveDbConnection(DatabaseConnectionModel model)
        {

            var obj = GetAttribute(typeof(DatabaseConnectionModel)); 

        }

      
    }
}