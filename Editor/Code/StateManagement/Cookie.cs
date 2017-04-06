using System;
using System.Web;
using AngleSharp.Network.Default;

namespace Editor.Code.StateManagement
{
    public class Cookie
    {
        public static void Keep(string cookiename, object value)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Value = value.ToString();
            cookie.Expires = DateTime.MinValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static object Peek(string cookiename)
        {
            if (HttpContext.Current.Response.Cookies[cookiename] != null)
            {
                return HttpContext.Current.Response.Cookies[cookiename];
            }
            else
            {
                return null;
            }
        }
        public static void Clear(string key)
        {
            HttpContext.Current.Response.Cookies.Remove(key);
        }
        public static bool Exists(string key)
        {
            return HttpContext.Current.Response.Cookies[key] != null;
        }
    }
}