using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetApplicationView.Startup))]
namespace BudgetApplicationView
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
