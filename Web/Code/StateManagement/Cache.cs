namespace Web.Code.StateManagement
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class Cache
    {
        public static void Keep(string cachename, object value)
        {
            HttpContext.Current.Cache.Add(cachename, value, null, DateTime.MinValue, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
        }

        public static object Peek(string cachename)
        {
            if (HttpContext.Current.Cache.Get(cachename) != null)
            {
                return HttpContext.Current.Cache.Get(cachename);
            }
            else
            {
                return null;
            }
        }
        public static void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }
    }
}