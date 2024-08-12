using ViewModels.MainViewModels;
using ViewModels.MainViewModels.QuartzScheduler;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ViewModels;

public static class DependencyInjection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("myJob", "group1");
            q.AddJob<AddPerFiveMin>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("myTrigger", "group1")
                .WithCronSchedule("0 0,15,30,45 8-23 ? * *"));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
