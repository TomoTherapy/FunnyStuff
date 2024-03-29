﻿using System;
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
using WebToonViewer.ViewModels;

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

        private void FitImageWidth_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).FitImageWidth_button_Click(ScrollListView);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).Window_Closing();
        }

        private void ScrollSpeedReset_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).ScrollSpeedReset_button_Click();
        }

        private void FolderBrowser_treeView_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindow_ViewModel).FolderBrowser_treeView_Loaded(sender);
        }

        private void FolderBrowser_treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SpeedyScrollViewer.ScrollToTop();
            (DataContext as MainWindow_ViewModel).FolderBrowser_treeView_SelectedItemChanged(sender, ScrollListView);
        }

    }
}
