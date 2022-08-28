using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WebToonViewer.ViewModels
{
    class MainWindow_ViewModel : ViewModelBase
    {
        private Xml.Settings settings;
        private List<Episode> webtoon;
        private Episode selectedEpisode;
        private object dummyNode = null;

        public List<Episode> WebToon { get => webtoon; set { webtoon = value; RaisePropertyChanged(nameof(WebToon)); } }
        public Episode SelectedEpisode { get => selectedEpisode; set { selectedEpisode = value; RaisePropertyChanged(nameof(SelectedEpisode)); } }
        public int Width { get => settings.Width; set { settings.Width = value; RaisePropertyChanged(nameof(Width)); } }
        public double SpeedFactor { get => settings.SpeedFactor; set { settings.SpeedFactor = value; RaisePropertyChanged(nameof(SpeedFactor)); } }
        public string SelectedImagePath { get; set; }
        public int ScrollViewWidth { get; set; }

        public MainWindow_ViewModel()
        {
            settings = ((App)System.Windows.Application.Current).Xml.Settings;

        }

        internal void FolderBrowser_treeView_Loaded(object sender)
        {
            List<string> specials = new List<string>();
            specials.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            specials.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            specials.Add(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
            specials.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            specials.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            specials.Add(Environment.GetEnvironmentVariable("OneDriveConsumer"));
            foreach (string s in specials)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                (sender as TreeView).Items.Add(item);
            }

            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                (sender as TreeView).Items.Add(item);
            }
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        internal void FolderBrowser_treeView_SelectedItemChanged(object sender, ListView ScrollListView)
        {
            try
            {
                TreeView tree = (TreeView)sender;
                TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

                if (temp == null)
                    return;
                SelectedImagePath = "";
                string temp1 = "";
                string temp2 = "";
                while (true)
                {
                    temp1 = temp.Header.ToString();
                    if (temp1.Contains(@"\"))
                    {
                        temp1 = temp1 + '\\';
                        temp2 = "";
                    }
                    SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                    if (temp.Parent.GetType().Equals(typeof(TreeView)))
                    {
                        break;
                    }
                    temp = ((TreeViewItem)temp.Parent);
                    temp2 = @"\";
                }

                WebToon = new List<Episode>();

                DirectoryInfo dir = new DirectoryInfo(SelectedImagePath);
                List<string> parts = new List<string>();
                FileInfo[] files = dir.GetFiles().OrderBy(n => Regex.Replace(n.Name, @"\d+", a => a.Value.PadLeft(4, '0'))).ToArray();
                foreach (var part in files)
                {
                    string ext = part.Extension.ToUpper();
                    if (ext == ".JPEG" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP" || ext == ".WEBP")
                        parts.Add(part.FullName);
                }

                SelectedEpisode = new Episode() { Name = "", Parts = parts };
                FitImageWidth_button_Click(ScrollListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void FitImageWidth_button_Click(ListView ScrollListView)
        {
            try
            {
                if (SelectedEpisode == null) return;
                if (SelectedEpisode.Parts.Count == 0) return;

                Bitmap bmp = new Bitmap(SelectedEpisode.Parts[0]);
                if (bmp.Width > ScrollListView.ActualWidth)
                    Width = (int)ScrollListView.ActualWidth;
                else
                    Width = bmp.Width;
                bmp.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal void Window_Closing()
        {
            ((App)Application.Current).Xml.SettingsSave();
        }

        internal void ScrollSpeedReset_button_Click()
        {
            SpeedFactor = 2.5;
        }

    }

    public class Episode
    {
        public string Name { get; set; }
        public List<string> Parts { get; set; }
    }
}
