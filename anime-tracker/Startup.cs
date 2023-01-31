using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(anime_tracker.Startup))]
namespace anime_tracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
