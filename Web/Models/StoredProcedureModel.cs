// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoredProcedureModel.cs" company="CodeEditor">
//   Code Editor
// </copyright>
// <summary>
//   The stored procedure model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The stored procedure model.
    /// </summary>
    public class StoredProcedureModel
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        [Required]
        [DisplayName("DataBase Name")]
        [FieldName("DataBaseName")]
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the initial catalog.
        /// </summary>
        [Required]
        [DisplayName("Server Name")]
        public string InitialCatalog { get; set; }

        /// <summary>
        /// Gets or sets the stored procedure name.
        /// </summary>
        public string StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the stored procedure query.
        /// </summary>
        public string StoredProcedureQuery { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public string Parameters { get; set; }
    }
}