using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeroAcademy.Startup))]
namespace HeroAcademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
