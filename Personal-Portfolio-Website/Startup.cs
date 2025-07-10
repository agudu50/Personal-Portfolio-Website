using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Personal_Portfolio_Website.Startup))]
namespace Personal_Portfolio_Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
