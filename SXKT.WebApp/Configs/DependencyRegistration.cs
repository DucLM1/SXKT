using Microsoft.Extensions.DependencyInjection;

namespace SXKT.WebApp.Configs
{
    public class DependencyRegistration
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.AddSingleton<IRegionDtoDal, RegionDtoDal>();
        }
    }
}