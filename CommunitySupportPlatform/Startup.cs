using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommunitySupportPlatform.Startup))]
namespace CommunitySupportPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
