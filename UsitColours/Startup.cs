using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UsitColours.Startup))]
namespace UsitColours
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
