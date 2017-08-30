// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseConnectionModel.cs" company="CodeEditor">
//   Code Editor
// </copyright>
// <summary>
//   The database connection model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The database connection model.
    /// </summary>
    public class DatabaseConnectionModel
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        [Required]
        [DisplayName("DataBase Name")]
        [FieldName("DataBaseName")]
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        [Required]
         [DisplayName("Server Name")]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the User Id.
        /// </summary>
        [Required]
         [DisplayName("User Name")]
        public string UID { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
         [DisplayName("Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the ASYNC.
        /// </summary>
        public string Async { get; set; }

        /// <summary>
        /// Gets or sets the attach database file name.
        /// </summary>
        public string AttachDbFileName { get; set; }

        /// <summary>
        /// Gets or sets the trusted connection.
        /// </summary>
        public string TrustedConnection { get; set; }

        /// <summary>
        /// Gets or sets the network.
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the table.
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the initial catalog.
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// Gets or sets the integrated security.
        /// </summary>
        public string IntegratedSecurity { get; set; }

        /// <summary>
        /// Gets or sets the encrypt.
        /// </summary>
        public string Encrypt { get; set; }

        /// <summary>
        /// Gets or sets the connect timeout.
        /// </summary>
        public string ConnectTimeout { get; set; }

        /// <summary>
        /// Gets or sets the trust server certificate.
        /// </summary>
        public string TrustServerCertificate { get; set; }

        /// <summary>
        /// Gets or sets the application intent.
        /// </summary>
        public string ApplicationIntent { get; set; }

        /// <summary>
        /// Gets or sets the multi sub net fail over.
        /// </summary>
        public string MultiSubNetFailOver { get; set; }

        /// <summary>
        /// Gets or sets the persist security info.
        /// </summary>
        public string PersistSecurityInfo { get; set; }

        /// <summary>
        /// Gets or sets the pooling.
        /// </summary>
        public string Pooling { get; set; }

        /// <summary>
        /// Gets or sets the multiple active result sets.
        /// </summary>
        public string MultipleActiveResultSets { get; set; }

        /// <summary>
        /// Gets or sets the replication.
        /// </summary>
        public string Replication { get; set; }

        /// <summary>
        /// Gets or sets the authentication.
        /// </summary>
        public string Authentication { get; set; }
    }
}