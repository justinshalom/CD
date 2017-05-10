using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AngleSharp.Parser.Html;
using CsQuery;
using Editor.Code.Config;
using Editor.Code.File;

namespace Editor.Controllers
{
    /// <summary>
    /// Json Controller
    /// </summary>
    /// <seealso cref="Editor.Controllers.BaseController" />
    public class JsonController : BaseController
    {
        private string regreplace(string pattern, string substitution, string content)
        {
            Regex regex = new Regex(pattern);
            string result = regex.Replace(content, substitution);
            return result;
        }

        private string ApplyGeneratedId(string content, string controller, string view, string appendfrom = "")
        {
            var seperator = "-$-";
            var seperatorreg = @"-\$-";
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(.*)("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5" + seperator + "$7" + seperator + "$9" + seperator + "$11" + seperator + "$13", content);
            content = regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(\))", @"$1'" + seperator + "$3'" + seperator + "$5'" + seperator + "$7'" + seperator + "$9", content);
            content = regreplace(@"(\()("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5", content);
            Regex regex = new Regex(@"(\()(.*)("")(.*)(\))");
            while (regex.Match(content).Length > 0)
            {
                content = regreplace(@"(\()(.*)("")(.*)(\))", @"$1$2" + seperator + "$4$5", content);
            }
            var genidname = "data-genid";
            var html = "";
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                var uniquekey = 0;
                var elements = document.QuerySelectorAll("*");

                foreach (var el in elements)
                {
                    el.RemoveAttribute(genidname);
                    el.SetAttribute(genidname, controller + "_" + view + "_" + uniquekey);
                    uniquekey++;
                }
                if (appendfrom != "")
                {
                    html = document.DocumentElement.QuerySelector(appendfrom).InnerHtml;
                }
                else
                {
                    html = document.DocumentElement.OuterHtml;
                }
            }
            catch (Exception e)
            {
                var eobj = e;
            }
            html = regreplace(seperatorreg, '"'.ToString(), html);
            return html;
        }

        public static string Encrypt(string toEncrypt, bool useHashing = false)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
                cTransform.TransformFinalBlock(toEncryptArray,
                    0,
                    toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing = false)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",
                typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                toEncryptArray,
                0,
                toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private string ApplyDependencyDetails(string content, string controller, string view, string appendfrom = "body")
        {
            string[] htmlsplit = { "<body" };
            string[] htmlaftersplit = { @"</body>", @"</ body>" };
            var beforehtml = "";
            var afterhtml = "";
            var beforehtmlarray = content.Split(htmlsplit, StringSplitOptions.None);
            if (beforehtmlarray.Length == 2)
            {
                beforehtml = beforehtmlarray[0];
                content = "<body" + beforehtmlarray[1];
            }
            var afterhtmlarray = content.Split(htmlaftersplit, StringSplitOptions.None);
            if (afterhtmlarray.Length == 2)
            {
                afterhtml = afterhtmlarray[1];
                content = afterhtmlarray[0] + "</body>";
            }
            ////string[] headplit = { @"<head>", @"</head>", @"</ head>" };
            ////var headhtmlarray = content.Split(htmlsplit, StringSplitOptions.None);
            ////if (headhtmlarray.Length == 3)
            ////{
            ////    string headcleanpattern = @"(@)(.*)\((.*)\)";
            ////    RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;

            ////    foreach (Match m in Regex.Matches(headhtmlarray[1], headcleanpattern, options))
            ////    {
            ////        Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
            ////    }

            ////}
            var seperator = "-$-";
            var seperatorreg = @"-\$-";
            var genidname = "data-genid";
            var html = "";
            var tempcontent = content;
            var contenthider = new Dictionary<int, string>();
            try
            {
                var razorstarts = false;
                var razorcommentstarts = false;
                var contenthiderkey = 0;
                var commentdata = "";
                var options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                string cleanpattern = @"@(?<=\@).+?(?<=\().+?(?=\))\)";
                string razorcommentspattern = @"(@\*)(.*)(\*@)";
                var templinecontent = "";
                using (StringReader reader = new StringReader(content))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var regexcm = new Regex(razorcommentspattern);
                        if (line.Contains("@*") && line.Contains("*@") && regexcm.Matches(line).Count > 0)
                        {
                            foreach (Match m in Regex.Matches(line, razorcommentspattern, options))
                            {
                                var hdkey = "hdkey_" + contenthiderkey + "_hdkey";
                                contenthider.Add(contenthiderkey, m.Value);
                                contenthiderkey++;
                                line = line.Replace(m.Value, hdkey);
                            }
                        }
                        else if (line.Contains("@*"))
                        {
                            var commentstartsarray = line.Split(new string[1] { "@*" }, StringSplitOptions.None);
                            commentdata = "@*" + commentstartsarray[1];
                            line = line.Replace(commentdata, "");
                            razorcommentstarts = true;
                        }
                        else if (line.Contains("*@"))
                        {
                            var commentstartsarray = line.Split(new string[1] { "*@" }, StringSplitOptions.None);
                            commentdata = commentdata + commentstartsarray[0] + "*@";
                            var hdkey = "hdkey_" + contenthiderkey + "_hdkey";
                            contenthider.Add(contenthiderkey, commentdata);
                            contenthiderkey++;
                            line = line.Replace(commentstartsarray[0] + "*@", hdkey);
                            razorcommentstarts = false;
                        }
                        else if (razorcommentstarts == true)
                        {
                            commentdata = commentdata + Environment.NewLine + line;
                            line = "";
                        }
                        var atoccurs = line.Split('@');

                        if (atoccurs.Length > 1)
                        {
                            for (int i = 1; i < atoccurs.Length; i++)
                            {
                                var razorcode = "@" + atoccurs[i];
                                if (razorcode.Contains("@*"))
                                {
                                    razorcode = razorcode + "@";
                                }
                                if (razorcode.Contains("@{"))
                                {
                                    razorstarts = true;
                                }
                                var regexcp = new Regex(cleanpattern);
                                regexcm = new Regex(razorcommentspattern);
                                if (regexcm.Matches(razorcode).Count > 0)
                                {
                                }

                                if (regexcp.Matches(razorcode).Count > 0)
                                {
                                    foreach (Match m in Regex.Matches(razorcode, cleanpattern, options))
                                    {
                                        var hdkey = "hdkey_" + contenthiderkey + "_hdkey";
                                        contenthider.Add(contenthiderkey, m.Value);
                                        contenthiderkey++;
                                        line = line.Replace(m.Value, hdkey);
                                    }
                                }
                            }
                        }
                        templinecontent += Environment.NewLine + line;
                    }
                }
                content = templinecontent;

