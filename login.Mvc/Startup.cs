using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(login.Mvc.Startup))]
namespace login.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
