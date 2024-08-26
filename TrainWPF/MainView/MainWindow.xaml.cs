using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using ViewModels.MainViewModels;

namespace TrainWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  /*      private readonly MainViewModel _mainViewModel;*/
        public MainWindow(/*MainViewModel mainViewModel*/)
        {
            InitializeComponent();
            //DataContext = mainViewModel;
            //_mainViewModel = mainViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.Print(sender.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Debug.Print(sender.ToString());
        }
    }
}