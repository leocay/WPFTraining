using Microsoft.Extensions.DependencyInjection;
using Views.AgiViews;
using Views.IntelViews;
using Views.StrViews;
using Views.VitViews;

namespace Views;

public static class DependencyInjection
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<AgiView>();
        services.AddSingleton<StrView>();
        services.AddSingleton<IntelView>();
        services.AddSingleton<VitView>();
        return services;
    }
}
