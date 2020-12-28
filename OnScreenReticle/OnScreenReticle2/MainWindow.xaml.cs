using OnScreenReticle2.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;

namespace OnScreenReticle2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowExTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowExTransparent(hwnd);
        }

        public MainWindow()
        {
            InitializeComponent();
            GenerateNotifyIcon();
            DataContext = new MainWindow_ViewModel();
        }

        private NotifyIcon Noti;

        private void GenerateNotifyIcon()
        {
            ContextMenu Menu = new ContextMenu();

            MenuItem OpenSettingsItem = new MenuItem()
            {
                Text = "Open Setting"
            };
            OpenSettingsItem.Click += (object o, EventArgs e) =>
            {
                var window = new SettingsWindow(this.DataContext as MainWindow_ViewModel);
                window.ShowDialog();
            };
            Menu.MenuItems.Add(OpenSettingsItem);

            MenuItem ExitItem = new MenuItem()
            {
                Text = "Exit"
            };
            ExitItem.Click += (object o, EventArgs e) =>
            {
                this.Close();
            };
            Menu.MenuItems.Add(ExitItem);

            Noti = new NotifyIcon
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Visible = true,
                Text = "OnScreenReticle",
                ContextMenu = Menu
            };
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show(e.Key.ToString());
        }
    }
}
