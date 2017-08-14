// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonController.cs" company="Code Editor">
//   Code Editor
// </copyright>
// <summary>
//   Json Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Controllers.Json
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AngleSharp.Parser.Html;

    using Web.Code.Config;
    using Web.Code.File;
    using Web.Controllers.Action;

    /// <summary>
    /// Json Controller
    /// </summary>
    /// <seealso cref="BaseController" />
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
            content = this.regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(.*)("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5" + seperator + "$7" + seperator + "$9" + seperator + "$11" + seperator + "$13", content);
            content = this.regreplace(@"(\()("")(.*)("")(.*)("")(.*)("")(\))", @"$1'" + seperator + "$3'" + seperator + "$5'" + seperator + "$7'" + seperator + "$9", content);
            content = this.regreplace(@"(\()("")(.*)("")(\))", @"$1" + seperator + "$3" + seperator + "$5", content);
            Regex regex = new Regex(@"(\()(.*)("")(.*)(\))");
            while (regex.Match(content).Length > 0)
            {
                content = this.regreplace(@"(\()(.*)("")(.*)(\))", @"$1$2" + seperator + "$4$5", content);
            }
            var genidname = "data-genid";
            var html = string.Empty;
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
                if (appendfrom != string.Empty)
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
            html = this.regreplace(seperatorreg, '"'.ToString(), html);
            return html;
        }

        public static string Encrypt(string toEncrypt, bool useHashing = false)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue(
                "SecurityKey",
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
                cTransform.TransformFinalBlock(
                    toEncryptArray,
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
            string key = (string)settingsReader.GetValue(
                "SecurityKey",
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
            string[] htmlsplit =
                {
                    "<body"
                };
            string[] htmlaftersplit =
                {
                    @"</body>",
                    @"</ body>"
                };
            var beforehtml = string.Empty;
            var afterhtml = string.Empty;
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
            var html = string.Empty;
            var tempcontent = content;
            var contenthider = new Dictionary<int, string>();
            try
            {
                var razorstarts = false;
                var razorcommentstarts = false;
                var contenthiderkey = 0;
                var commentdata = string.Empty;
                var options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
                string cleanpattern = @"@(?<=\@).+?(?<=\().+?(?=\))\)";
                string razorcommentspattern = @"(@\*)(.*)(\*@)";
                var templinecontent = string.Empty;
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
                            var commentstartsarray = line.Split(
                                new string[1]
                                    {
                                        "@*"
                                    },
                                StringSplitOptions.None);
                            commentdata = "@*" + commentstartsarray[1];
                            line = line.Replace(commentdata, string.Empty);
                            razorcommentstarts = true;
                        }
                        else if (line.Contains("*@"))
                        {
                            var commentstartsarray = line.Split(
                                new string[1]
                                    {
                                        "*@"
                                    },
                                StringSplitOptions.None);
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
                            line = string.Empty;
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
                    this.Url.Action(
                        "Designer",
                        "Visual",
                        routeValues: null /* specify if needed */,
                        protocol: this.Request.Url.Scheme /* This is the trick */);
                InnerHtml = InnerHtml.Replace("generatedurl", UriString);

                code.InnerHtml = InnerHtml;

                document.Body.AppendChild(code);

                if (appendfrom != string.Empty)
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
                    html = html.Replace("hdkey_" + c.Key + "_hdkey", c.Value);
                }
            }
            catch (Exception e)
            {
                var eobj = e;
                html = tempcontent;
            }
            html = beforehtml + html + afterhtml;
            html = this.regreplace(seperatorreg, '"'.ToString(), html);
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
            var content = string.Empty;
            var filepath = string.Empty;
            if (area == string.Empty && controllername == "shared")
            {
                filepath = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                           "_" + view + FIleUtilities.Dot.cshtml;
                content = FIleUtilities.GetFileContent(filepath);
            }
            else
            {
                content = FIleUtilities.GetFileContent(
                    area,
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
                var html = string.Empty;
                html = area == "shared" ? document.DocumentElement.OuterHtml : document.DocumentElement.QuerySelector("body").InnerHtml;
                if (filepath.Length == 0)
                {
                    FIleUtilities.SetFileContent(
                        area,
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
            return this.OutPut(false);
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="controllername">The controller.</param>
        /// <param name="view">The view.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1616:ElementReturnValueDocumentationMustHaveText", Justification = "Reviewed. Suppression is OK here.")]
        public JsonResult SolveDependency(string controllername = "", string view = "", string area = "")
        {
            try
            {
                ////var htmlnew = FIleUtilities.GetFileContent(@"D:\\WorkOfJustin\\Replica\\Main\\Replika\\Replika.Neiman\\Default.html");
                ////var parser = new HtmlParser();
                ////var document = parser.Parse(htmlnew);

                ////htmlnew = document.DocumentElement.OuterHtml;
                ////htmlnew = Regex.Replace(htmlnew, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                ////FIleUtilities.SetFileContent(htmlnew, @"D:\\WorkOfJustin\\Replica\\Main\\Replika\\Replika.Neiman\\Default.html");
                if (controllername == string.Empty && view == string.Empty)
                {
                    var url = Project.Url;
                    var dir = Project.WebDirectory;

                    var paths = Directory.GetFiles(dir + FIleUtilities.ViewPath + FIleUtilities.SharedPath);
                    var lhtml = string.Empty;
                    foreach (var path in paths)
                    {
                        if (path.Contains("Layout"))
                        {
                            lhtml = FIleUtilities.GetFileContent(path);
                            string changedlhtml = this.ApplyDependencyDetails(lhtml, "Shared", "Layout");
                            if (changedlhtml != lhtml)
                            {
                                FIleUtilities.SetFileContent(changedlhtml, path);
                                return this.OutPut(true);
                            }
                        }
                    }
                    return this.OutPut(false);
                }

                var content = FIleUtilities.GetFileContent(area, controllername, view, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
                var html = this.ApplyGeneratedId(content, controllername, view, "body");
                var layout = content.Split(
                    new string[2]
                        {
                            "Layout=",
                            "Layout ="
                        },
                    StringSplitOptions.None);
                var layoutFile = string.Empty;
                if (layout.Length < 2)
                {
                    layoutFile = FIleUtilities.RootPath + FIleUtilities.ViewPath + FIleUtilities.SharedPath +
                                 "_Layout.cshtml";
                }
                else
                {
                    layoutFile = FIleUtilities.RootPath + string.Join(
                                         string.Empty,
                                         layout[1]
                                             .Split(
                                                 new string[2]
                                                     {
                                                         System.Environment.NewLine,
                                                         ";"
                                                     },
                                                 StringSplitOptions.RemoveEmptyEntries)[0]
                                             .Split(
                                                 new string[1]
                                                     {
                                                         '"' + string.Empty
                                                     },
                                                 StringSplitOptions.RemoveEmptyEntries))
                                     .Replace("~", string.Empty)
                                     .Replace(" ", string.Empty);
                }

                var layoutcontent = FIleUtilities.GetFileContent(layoutFile);
                var layouthtml = this.ApplyGeneratedId(layoutcontent, "Shared", "Layout");

                if (layouthtml != layoutcontent)
                {
                    ////layoutcontent = editorSignature[0] + System.Environment.NewLine + layoutcontent + System.Environment.NewLine +
                    ////editorSignature[0];
                    FIleUtilities.SetFileContent(layouthtml, layoutFile);
                    return this.OutPut(true);
                }
                if (html != content)
                {
                    var success = FIleUtilities.SetFileContent(area, controllername, view, html, FIleUtilities.ViewPath, FIleUtilities.Dot.cshtml);
                    return this.OutPut(true);
                }
            }
            catch (Exception e)
            {
                var obj = e;
            }
            return this.OutPut(false);
        }
    }
}