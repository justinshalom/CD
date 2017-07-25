using Caxita.Web.MVC.Areas.AdminWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Web;
namespace B2Bauto
{
    public class AutoPage
    {

        public static void XMLforFormCreate(AutoPageRootModel data)
        {
            XmlDocument writer = new XmlDocument();
            // Create XML declaration
            XmlNode declaration = writer.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            writer.AppendChild(declaration);
        }
        public static AutoPageRootModel GetModel(string pagename, string directory)
        {
            string FilePath = MakePath(directory,pagename);
            return GetData(FilePath);       
        }

        private static AutoPageRootModel GetData(string FilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AutoPageRootModel));
            if (System.IO.File.Exists(FilePath))
            {
                using (FileStream stream = System.IO.File.OpenRead(FilePath))
                {
                    return (AutoPageRootModel)serializer.Deserialize(stream);

                }
            }

            return null;
        }
        public static List<AutoPageHeadModel> GetDataList()
        {
            List<AutoPageHeadModel> list = new List<AutoPageHeadModel>();
            var FilePath = System.AppDomain.CurrentDomain.BaseDirectory + "XML\\AutoPages\\PageList.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<AutoPageHeadModel>));
            if (System.IO.File.Exists(FilePath))
            {
                using (FileStream stream = System.IO.File.OpenRead(FilePath))
                {
                    list= (List<AutoPageHeadModel>)serializer.Deserialize(stream);
                }
            }
            return list;
        }
        public static List<AutoPageHeadModel> GetLinkList(List<AutoPageHeadModel> data, string ControllerName)
        {
            List<AutoPageHeadModel> list = new List<AutoPageHeadModel>();
            if (data != null && data.Count>0) 
                list = data.Where(p => p.ControllerName == ControllerName).ToList();
            return list;
        }

        private static string MakePath(string directory, string pagename)
        {
            return directory + "XML\\AutoPages\\" + pagename + ".xml";
        }
        private static string joinit(string jointable, string totablename, string primarykey, string tablename, string foreignkey)
        {
            return " LEFT JOIN [" + jointable + "] ON [" + totablename + "].[" + primarykey + "] = [" + tablename + "].[" + foreignkey+"]";
        }
        private static string selectit(string fieldname, string name)
        {
            return "["+fieldname + "] as " + name;
        }
        private static string selectit(string tablename, string fieldname, string name)
        {
            return "["+tablename + "].[" + fieldname + "] as " + name;
        }
        private static string selectitequal(string fieldname, string name)
        {
           name= name.Replace("'",""+'"'+"");
            return "["+fieldname + "] = '" + name + "'";
        }
        public static List<AutoPageFieldsModel> FilterById(AutoPageBaseModel List)
        {
            return List.Fields.Where(x => x.id != null).ToList();
        }
        
        private static Dictionary<string, object> CollectionToModel(FormCollection collection)
        {
            return collection.AllKeys.ToDictionary(k => k, v => (object)(collection[v]));
        }

        private static string Equal(string fieldname, object id)
        {
            return "[" + fieldname + "] = '" + id+"'";
        }

        private static string WhereClauseLikeBoth(string fieldname, string word)
        {
            return "["+fieldname + "] like '" + "%" + word + "%" + "'";
        }
        private static string CheckWhereClause(List<string> WhereArray)
        {
            var flag=false;
            List<string> WhereArrays = new List<string>();
            if (WhereArray.Count > 0)
            {
                for (int i = 0; i < WhereArray.Count; i++)
                {
                    if(WhereArray[i]!=null)
                    {
                        flag = true;
                        WhereArrays.Add(WhereArray[i]);
                    }
                }
                if(flag==true)
                    return String.Join(" AND ", WhereArrays.ToArray());
            }
            return "";
        }
        private static string CheckWhereClauseOR(List<string> WhereArray)
        {
            var flag = false;
            List<string> WhereArrays = new List<string>();
            if (WhereArray.Count > 0)
            {
                for (int i = 0; i < WhereArray.Count; i++)
                {
                    if (WhereArray[i] != null)
                    {
                        flag = true;
                        WhereArrays.Add(WhereArray[i]);
                    }
                }
                if (flag == true)
                    return String.Join(" OR ", WhereArrays.ToArray());
            }
            return "";
        }

        private static string Conditionby(string fieldname, object values, string condition)
        {
            var value = values.ToString().TrimEnd(' ');
            switch (condition)
            {
                case "0":
                    return null;
                case "likeend":
                    return " [" + fieldname + "] like '" + "%" + value + "' ";

                case "likestart":
                    return " [" + fieldname + "] like '" + value + "%" + "'";

                case "likeboth":
                    return " [" + fieldname + "] like '" + "%" + value + "%" + "'";

                default:
                    return " [" + fieldname + "] " + condition + " '" + value + "' ";

            }
        }    
        public static DataSet GetConfig(FormCollection collection,string directory,string pagename)
        {
            var model = CollectionToModel(collection);
            string WhereClause = "";
            string JoinClause = "";
            var WhereArray = new List<string>();
            var JoinArray = new List<string>();

            var root = GetModel(pagename, directory);

            var filteredlist = FilterById(root.Root.TableFields);
            string[] mylist = new string[filteredlist.Count];
            string[] joinList = new string[filteredlist.Count];
            for (int i = 0; i < filteredlist.Count; i++)
            {
                if (filteredlist[i].id != null)
                {
                    if (root.Root.TableFields.Fields[i].attributes != null && root.Root.TableFields.Fields[i].attributes.data_text != null && root.Root.TableFields.Fields[i].attributes.data_value != null)
                    {
                        if (filteredlist[i].inputtype.ToLower() == "delete" || filteredlist[i].inputtype.ToLower() == "edit" || filteredlist[i].inputtype.ToLower() == "view" || filteredlist[i].inputtype.ToLower() == "custom")
                        {
                            mylist[i] = "[" + root.Root.TableFields.Fields[i].join_tableName.ToString() + "]" + '.' + "[" + root.Root.TableFields.Fields[i].attributes.data_text.ToString() + "] as " + filteredlist[i].id.ToString() + "id";
                             joinList[i] = joinit(root.Root.TableFields.Fields[i].join_tableName, root.Root.TableFields.Fields[i].join_tableName.ToString(), root.Root.TableFields.Fields[i].attributes.data_value.ToString(), root.Root.TableFields.TableName, root.Root.TableFields.Fields[i].fieldname);
                        }
                        else
                        {
                            mylist[i] = "[" + root.Root.TableFields.Fields[i].join_tableName.ToString() + "]" + '.' + "[" + root.Root.TableFields.Fields[i].attributes.data_text.ToString() + "] as " + filteredlist[i].id.ToString();
                            joinList[i] = joinit(root.Root.TableFields.Fields[i].join_tableName, root.Root.TableFields.Fields[i].join_tableName.ToString(), root.Root.TableFields.Fields[i].attributes.data_value.ToString(), root.Root.TableFields.TableName, root.Root.TableFields.Fields[i].fieldname);

                        }
                    }
                    else
                    {
                        if (filteredlist[i].inputtype.ToLower() == "delete" || filteredlist[i].inputtype.ToLower() == "edit" || filteredlist[i].inputtype.ToLower() == "view" || filteredlist[i].inputtype.ToLower() == "custom")
                        {
                            mylist[i] = selectit(root.Root.TableFields.TableName,filteredlist[i].fieldname.ToString(),filteredlist[i].id.ToString() + "id");
                        }
                        else
                        {
                            mylist[i] = selectit(root.Root.TableFields.TableName,filteredlist[i].fieldname.ToString(),filteredlist[i].id.ToString());
                        }
                    }
                }
            }

            for (int i = 0; i < root.Root.FilterFields.Fields.Count; i++)
            {
                if (root.Root.FilterFields.Fields[i].attributes != null && root.Root.FilterFields.Fields[i].attributes.name != null && model.ContainsKey(root.Root.FilterFields.Fields[i].attributes.name) && model[root.Root.FilterFields.Fields[i].attributes.name] != null && model[root.Root.FilterFields.Fields[i].attributes.name] != "")
                {
                    var condition = root.Root.FilterFields.Fields[i].wherecondition;
                    WhereArray.Add(Conditionby(root.Root.FilterFields.Fields[i].fieldname, model[root.Root.FilterFields.Fields[i].attributes.name], condition));
                    
                }
            }
            WhereClause = CheckWhereClause(WhereArray);  
            if (joinList.Length > 0)
            {
                JoinClause = String.Join(" ", joinList.ToArray());
            }
            var dt = AutoPageDA.GetautoRows(string.Join(",", mylist), root.Root.TableFields.TableName, JoinClause, WhereClause);
            return dt;
        }

        public static string SaveConfig(AutoPageRootModel model, string domain, string culture, string areaname, string scheme, string authority)
        {
            if (model.Root.FormFields.PageName != null)
            {
                model.Head.PageName = model.Root.FormFields.PageName;
                model.Root.FormFields.PageText= model.Head.PageText;
                model.Root.FormFields.ControllerName = model.Head.ControllerName;
                string FilePath = MakePath(domain, model.Root.FormFields.PageName);
                XmlSerializer serializer = new XmlSerializer(typeof(AutoPageRootModel));
                MemoryStream memStream = new MemoryStream();

                serializer.Serialize(memStream, model);//lstdevices : I take result as a list.
                FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite); //foldername:Specify the path to store the xml file
                memStream.WriteTo(file);
                file.Close();

                FilePath = MakePath(domain, "PageList");
                List<AutoPageHeadModel> list = new List<AutoPageHeadModel>();
                var data = GetDataList();
                list = data == null ? new List<AutoPageHeadModel>() : data;
                
                if (list.Where(p => p.ControllerName == model.Head.ControllerName && p.PageName == model.Head.PageName && p.PageText == model.Head.PageText).ToList().Count() > 0)
                {
                    list.RemoveAll(p => p.ControllerName == model.Head.ControllerName && p.PageName == model.Head.PageName && p.PageText == model.Head.PageText);
                }
                list.Add(model.Head);

                serializer = new XmlSerializer(typeof(List<AutoPageHeadModel>));
                memStream = new MemoryStream();
                serializer.Serialize(memStream, list);

                file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite); //foldername:Specify the path to store the xml file
                memStream.WriteTo(file);
                file.Close();

                return string.Format("{0}://{1}{2}", scheme, authority, "/" + culture + "/" + areaname + "/Page/AutoPages/" + model.Root.FormFields.PageName);
            }
            return "Try Again";
        }

        public static DataSet GetById(FormCollection collection,string directory,string pagename)
        {
            var model = CollectionToModel(collection);
            string WhereClause = "";
            string JoinClause = "";
            var WhereArray = new List<string>();
            var JoinArray = new List<string>();
            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory, pagename);
            var root = GetData(FilePath);
            var filteredlist = FilterById(root.Root.TableFields);
            string[] joinList = new string[filteredlist.Count];
            for (int i = 0; i < filteredlist.Count; i++)
            {
                if (filteredlist[i].id != null)
                {
                    if (model.ContainsKey(filteredlist[i].id) && filteredlist[i] != null)
                    {
                        WhereArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                    }
                }

            }
            var formlist = FilterById(root.Root.FormFields).Where(x => x.tagname != "Button").ToList();
            
            string[] mylists = new string[formlist.Count];
            string[] joinLists = new string[filteredlist.Count];
            for (int i = 0; i < formlist.Count; i++)
            {
                if (formlist[i].id != null && formlist[i].fieldname != null)
                {
                    if (formlist[i].attributes != null && formlist[i].attributes.data_text != null && formlist[i].attributes.data_value != null && formlist[i].inputtype == "autocomplete")
                    {
                        {
                            mylists[i] = selectit(formlist[i].join_tableName.ToString(), formlist[i].attributes.data_text.ToString(), formlist[i].id.ToString());
                            joinList[i] = joinit(formlist[i].join_tableName, formlist[i].join_tableName.ToString(), formlist[i].attributes.data_value.ToString(), root.Root.FormFields.TableName, formlist[i].fieldname);

                        }
                    }
                    else
                    {
                        mylists[i] = selectit(root.Root.FormFields.TableName,formlist[i].fieldname.ToString(), formlist[i].id.ToString());
                    }
                
                }
            }

            if (joinList.Length > 0)
            {
                JoinClause = String.Join(" ", joinList.ToArray());
            }
            WhereClause = CheckWhereClause(WhereArray);  
            return AutoPageDA.GetautoRows(string.Join(",", mylists).TrimEnd(','), root.Root.TableFields.TableName, JoinClause, WhereClause);
            
        }

        public static DataSet GetDropDownList(FormCollection collection, string pagename, string id, string listname)
        {
            Dictionary<string, object> model = CollectionToModel(collection);
            var WhereArray = new List<string>();
            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory,pagename);
            var root = GetData(FilePath);
            var filteredlist = new List<AutoPageFieldsModel>();
            if (listname == "FormFields")
            {
                filteredlist = FilterById(root.Root.FormFields);
            }
            else
            {
                filteredlist = FilterById(root.Root.FilterFields);
            }

            string[] mylist = new string[filteredlist.Count];
            for (var i = 0; i < filteredlist.Count; i++)
            {
                if (filteredlist[i].attributes.data_value != null && filteredlist[i].attributes.data_text != null && filteredlist[i].join_tableName!=null)
                    return AutoPageDA.GetautoRows(selectit(filteredlist[i].attributes.data_value, "value ") + ", " + selectit(filteredlist[i].attributes.data_text, "text"), filteredlist[i].join_tableName, "", "");
            }
            return null;
        }

        public static Datas GetAutocomplete(FormCollection collection, string directory, string pagename, string formtype, string name, int limit, string word, int from)
        {
            Dictionary<string, object> model = CollectionToModel(collection);
            var WhereArray = new List<string>();
            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory, pagename);
            var root = GetModel(pagename, directory);
            var filteredlist = new List<AutoPageFieldsModel>();
            if(formtype=="FormFields")
            {
                filteredlist = FilterById(root.Root.FormFields);
            }
            else if(formtype=="FilterFields")
            {
                filteredlist = FilterById(root.Root.FilterFields);
            }
            
            string WhereClause = " WHERE ";
            string[] mylist = new string[2];
            var tablename = "";
            word = word.ToLower();
                //Get dat from table based on the word -start
                for (var i = 0; i < filteredlist.Count; i++)
                {
                    if ( filteredlist[i].attributes != null&&filteredlist[i].attributes.name == name &&  filteredlist[i].inputtype == "autocomplete")
                    {
                        tablename = filteredlist[i].join_tableName;
                        if (filteredlist[i].attributes.data_value != null)
                        {
                            mylist[0] = selectit(filteredlist[i].attributes.data_value, "value");
                            if (!string.IsNullOrEmpty(word))
                            {              
                                WhereArray.Add(WhereClauseLikeBoth(filteredlist[i].attributes.data_value,word));
                            }
                        }

                        if (filteredlist[i].attributes.data_text != null)
                        {
                            mylist[1] = selectit(filteredlist[i].attributes.data_text, "text");
                            if (!string.IsNullOrEmpty(word))
                            {            
                                WhereArray.Add(WhereClauseLikeBoth(filteredlist[i].attributes.data_text,word));
                            }
                        }
                    }
                }
           
                WhereClause=CheckWhereClauseOR(WhereArray);
                var tableData = AutoPageDA.GetAutcomplete(string.Join(",", mylist).TrimEnd(',').TrimStart(','), tablename, WhereClause, limit);
                //Get dat from table based on the word - end

                //Show the table to UI
                int n = 0;
                string[] arrays = new string[tableData.Tables[0].Rows.Count];
                if (tableData != null && tableData.Tables[0].Rows!=null)
                {
                        foreach (DataRow dr in tableData.Tables[0].Rows)
                        {
                            //if (dr["value"] != null && dr["text"].ToString() != null && dr["value"].ToString() != dr["text"].ToString())
                            //{
                            //    arrays[n] = filteredlist[i].attributes.data_val_text + "-" + filteredlist[i].attributes.data_value;
                            //}
                            //else 
                                if(dr["text"].ToString()!=null)
                                {
                                    arrays[n] = dr["text"].ToString();
                                }
                                else
                                {
                                    arrays[n] = dr["value"].ToString();
                                }
                           
                            n++;
                        }
                }

                Datas data = new Datas(tableData.Tables[0].Rows.Count, arrays, DateTime.Now.ToString());
                return data;
            
        }
        public static string Save(FormCollection collection, string pagename)
        {
            Dictionary<string, object> model = CollectionToModel(collection);

            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory, pagename);
            var root = GetData(FilePath);
            var filteredlist = FilterById(root.Root.FormFields);
            string[] mylist = new string[filteredlist.Count];
            var WhereArray = new List<string>();
            string WhereClause = "";
            var UniqueArray = new List<string>();
            string UniqueClause = "";
            var columnlist = new List<string>();
            var paramlist = new List<string>();
            string result = "";
            if (model[filteredlist[0].id] != null && model[filteredlist[0].id]!="")
            {
                for (int i = 0; i < filteredlist.Count; i++)
                {
                    if (filteredlist[i].fieldname != null)
                    {
                        if (filteredlist[i] != null && filteredlist[i].beunique != null && model.ContainsKey(filteredlist[i].id))
                        {
                            UniqueArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                        }
                        if (filteredlist[i] != null && filteredlist[i].inputtype == "hidden" && model.ContainsKey(filteredlist[i].id))
                        {
                            WhereArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                        }
                        else
                        {
                            if (filteredlist[i].inputtype == "checkbox" && model.ContainsKey(filteredlist[i].id) == false)
                            {
                                mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), "false");
                            }
                            else if (model.ContainsKey(filteredlist[i].id) && filteredlist[i].attributes != null && filteredlist[i].attributes.data_text != null && filteredlist[i].attributes.data_value != null && filteredlist[i].inputtype == "autocomplete")
                            {
                                var subresult = SavesSubForm(filteredlist, model, i);
                               
                                mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), subresult);
                            }
                            else{
                                if (model.ContainsKey(filteredlist[i].id))
                                    mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id].ToString());
                            }
                        }
                    }
                }
                WhereClause = CheckWhereClause(WhereArray);
                UniqueClause = CheckWhereClause(UniqueArray);
                result = AutoPageDA.UpdateAutoPage(string.Join(",", mylist.Where(s => !string.IsNullOrEmpty(s))).TrimEnd(',').TrimStart(','), root.Root.TableFields.TableName, WhereClause, UniqueClause);
                return "Updated";
            }
            else
            {
                for (int i = 0; i < filteredlist.Count; i++)
                {
                    if (filteredlist[i].fieldname != null && filteredlist[i].id != null)
                    {
                        if (filteredlist[i].attributes != null && filteredlist[i].attributes.name != null && model.ContainsKey(filteredlist[i].attributes.name) && model[filteredlist[i].attributes.name] != null && model[filteredlist[i].attributes.name] != "")
                        {
                            if (filteredlist[i] != null && filteredlist[i].beunique != null && model.ContainsKey(filteredlist[i].id))
                            {
                                UniqueArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                            }
                            columnlist.Add('['+filteredlist[i].fieldname.ToString()+']');
                            paramlist.Add("'" + model[filteredlist[i].attributes.name].ToString().Replace("'",""+'"'+"") + "'");
                        }
                      
                        if (filteredlist[i] != null && filteredlist[i].inputtype == "hidden" && model.ContainsKey(filteredlist[i].id))
                        {
                          //  WhereArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                        }
                        else
                        {
                            if (filteredlist[i].inputtype == "checkbox" && model.ContainsKey(filteredlist[i].id) == false)
                            {
                                mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), "false");
                            }
                            else if (model.ContainsKey(filteredlist[i].id)&&filteredlist[i].attributes != null && filteredlist[i].attributes.data_text != null && filteredlist[i].attributes.data_value != null && filteredlist[i].inputtype=="autocomplete")
                            {
                                var subresult = SavesSubForm(filteredlist, model,i);
                                paramlist[paramlist.Count - 1] = subresult;
                                mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), subresult);
                            }
                            else{
                                if (model.ContainsKey(filteredlist[i].id))
                                    mylist[i] = selectitequal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id].ToString());
                            }
                        }
                    }
                }
                if (columnlist.Count > 0)
                {
                    UniqueClause = CheckWhereClause(UniqueArray);
                    result = AutoPageDA.SaveAutoPage(String.Join(", ", columnlist.ToArray()), String.Join(", ", paramlist.ToArray()), root.Root.FormFields.TableName, UniqueClause, string.Join(",", mylist.Where(s => !string.IsNullOrEmpty(s))).TrimEnd(',').TrimStart(','));
                }

                return  "Saved";
            }
        }
        public static string SavesSubForm(List<AutoPageFieldsModel> filteredlist,Dictionary<string, object> model,int i)
        {
            var subUniqueArray = new List<string>();
            string subUniqueClause = "";
            var subcolumnlist = new List<string>();
            var subparamlist = new List<string>();
            string[] submylist = new string[1];
            subUniqueArray.Add(Equal(filteredlist[i].attributes.data_text, model[filteredlist[i].attributes.name]));
            subcolumnlist.Add('[' + filteredlist[i].attributes.data_text + ']');
            subparamlist.Add("'" + model[filteredlist[i].attributes.name] + "'");
            submylist[0] = selectitequal(filteredlist[i].attributes.data_text, model[filteredlist[i].id].ToString());

            subUniqueClause = CheckWhereClause(subUniqueArray);

            var subresult = AutoPageDA.SaveAutoPage(String.Join(", ", subcolumnlist.ToArray()), String.Join(", ", subparamlist.ToArray()), filteredlist[i].join_tableName, subUniqueClause, string.Join(",", submylist.Where(s => !string.IsNullOrEmpty(s))).TrimEnd(',').TrimStart(','));

            return subresult;

        }
        public static string DeleteRows(FormCollection collection, string pagename)
        {
            Dictionary<string, object> model = CollectionToModel(collection);
            string WhereClause = "";
            var WhereArray = new List<string>();
            var JoinArray = new List<string>();
            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory, pagename);
            var root = GetData(FilePath);
            var filteredlist = FilterById(root.Root.TableFields);
            string[] joinList = new string[filteredlist.Count];
            for (int i = 0; i < filteredlist.Count; i++)
            {
                if (filteredlist[i].id != null)
                {
                    if (model.ContainsKey(filteredlist[i].id) && filteredlist[i] != null)
                    {
                        WhereArray.Add(Equal(filteredlist[i].fieldname.ToString(), model[filteredlist[i].id]));
                    }
                }
            }

            WhereClause = CheckWhereClause(WhereArray);  
            return AutoPageDA.DeleteAutoPage(root.Root.TableFields.TableName, WhereClause);            
        }
        public class Datas
        {
            public int Count;
            public string[] List;
            public string Datevalue;
            public Datas(int count, string[] list, string datevalue)
            {
                Count = count;
                List = list;
                Datevalue = datevalue;

            }
        }

        public static AutoPageFieldsModel GetFolderName(string pagename, string fieldname)
        {
            string FilePath = MakePath(System.AppDomain.CurrentDomain.BaseDirectory, pagename);
            var root = GetData(FilePath);
            var foldername = new AutoPageFieldsModel();
            foldername=root.Root.FormFields.Fields.Where(x => x.attributes.name == fieldname).First();


            return foldername;
        }
    }
}
