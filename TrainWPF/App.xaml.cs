using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Models;
//using Quartz;
using Serilog;
using System.Windows;
//using ViewModels;
//using Views;

namespace TrainWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost = null!;
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //services.AddModels();
                    //services.AddViewModels();
                    //services.AddViews();
                    services.AddSystem();
                }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await AppHost.StartAsync();
            var appWindow = AppHost.Services.GetRequiredService<MainWindow>();
            appWindow.Show();


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Log lỗi
            Log.Error(e.Exception, "Unhandled UI Exception");

            // Hiển thị thông báo
            MessageBox.Show("Đã xảy ra lỗi trong ứng dụng. Vui lòng liên hệ với bộ phận hỗ trợ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

            // Ngăn chặn ứng dụng crash
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal((Exception)e.ExceptionObject, "Unhandled Non-UI Exception");

            // Tùy chọn: hiển thị thông báo
            MessageBox.Show("Ứng dụng gặp lỗi nghiêm trọng và sẽ tắt.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

            // Kết thúc ứng dụng
            Environment.Exit(1);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();
            base.OnExit(e);
        }
    }


}
