// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectPropertiesModel.cs" company="JSlab">
//   
// </copyright>
// <summary>
//   Project Properties Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Project Properties Model
    /// </summary>
    public class ProjectPropertiesModel
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        [Required]
        [DisplayName("Project Name")]
        [FieldName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        [Required]
        [DisplayName("Project Path")]
        [FieldName("Path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [Required]
        [DisplayName("Project Name")]
        [FieldName("URL")]
        public string URL { get; set; }

    }
}