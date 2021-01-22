using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebToonViewer.ViewModels;

namespace WebToonViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindow_ViewModel();
        }

        private void WebToonLoad_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).OpenWebToonFolder();
        }

        private void FitImageWidth_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).FitImageWidth_button_Click();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).Window_Closing();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpeedyScrollViewer.ScrollToTop();
        }
    }
}
