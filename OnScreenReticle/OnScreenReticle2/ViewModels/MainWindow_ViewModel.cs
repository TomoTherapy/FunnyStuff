using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;

namespace OnScreenReticle2.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private Settings settings;
        private string visibility;

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

        public MainWindow_ViewModel()
        {
            settings = ((App)Application.Current).Xml.settings;
        }

        internal void SetVisible()
        {
            Visibility = !Visibility;
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                if (val) return "Visible";
                else return "Hidden";
            }

            return "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
