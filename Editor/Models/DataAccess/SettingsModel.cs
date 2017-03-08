using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Models.DataAccess
{
    public class SettingsDA : BaseDA
    {
        
        internal static void SaveDBConnection(DatabaseConnectionModel model)
        {

            var obj = GetAttribute(typeof(DatabaseConnectionModel)); 

        }

      
    }
}