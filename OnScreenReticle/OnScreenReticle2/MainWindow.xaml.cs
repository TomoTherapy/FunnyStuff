using OnScreenReticle2.ViewModels;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
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
        private SettingsWindow window;

        private void GenerateNotifyIcon()
        {
            ContextMenu Menu = new ContextMenu();

            MenuItem OpenSettingsItem = new MenuItem()
            {
                Text = "Open Setting"
            };
            OpenSettingsItem.Click += (object o, EventArgs e) =>
            {
                window = new SettingsWindow(this.DataContext as MainWindow_ViewModel);
                window.ShowDialog();
            };
            Menu.MenuItems.Add(OpenSettingsItem);

            MenuItem ExitItem = new MenuItem()
            {
                Text = "Exit"
            };
            ExitItem.Click += (object o, EventArgs e) =>
            {
                if (window != null) window.Close();
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

        

    }
}