                ////string headcleanpattern = @"(?<=\().+?(?=\))";

                ////foreach (Match m in Regex.Matches(content, headcleanpattern, options))
                ////{
                ////    Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
                ////    content = "<!--encrypted" + content.Replace(m.Value, Encrypt(m.Value)) + "encryptedend-->";
                ////}
                ////Regex regex = new Regex(@"(\()(.*)("")(.*)(\))");
                ////while (regex.Match(content).Length > 0)
                ////{
                ////    content = regreplace(@"(\()(.*)("")(.*)(\))", @"$1$2" + seperator + "$4$5", content);
                ////}
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                var id = document.QuerySelector("#generatedmaindetails");
                if (id != null)
                {
                    id.Remove();
                }
                var code = document.CreateElement("div");
                code.SetAttribute("id", "generatedmaindetails");
                var mypath = @FIleUtilities.RootPath + FIleUtilities.ViewPath + "Visual\\PrependHeader." + FIleUtilities.Dot.cshtml;
                var InnerHtml = FIleUtilities.GetFileContent(mypath);
                var UriString =
                    Url.Action("Designer", "Visual",
                        routeValues: null /* specify if needed */,
                        protocol: Request.Url.Scheme /* This is the trick */);
                InnerHtml = InnerHtml.Replace("generatedurl", UriString);

                code.InnerHtml = InnerHtml;

                document.Body.AppendChild(code);

                if (appendfrom != "")
                {
                    html = document.DocumentElement.QuerySelector(appendfrom).OuterHtml;
                }
                else
                {
                    html = document.DocumentElement.OuterHtml;
                }
                string entitypattern = @"&gt;";
                Regex entitypatternregex = new Regex(entitypattern);
                html = entitypatternregex.Replace(html, ">");
                entitypattern = @"&gt;";
                entitypatternregex = new Regex(entitypattern);
                html = entitypatternregex.Replace(html, ">");

