using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Myfashionmarketer.Startup))]
namespace Myfashionmarketer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
