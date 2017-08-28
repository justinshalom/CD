namespace Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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
        public string TrustedConnection { get; set; }
        public string Network { get; set; }
        public string Table { get; set; }
        public string InitialCatalog { get; set; }
        public string IntegratedSecurity { get; set; }
        public string Encrypt { get; set; }
        public string ConnectTimeout { get; set; }
        public string TrustServerCertificate { get; set; }
        public string ApplicationIntent { get; set; }
        public string MultiSubnetFailover { get; set; }
        public string PersistSecurityInfo { get; set; }
        public string Pooling { get; set; }
        public string MultipleActiveResultSets { get; set; }
        public string Replication { get; set; }
        public string Authentication { get; set; }
    }
}