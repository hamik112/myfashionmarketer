using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Api.Myfashionmarketer.Startup))]
namespace Api.Myfashionmarketer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
