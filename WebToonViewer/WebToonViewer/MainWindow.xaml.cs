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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();

            list.Add(@"C:\Users\crazy\OneDrive\사진\만화\개목걸이\16화\3.jpg");
            list.Add(@"C:\Users\crazy\OneDrive\사진\만화\개목걸이\16화\4.jpg");
            list.Add(@"C:\Users\crazy\OneDrive\사진\만화\개목걸이\16화\5.jpg");
            list.Add(@"C:\Users\crazy\OneDrive\사진\만화\개목걸이\16화\6.jpg");

            Viewer.ItemsSource = list;
        }

        private void WebToonLoad_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).OpenWebToonFolder();
        }
    }
}
