using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WebToonViewer.ViewModels
{
    class MainWindow_ViewModel : ViewModelBase
    {
        private Xml.Settings settings;
        private List<Episode> webtoon;
        private string title;
        private Episode selectedEpisode;

        public List<Episode> WebToon { get => webtoon; set { webtoon = value; RaisePropertyChanged(nameof(WebToon)); } }
        public Episode SelectedEpisode { get => selectedEpisode; set { selectedEpisode = value; RaisePropertyChanged(nameof(SelectedEpisode)); } }
        public string Title { get => title; set { title = value; RaisePropertyChanged(nameof(Title)); } }
        public int Width { get => settings.Width; set { settings.Width = value; RaisePropertyChanged(nameof(Width)); } }
        public bool IsLevel2 { get => settings.IsLevel2; set { settings.IsLevel2 = value; RaisePropertyChanged(nameof(IsLevel2)); } }
        public double SpeedFactor { get => settings.SpeedFactor; set { settings.SpeedFactor = value; RaisePropertyChanged(nameof(SpeedFactor)); } }

        internal void FitImageWidth_button_Click()
        {
            try
            {
                Bitmap bmp = new Bitmap(SelectedEpisode.Parts[0]);
                Width = bmp.Width;
                bmp.Dispose();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        internal void Window_Closing()
        {
            ((App)System.Windows.Application.Current).Xml.SettingsSave();
        }

        public MainWindow_ViewModel()
        {
            settings = ((App)System.Windows.Application.Current).Xml.Settings;
        }

        public void OpenWebToonFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (!IsLevel2)
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                Title = dialog.SelectedPath.Split('\\').Last();

                WebToon = new List<Episode>();

                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath);
                List<string> parts = new List<string>();
                foreach (var part in dir.GetFiles())
                {
                    parts.Add(part.FullName);
                }

                WebToon.Add(new Episode() { Name = Title, Parts = parts });
            }
            else
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                Title = dialog.SelectedPath.Split('\\').Last();

                WebToon = new List<Episode>();

                DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath);
                DirectoryInfo[] episodes = dir.GetDirectories();

                foreach (var episode in episodes)
                {
                    List<string> parts = new List<string>();
                    foreach (var part in episode.GetFiles())
                    {
                        parts.Add(part.FullName);
                    }

                    WebToon.Add(new Episode() { Name = episode.Name, Parts = parts });
                }

            }
        }
    }

    class Episode
    {
        public string Name { get; set; }
        public List<string> Parts { get; set; }
    }
}
