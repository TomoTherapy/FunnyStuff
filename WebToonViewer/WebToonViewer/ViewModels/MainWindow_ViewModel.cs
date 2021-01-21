using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebToonViewer.ViewModels
{
    class MainWindow_ViewModel : ViewModelBase
    {
        private List<Episode> webtoon;
        private string title;
        private int m_Width;
        private Episode selectedEpisode;
        private bool isLevel2;

        public List<Episode> WebToon { get => webtoon; set { webtoon = value; RaisePropertyChanged(nameof(WebToon)); } }
        public Episode SelectedEpisode { get => selectedEpisode; set { selectedEpisode = value; RaisePropertyChanged(nameof(SelectedEpisode)); } }
        public string Title { get => title; set { title = value; RaisePropertyChanged(nameof(Title)); } }
        public int Width { get => m_Width; set { m_Width = value; RaisePropertyChanged(nameof(Width)); } }
        public bool IsLevel2 { get => isLevel2; set { isLevel2 = value; RaisePropertyChanged(nameof(IsLevel2)); } }

        public MainWindow_ViewModel()
        {

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

                //RaisePropertyChanged("WebToon");

            }
        }
    }

    class Episode
    {
        public string Name { get; set; }
        public List<string> Parts { get; set; }
    }
}
