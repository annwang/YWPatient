using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YWPatient.Startup))]
namespace YWPatient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
