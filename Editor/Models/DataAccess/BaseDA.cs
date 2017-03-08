using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Editor.Models.DataAccess
{
    public class BaseDA
    {
        
        public static Dictionary<string, string> GetAttribute(Type t)
        {
            Dictionary<string, string> _dict = new Dictionary<string, string>();

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    FieldNameAttribute authAttr = attr as FieldNameAttribute;
                    if (authAttr != null)
                    {
                        string propName = prop.Name;
                        string auth = authAttr.Name;

                        _dict.Add(propName, auth);
                    }
                }
            }

            return _dict;
        }
    }
}