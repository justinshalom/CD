using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Editor.Startup))]
namespace Editor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
