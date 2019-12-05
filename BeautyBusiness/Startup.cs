using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeautyBusiness.Startup))]
namespace BeautyBusiness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
