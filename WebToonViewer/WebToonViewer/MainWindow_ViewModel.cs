using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebToonViewer
{
    class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Episode> m_Webtoon;
        private string m_Title;
        private int m_Width;

        public List<Episode> WebToon
        {
            get { return m_Webtoon; }
            set
            {
                if (m_Webtoon != value)
                {
                    m_Webtoon = value;
                    RaisePropertyChanged("Webtoon");
                }
            }
        }

        public string Title
        {
            get { return m_Title; }
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public int Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                    RaisePropertyChanged("Width");
                }
            }
        }


        public void OpenWebToonFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
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

                RaisePropertyChanged("WebToon");
                    
            }


        }









    }

    class Episode
    {
        public string Name { get; set; }
        public List<string> Parts { get; set; }
    }
}
