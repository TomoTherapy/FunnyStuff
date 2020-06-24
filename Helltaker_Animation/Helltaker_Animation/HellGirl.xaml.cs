using System;
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
using System.Windows.Shapes;

namespace Helltaker_Animation
{
    /// <summary>
    /// Interaction logic for HellGirl.xaml
    /// </summary>
    public partial class HellGirl : Window
    {
        public HellGirl()
        {
            InitializeComponent();

            DataContext = new HellGirl_ViewModel(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Dismiss_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Dismiss_button_Click();
        }

        private void Azazel_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Azazel_button_Click();
        }

        private void Cerberus_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Cerberus_button_Click();
        }

        private void Judgement_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Judgement_button_Click();
        }

        private void Justice_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Justice_button_Click();
        }

        private void Lucifer_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Lucifer_button_Click();
        }

        private void Lucifer_Apron_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Lucifer_Apron_button_Click();
        }

        private void Malina_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Malina_button_Click();
        }

        private void Modeus_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Modeus_button_Click();
        }

        private void Pandemonica_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Pandemonica_button_Click();
        }

        private void Zdrada_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Zdrada_button_Click();
        }

        private void Glorious_Left_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Glorious_Left_button_Click();
        }

        private void Glorious_Right_button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as HellGirl_ViewModel).Glorious_Right_button_Click();
        }
    }
}
