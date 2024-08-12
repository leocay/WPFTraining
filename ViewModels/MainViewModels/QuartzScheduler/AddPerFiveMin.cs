using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MainViewModels.QuartzScheduler
{
    public class AddPerFiveMin : IJob
    {

        private readonly MainViewModel _mainViewModel;

        public AddPerFiveMin(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _mainViewModel.GetRandomSideBarItem();
            return Task.CompletedTask;
        }
    }
}