                foreach (var c in contenthider)
                {
                    html=html.Replace("hdkey_"+c.Key+"_hdkey",c.Value);
                }


            }
            catch (Exception e)
            {
                var eobj = e;
                html = tempcontent;
            }
            html = beforehtml + html + afterhtml;
            html = regreplace(seperatorreg, '"'.ToString(), html);
            return html;
        }

        /// <summary>
        /// Adds the class.
        /// </summary>
        /// <param name="genid"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="controllername">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        public JsonResult Attribute(string genid, string key = "", string value = "", string controllername = "", string view = "layout", string area = "")
        {
            var content = "";
            var filepath = "";
            if (area == "" && controllername == "shared")
            {
                filepath = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                    "_" + view + FIleUtilities.Dot.cshtml;
                content = FIleUtilities.GetFileContent(filepath);
            }
            else
            {
                content = FIleUtilities.GetFileContent(area,
                    controllername,
                    view,
                    FIleUtilities.ViewPath,
                    FIleUtilities.Dot.cshtml);
            }
            try
            {
                var parser = new HtmlParser();
                var document = parser.Parse(content);
                document.QuerySelector("[data-genid=" + genid + "]").SetAttribute(key, value);
                var html = "";
                html = area == "shared" ? document.DocumentElement.OuterHtml : document.DocumentElement.QuerySelector("body").InnerHtml;
                if (filepath.Length == 0)
                {
                    FIleUtilities.SetFileContent(area,
                        controllername,
                        view,
                        html,
                        FIleUtilities.ViewPath,
                        FIleUtilities.Dot.cshtml);
                }
                else
                {
                    FIleUtilities.SetFileContent(html, filepath);
                }
            }
            catch (Exception e)
            {
                var eobj = e;
            }
            return Json(false, String.Empty, string.Empty);
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="controllername">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        public JsonResult SolveDependency(string controllername = "", string view = "", string area = "")
        {
            try{
            ////var htmlnew = FIleUtilities.GetFileContent(@"D:\\WorkOfJustin\\Replica\\Main\\Replika\\Replika.Neiman\\Default.html");
            ////var parser = new HtmlParser();
            ////var document = parser.Parse(htmlnew);

            ////htmlnew = document.DocumentElement.OuterHtml;
            ////htmlnew = Regex.Replace(htmlnew, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            ////FIleUtilities.SetFileContent(htmlnew, @"D:\\WorkOfJustin\\Replica\\Main\\Replika\\Replika.Neiman\\Default.html");
           
          

            if (controllername == "" && view == "")
            {
                var url = Project.Url;
                var dir = Project.Directory;

                var paths = Directory.GetFiles(dir + FIleUtilities.ViewPath + FIleUtilities.SharedPath);
                var lhtml = "";
                foreach (var path in paths)
                {
                    if (path.Contains("Layout"))
                    {
                        lhtml = FIleUtilities.GetFileContent(path);
                        string changedlhtml = ApplyDependencyDetails(lhtml, "Shared", "Layout");
                        if (changedlhtml != lhtml)
                        {

                            FIleUtilities.SetFileContent(changedlhtml, path);
                            return Json(true, String.Empty, string.Empty);
                        }
                    }
                }
                return Json(false, String.Empty, string.Empty);
            }

            var content = FIleUtilities.GetFileContent(area, controllername, view, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
            var html = ApplyGeneratedId(content, controllername, view, "body");
            var layout = content.Split(new string[2] { "Layout=", "Layout =" }, StringSplitOptions.None);
            var layoutFile = "";
            if (layout.Length < 2)
            {
                layoutFile = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                    "_Layout.cshtml";
            }
            else
            {
                layoutFile = FIleUtilities.RootPath + string.Join("", layout[1].Split(new string[2] { System.Environment.NewLine, ";" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new string[1] { '"' + "" }, StringSplitOptions.RemoveEmptyEntries)).Replace("~", "").Replace(" ", "");
            }

            var layoutcontent = FIleUtilities.GetFileContent(layoutFile);
            var layouthtml = ApplyGeneratedId(layoutcontent, "Shared", "Layout");

            if (layouthtml != layoutcontent)
            {
                ////layoutcontent = editorSignature[0] + System.Environment.NewLine + layoutcontent + System.Environment.NewLine +
                ////editorSignature[0];
                FIleUtilities.SetFileContent(layouthtml, layoutFile);
                return Json(true, string.Empty, string.Empty);
            }
            if (html != content)
            {
                var success = FIleUtilities.SetFileContent(area, controllername, view, html, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
                return Json(true, string.Empty, string.Empty);
            }
            }
            catch(Exception e){
                var obj=e;
            }
            return Json(false, String.Empty, string.Empty);
        }
    }
}