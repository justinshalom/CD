using System;
using System.Collections.Generic;

namespace Caxita.Web.MVC.Areas.AdminWeb.Models
{
    public class AutoPageRootModel
    {
        public AutoPageListModel Root { get; set; }
        public AutoPageHeadModel Head { get; set; }
    }
    public class AutoPageHeadModel
    {
        public string PageName { get; set; }
        public string ControllerName { get; set; }
        public string PageText { get; set; }
    }
    public class AutoPageListModel
    {
        public AutoPageBaseModel FormFields { get; set; }
        public AutoPageBaseModel FilterFields { get; set; }
        public AutoPageBaseModel TableFields { get; set; }
    }
    public class AutoPageBaseModel
    {
        public List<AutoPageFieldsModel> Fields { get; set; }

        public string TableName { get; set; }

        public string PageName { get; set; }
        public string ControllerName { get; set; }
        public string PageText { get; set; }
        public AutoPageAttributesModel attributes { get; set; }
        public AutoPageAdvancedSettingsModel AdvancedSettings { get; set; }

        public string HelpContent { get; set; }
    }
    public class AutoPageFieldsModel
    {
        public string fieldname { get; set; }   //field in db table
        public string label { get; set; }     // label on page
        public string id { get; set; }      // id for element
        public string vartype { get; set; } // variable type on db table
        public string tagname { get; set; } // tagname in html
        public string inputtype { get; set; }   // input type in html
        public string moreclasses { get; set; } // to add more classes to tags
        public string wherecondition { get; set; } // to add more classes to tags
        public string moreattributes { get; set; }  // add more attributes to tags 
        public string join_tableName { get; set; }
        public string join_form { get; set; } // Join with other form
        public string join_key { get; set; } // Join with keyvalue
        public string fileuploadfolder { get; set; }
        public string beunique { get; set; } // Join with keyvalue
        public AutoPageJsonListModel jsonlist { get; set; } // json lists for select tags
        public AutoPageAttributesModel attributes { get; set; }   //all other common attributes for tags
    }
    public class AutoPageJsonListModel
    {
        public string controller { get; set; }  //controller name 
        public string function { get; set; }    //function name
    }
    public class AutoPageAttributesModel
    {
        public string required { get; set; }    //common attributes; no need to provide all of them at same time
        public string name { get; set; }
        public string placeholder { get; set; }
        public string data_value { get; set; }
        public string data_text { get; set; }
        public string data_serverdepend { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public string disable { get; set; }
        public string @readonly { get; set; }
        public string minlength { get; set; }
        public string maxlength { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string rows { get; set; }
        public string cols { get; set; }
        public string data_val_email { get; set; }
        public string data_val_mobile { get; set; }
        public string data_val_number { get; set; }
        public string data_val_phone { get; set; }
        public string data_val_text { get; set; }
        public string data_val_float { get; set; }
        public string data_val_time { get; set; }
        public string xs { get; set; }
        public string sm { get; set; }
        public string md { get; set; }
        public string lg { get; set; }
        public string xsoffset { get; set; }
        public string smoffset { get; set; }
        public string mdoffset { get; set; }
        public string lgoffset { get; set; }
        public string date_noofmonth { get; set; }
        public string date_showbuttonpanel { get; set; }
        public string date_changemonth { get; set; }
        public string date_changeyear { get; set; }
        public string date_dateformat { get; set; }
        public string date_yearrange { get; set; }
        public string date_mindate { get; set; }
        public string date_maxdate { get; set; }
        public string date_dependent { get; set; }
        public string data_multiple_file {get;set;}
    }
    public class AutoPageAdvancedSettingsModel
    {
        public string editclass { get; set; }
        public string editiconclass { get; set; }
        public string editlabel { get; set; }
        public string deleteclass { get; set; }
        public string deleteiconclass { get; set; }
        public string deletelabel { get; set; }
        public string custombuttonclass { get; set; }
        public string custombuttoniconclass { get; set; }
        public string custombuttonlabel { get; set; }
        public string viewclass { get; set; }
        public string viewiconclass { get; set; }
        public string viewlabel { get; set; }
        public string filterbycheckboxclass { get; set; }
        public string filterbycheckbox { get; set; }
        public string bindsearchformclass { get; set; }
        public string outerimageclass { get; set; }
        public string imageclass { get; set; }
        public string paginationperpage { get; set; }
        public string serverdepend { get; set; }
        public string noresultmessage { get; set; }
        public string headclass { get; set; }
        public string bodyclass { get; set; }
        public string footclass { get; set; }
        public string thead { get; set; }
        public string tbody { get; set; }
        public string tfoot { get; set; }
        public string tr { get; set; }
        public string th { get; set; }
        public string td { get; set; }
        public string list { get; set; }
        public string headrowsclass { get; set; }
        public string rowsclass { get; set; }
        public string colmclass { get; set; }

    }
}