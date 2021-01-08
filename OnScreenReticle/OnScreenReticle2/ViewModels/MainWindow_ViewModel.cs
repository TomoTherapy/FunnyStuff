using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace OnScreenReticle2.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private Settings settings;
        private SettingsWindow window;
        private MainWindow main;
        private NotifyIcon Noti;

        public double ReticleSize { get => settings.ReticleSize; set { settings.ReticleSize = Math.Round(value, 1); RaisePropertyChanged(nameof(ReticleSize)); } }
        public double WindowTop { get => settings.WindowTop; set { settings.WindowTop = value; RaisePropertyChanged(nameof(WindowTop)); } }
        public double WindowLeft { get => settings.WindowLeft; set { settings.WindowLeft = value; RaisePropertyChanged(nameof(WindowLeft)); } }
        public int ColorR { get => settings.ColorR; set { settings.ColorR = value; RaisePropertyChanged(nameof(ColorR)); RaisePropertyChanged(nameof(ColorBrush)); } }
        public int ColorG { get => settings.ColorG; set { settings.ColorG = value; RaisePropertyChanged(nameof(ColorG)); RaisePropertyChanged(nameof(ColorBrush)); } }
        public int ColorB { get => settings.ColorB; set { settings.ColorB = value; RaisePropertyChanged(nameof(ColorB)); RaisePropertyChanged(nameof(ColorBrush)); } }
        public int ColorA { get => settings.ColorA; set { settings.ColorA = value; RaisePropertyChanged(nameof(ColorA)); RaisePropertyChanged(nameof(ColorBrush)); } }
        public bool Visibility { get => settings.Visibility; set { settings.Visibility = value; RaisePropertyChanged(nameof(Visibility)); } }

        public Brush ColorBrush
        {
            get => new SolidColorBrush(Color.FromArgb(byte.Parse(ColorA.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorR.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorG.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorB.ToString(), NumberStyles.Integer)));
            set { return; }
        }

        public void Refresh()
        {
            RaisePropertyChanged(nameof(ReticleSize));
            RaisePropertyChanged(nameof(WindowTop));
            RaisePropertyChanged(nameof(WindowLeft));
            RaisePropertyChanged(nameof(ColorR));
            RaisePropertyChanged(nameof(ColorG));
            RaisePropertyChanged(nameof(ColorB));
            RaisePropertyChanged(nameof(ColorA));
            RaisePropertyChanged(nameof(ColorBrush));
        }

        public MainWindow_ViewModel(MainWindow main)
        {
            this.main = main;
            settings = ((App)Application.Current).Xml.settings;
            GenerateNotifyIcon();
        }

        internal void SetVisibility()
        {
            Visibility = !Visibility;
        }

        internal void RotateProfiles()
        {
            if (settings.WindowTop == Screen.PrimaryScreen.Bounds.Height * 0.5 - 50 && settings.WindowLeft == Screen.PrimaryScreen.Bounds.Width * 0.5 - 50)
            {
                settings.WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.6 - 50;
                settings.WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
            }
            else if (settings.WindowTop == Screen.PrimaryScreen.Bounds.Height * 0.6 - 50 && settings.WindowLeft == Screen.PrimaryScreen.Bounds.Width * 0.5 - 50)
            {
                settings.WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.5 - 50;
                settings.WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
            }
            else
            {
                settings.WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.5 - 50;
                settings.WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
            }

            Refresh();
        }

        #region NotifyIcon
        private void GenerateNotifyIcon()
        {
            ContextMenu Menu = new ContextMenu();

            MenuItem OpenSettingsItem = new MenuItem()
            {
                Text = "Open Setting"
            };
            OpenSettingsItem.Click += (object o, EventArgs e) =>
            {
                OpenSettingWindow();
            };
            Menu.MenuItems.Add(OpenSettingsItem);

            MenuItem SetVisibilityItem = new MenuItem()
            {
                Text = "Set Visibility"
            };
            SetVisibilityItem.Click += (object o, EventArgs e) =>
            {
                SetVisibility();
                ((App)Application.Current).Xml.SaveSettings();
            };
            Menu.MenuItems.Add(SetVisibilityItem);

            MenuItem RotateProfilesItem = new MenuItem()
            {
                Text = "Rotate Profiles"
            };
            RotateProfilesItem.Click += (object o, EventArgs e) =>
            {
                RotateProfiles();
                ((App)Application.Current).Xml.SaveSettings();
            };
            Menu.MenuItems.Add(RotateProfilesItem);

            MenuItem ExitItem = new MenuItem()
            {
                Text = "Exit"
            };
            ExitItem.Click += (object o, EventArgs e) =>
            {
                if (window != null) window.Close();
                main.Close();
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

        internal void Window_Closing()
        {
            Noti.Visible = false;
            Noti.Icon = null;
        }
        #endregion

        public void OpenSettingWindow()
        {
            if (window != null)
            {
                window.Close();
                window = null;
            }
            else
            {
                window = new SettingsWindow(this);
                window.ShowDialog();
            }
        }
    }
}
