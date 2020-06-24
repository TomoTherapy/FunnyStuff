using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace Helltaker_Animation
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private List<HellGirl> m_Girls;

        private double _interval;
        private int _frame;

        public double Interval { get => _interval; set { _interval = value; RaisePropertyChanged(nameof(Interval)); } }
        public int Frame { get => _frame; set { _frame = value; RaisePropertyChanged(nameof(Frame)); } }

        private MainWindow m_Window;

        public MainWindow_ViewModel(MainWindow window)
        {
            m_Window = window;

            m_Girls = (Application.Current as App).Girls;

            Interval = 0.0167 * 3;
            Frame = -1;

            //Timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(Interval);
            timer.Tick += NextFrame;
            timer.Start();

            var menu = new ContextMenu();
            var noti = new NotifyIcon
            {
                //Icon = System.Drawing.Icon.FromHandle(bitmapFrames[0].GetHicon()),
                Icon = new Icon(@"Resources\icon.ico", new System.Drawing.Size(10, 10)),
                Visible = true,
                Text = "Helltaker",
                ContextMenu = menu
            };

            var TopMost_item = new MenuItem
            {
                Index = 1,
                Text = "TopMost",
                Checked = true
            };
            TopMost_item.Click += (object o, EventArgs e) =>
            {
                if (m_Window.Topmost)
                {
                    m_Window.Topmost = false;
                    foreach (var girl in (Application.Current as App).Girls)
                        girl.Topmost = false;

                    TopMost_item.Checked = false;
                }
                else
                {
                    m_Window.Topmost = true;
                    foreach (var girl in (Application.Current as App).Girls)
                        girl.Topmost = true;

                    TopMost_item.Checked = true;
                }
            };

            var exit_item = new MenuItem
            {
                Index = 2,
                Text = "Exit",
            };
            exit_item.Click += (object o, EventArgs e) =>
            {
                foreach (var girl in m_Girls)
                {
                    girl.Close();
                }
                m_Girls.Clear();

                m_Window.Close();
            };
            var summon_girls = new MenuItem
            {
                Index = 2,
                Text = "Summon"
            };

            var Azazel_item = new MenuItem
            {
                Text = "Azazel"
            };
            Azazel_item.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Azazel_button_Click();

                m_Girls.Last().Show();
            };

            var Cerberus_item = new MenuItem
            {
                Text = "Cerberus"
            };
            Cerberus_item.Click += (object o, EventArgs e) =>
            {
                for (int i = 0; i < 3; i++)
                {
                    m_Girls.Add(new HellGirl());
                    (m_Girls.Last().DataContext as HellGirl_ViewModel).Cerberus_button_Click();
                    m_Girls.Last().Show();
                }
            };




            summon_girls.MenuItems.Add(Azazel_item);
            summon_girls.MenuItems.Add(Cerberus_item);



            menu.MenuItems.Add(summon_girls);
            menu.MenuItems.Add(TopMost_item);
            menu.MenuItems.Add(exit_item);

            noti.ContextMenu = menu;

            m_Girls.Add(new HellGirl());
            m_Girls.Last().Show();
        }

        private void NextFrame(object sender, EventArgs e)
        {
            Frame = (Frame + 1) % 24;

            foreach (var girl in m_Girls)
            {
                (girl.DataContext as HellGirl_ViewModel).NextFrame(Frame);
            }
        }

    }
}
