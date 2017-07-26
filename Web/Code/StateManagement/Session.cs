namespace Web.Code.StateManagement
{
    using System.Web;

    public class Session
    {
        public static void Keep(string sessionname, object value)
        {
            HttpContext.Current.Session[sessionname] = value;
        }
        public static object Peek(string sessionname)
        {
            if (HttpContext.Current.Session[sessionname] != null)
            {
                return HttpContext.Current.Session[sessionname];
            }
            else
            {
                return null;
            }
        }
        public static void Clear(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
        public static bool Exists(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }
    }
}