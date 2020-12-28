using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace OnScreenReticle2.ViewModels
{
    public class SettingsWindow_ViewModel : ViewModelBase
    {
        private Settings settings;
        private MainWindow_ViewModel main;

        public double ReticleSize { get => settings.ReticleSize; set { settings.ReticleSize = Math.Round(value, 1); RaisePropertyChanged(nameof(ReticleSize)); main.Refresh(); } }
        public double WindowTop { get => settings.WindowTop; set { settings.WindowTop = value; RaisePropertyChanged(nameof(WindowTop)); main.Refresh(); } }
        public double WindowLeft { get => settings.WindowLeft; set { settings.WindowLeft = value; RaisePropertyChanged(nameof(WindowLeft)); main.Refresh(); } }
        public int ColorR { get => settings.ColorR; set { settings.ColorR = value; RaisePropertyChanged(nameof(ColorR)); RaisePropertyChanged(nameof(ColorBrush)); main.Refresh(); } }
        public int ColorG { get => settings.ColorG; set { settings.ColorG = value; RaisePropertyChanged(nameof(ColorG)); RaisePropertyChanged(nameof(ColorBrush)); main.Refresh(); } }
        public int ColorB { get => settings.ColorB; set { settings.ColorB = value; RaisePropertyChanged(nameof(ColorB)); RaisePropertyChanged(nameof(ColorBrush)); main.Refresh(); } }
        public int ColorA { get => settings.ColorA; set { settings.ColorA = value; RaisePropertyChanged(nameof(ColorA)); RaisePropertyChanged(nameof(ColorBrush)); main.Refresh(); } }

        public Brush ColorBrush
        {
            get => new SolidColorBrush(Color.FromArgb(byte.Parse(ColorA.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorR.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorG.ToString(), NumberStyles.Integer)
                , byte.Parse(ColorB.ToString(), NumberStyles.Integer)));
            set { return; }
        }

        public SettingsWindow_ViewModel(MainWindow_ViewModel main)
        {
            settings = ((App)Application.Current).Xml.settings;
            this.main = main;
        }

        internal void Window_Closing()
        {
            ((App)Application.Current).Xml.SaveSettings();
        }

        internal void CenterScreen_button_Click()
        {
            WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.5 - 50;
            WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
        }

        internal void HuntShowdown_button_Click()
        {
            WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.6 - 50;
            WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
        }

    }
}
