using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Editor.Models
{
    public class DatabaseConnectionModel
    {
        [Required]
        [DisplayName("DataBase Name")]
        [FieldName("DataBaseName")]
        public string Database { get; set; }
         [Required]
         [DisplayName("Server Name")]
        public string Server { get; set; }
         [Required]
         [DisplayName("User Name")]
        public string UID { get; set; }
         [Required]
         [DisplayName("Password")]
        public string Password { get; set; }
        public string Language { get; set; }
        public string Async { get; set; }
        public string AttachDbFileName { get; set; }
        public bool Trusted_Connection { get; set; }
        public string Network { get; set; }
    }
}