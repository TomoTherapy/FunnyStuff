using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Forms;
using Application = System.Windows.Application;
using System.Windows.Forms.Design;

namespace Helltaker_Animation
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private List<HellGirl> m_Girls;
        private bool m_Language;

        private double _interval;
        private int _frame;

        public double Interval { get => _interval; set { _interval = value; RaisePropertyChanged(nameof(Interval)); } }
        public int Frame { get => _frame; set { _frame = value; RaisePropertyChanged(nameof(Frame)); } }

        private MainWindow m_Window;

        public MainWindow_ViewModel(MainWindow window)
        {
            m_Language = (Application.Current as App).Language;
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
                Icon = new Icon(@"Resources\icon.ico", new Size(10, 10)),
                Visible = true,
                Text = m_Language ? "헬테이커" : "Helltaker",
                ContextMenu = menu
            };

            var TopMostItem = new MenuItem
            {
                Index = 1,
                Text = m_Language ? "최상위고정" : "Top Most",
                Checked = true
            };
            TopMostItem.Click += (object o, EventArgs e) =>
            {
                if (m_Window.Topmost)
                {
                    m_Window.Topmost = false;
                    foreach (var girl in (Application.Current as App).Girls)
                        girl.Topmost = false;

                    TopMostItem.Checked = false;
                }
                else
                {
                    m_Window.Topmost = true;
                    foreach (var girl in (Application.Current as App).Girls)
                        girl.Topmost = true;

                    TopMostItem.Checked = true;
                }
            };

            #region girls menuitem
            MenuItem SummonGirls = new MenuItem()
            {
                Index = 2,
                Text = m_Language ? "소환" : "Summon"
            };

            MenuItem AzazelItem = new MenuItem() { Text = m_Language ? "아자젤" : "Azazel" };
            AzazelItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Azazel_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(AzazelItem);

            MenuItem CerberusItem = new MenuItem() { Text = m_Language ? "케르베로스" : "Cerberus" };
            CerberusItem.Click += (object o, EventArgs e) =>
            {
                for (int i = 0; i < 3; i++)
                {
                    m_Girls.Add(new HellGirl());
                    (m_Girls.Last().DataContext as HellGirl_ViewModel).Cerberus_button_Click();
                    m_Girls.Last().Show();
                }
            };
            SummonGirls.MenuItems.Add(CerberusItem);

            MenuItem JudgementItem = new MenuItem() { Text = m_Language ? "저지먼트" : "Judgement" };
            JudgementItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Judgement_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(JudgementItem);

            MenuItem JusticeItem = new MenuItem() { Text = m_Language ? "저스티스" : "Justice" };
            JusticeItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Justice_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(JusticeItem);

            MenuItem LuciferItem = new MenuItem() { Text = m_Language ? "루시퍼" : "Lucifer" };
            LuciferItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Lucifer_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(LuciferItem);

            MenuItem LuciferApronItem = new MenuItem() { Text = m_Language ? "앞치마 루시퍼" : "Lucifer Apron" };
            LuciferApronItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).LuciferApron_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(LuciferApronItem);

            MenuItem MalinaItem = new MenuItem() { Text = m_Language ? "말리나" : "Malina"};
            MalinaItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Malina_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(MalinaItem);

            MenuItem ModeusItem = new MenuItem() { Text = m_Language ? "모데우스" : "Modeus" };
            ModeusItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Modeus_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(ModeusItem);

            MenuItem PandemonicaItem = new MenuItem() { Text = m_Language ? "판데모니카" : "Pandemonica" };
            PandemonicaItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Pandemonica_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(PandemonicaItem);

            MenuItem ZdradaItem = new MenuItem() { Text = m_Language ? "즈드라다" : "Zdrada" };
            ZdradaItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Zdrada_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(ZdradaItem);

            MenuItem GloriousLeftItem = new MenuItem() { Text = m_Language ? "Glorious 왼쪽" : "Glorious Left" };
            GloriousLeftItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).GloriousLeft_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(GloriousLeftItem);

            MenuItem GloriousRightItem = new MenuItem() { Text = m_Language ? "Glorious 오른쪽" : "Glorious Right" };
            GloriousRightItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).GloriousRight_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(GloriousRightItem);

            MenuItem HelltakerItem = new MenuItem() { Text = m_Language ? "헬테이커" : "Helltaker" };
            HelltakerItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Helltaker_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(HelltakerItem);

            MenuItem HelltakerApronItem = new MenuItem() { Text = m_Language ? "앞치마 헬테이커" : "Helltaker Apron" };
            HelltakerApronItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).HelltakerApron_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(HelltakerApronItem);
            #endregion

            #region Lang setting menuitem
            MenuItem LangItem = new MenuItem()
            {
                Index = 2,
                Text = m_Language ? "언어" : "Language"
            };

            MenuItem LangKoreanItem = new MenuItem()
            {
                Text = "한국어"
            };
            LangKoreanItem.Click += (object o, EventArgs e) =>
            {
                (Application.Current as App).SaveLangInfo("Korean");
                MessageBox.Show("적용하려면 재시작 하세용");
            };
            LangItem.MenuItems.Add(LangKoreanItem);

            MenuItem LangEnglishItem = new MenuItem()
            {
                Text = "English"
            };
            LangEnglishItem.Click += (object o, EventArgs e) =>
            {
                (Application.Current as App).SaveLangInfo("English");
                MessageBox.Show("Restart the app to apply the language setting");
            };
            LangItem.MenuItems.Add(LangEnglishItem);
            #endregion

            MenuItem ExitItem = new MenuItem()
            {
                Index = 3,
                Text = m_Language ? "탈출" : "Exit"
            };
            ExitItem.Click += (object o, EventArgs e) =>
            {
                foreach (var girl in m_Girls)
                {
                    girl.Close();
                }
                m_Girls.Clear();
                m_Window.Close();
            };

            TrackBar trackBar = new TrackBar()
            {

            };

            /////////////////////////////
            MenuItem aaaa = new MenuItem();
            aaaa.

            menu.MenuItems.Add(SummonGirls);
            menu.MenuItems.Add(TopMostItem);
            menu.MenuItems.Add(LangItem);
            menu.MenuItems.Add(ExitItem);

            noti.ContextMenu = menu;

            m_Girls.Add(new HellGirl());
            m_Girls.Last().Show();

            m_Window.Close();
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
