using OnScreenReticle2.ViewModels;
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
using System.Windows.Shapes;

namespace OnScreenReticle2
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(MainWindow_ViewModel main)
        {
            InitializeComponent();
            DataContext = new SettingsWindow_ViewModel(main);
        }

        private void CenterScreen_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SettingsWindow_ViewModel).CenterScreen_button_Click();
        }

        private void HuntShowdown_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SettingsWindow_ViewModel).HuntShowdown_button_Click();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as SettingsWindow_ViewModel).Window_Closing();
        }
    }
}
