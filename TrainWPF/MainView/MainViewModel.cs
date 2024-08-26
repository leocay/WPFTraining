using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainWPF.MainView
{
    public partial class  MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<int> _userIds = [1, 2, 3, 4, 5];

        public MainViewModel()
        {
            
        }


    }
}
