// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="">
//   
// </copyright>
// <summary>
//   Starting Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Editor.Startup))]

namespace Editor
{
    /// <summary>
    /// Starting Class
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
