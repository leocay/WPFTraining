using Microsoft.Extensions.DependencyInjection;
using TrainWPF.CustomViews.AgiViews;

namespace TrainWPF;

public static class DependencyInjection
{
    public static IServiceCollection AddSystem(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<AgiView>();
        return services;
    }
}
