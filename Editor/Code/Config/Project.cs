using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Editor.Code.Config
{
    public class Project
    {
        private static string _url;
        private static string _directory;

        public static string Url
        {
            get { return "http://www.digit.in/samsung-mobile-phones/"; }
            set { _url = value; }
        }

        public static string Directory
        {
            get { return @"D:\WorkOfJustin\Mundipharma\Main\MundipharmaEAS\EAS.Web"; }
            set { _directory = value; }
        }
    }
}