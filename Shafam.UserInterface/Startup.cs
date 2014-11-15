using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shafam.UserInterface.Startup))]
namespace Shafam.UserInterface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
