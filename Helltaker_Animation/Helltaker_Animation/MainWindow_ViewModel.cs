using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Forms;
using Application = System.Windows.Application;
using System.Windows.Forms.Design;
using System.Threading;
using WMPLib;

namespace Helltaker_Animation
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private List<HellGirl> m_Girls;
        private bool m_Language;
        private NotifyIcon Noti;
        private WMPLib.WindowsMediaPlayer m_Player;

        private int _interval;
        private int _frame;

        public int Interval { get => _interval; set { _interval = value; RaisePropertyChanged(nameof(Interval)); } }
        public int Frame { get => _frame; set { _frame = value; RaisePropertyChanged(nameof(Frame)); } }

        private MainWindow m_Window;

        public MainWindow_ViewModel(MainWindow window)
        {
            m_Window = window;
            m_Language = (Application.Current as App).Language;
            m_Girls = (Application.Current as App).Girls;
            m_Player = new WindowsMediaPlayer();
            m_Player.settings.volume = 50;

            Interval = 49;
            Frame = -1;

            //Timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = Interval;
            timer.Tick += NextFrame;
            timer.Start();

            GenerateNotifyIcon();

            m_Girls.Add(new HellGirl());
            m_Girls.Last().Show();

            m_Window.Close();
        }

        private void GenerateNotifyIcon()
        {
            if (Noti != null) Noti.Dispose();

            ContextMenu Menu = new ContextMenu();
            Noti = new NotifyIcon
            {
                Icon = new Icon(@"Resources\icon.ico", new Size(10, 10)),
                Visible = true,
                Text = m_Language ? "헬테이커" : "Helltaker",
                ContextMenu = Menu
            };

            MenuItem TopMostItem = new MenuItem
            {
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

            #region girls menuItem
            MenuItem SummonGirls = new MenuItem()
            {
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

            MenuItem MalinaItem = new MenuItem() { Text = m_Language ? "말리나" : "Malina" };
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

            MenuItem SkeletonItem = new MenuItem() { Text = m_Language ? "스켈레톤" : "Skeleton" };
            SkeletonItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Skeleton_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(SkeletonItem);

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

            MenuItem SummonAllItem = new MenuItem() { Text = m_Language ? "전부소환" : "Summon all" };
            SummonAllItem.Click += (object o, EventArgs e) =>
            {
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Azazel_button_Click();
                m_Girls.Last().Show();
                for (int i = 0; i < 3; i++)
                {
                    m_Girls.Add(new HellGirl());
                    (m_Girls.Last().DataContext as HellGirl_ViewModel).Cerberus_button_Click();
                    m_Girls.Last().Show();
                }
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Judgement_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Justice_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Lucifer_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).LuciferApron_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Malina_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Modeus_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Pandemonica_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Zdrada_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Skeleton_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).Helltaker_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).HelltakerApron_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).GloriousLeft_button_Click();
                m_Girls.Last().Show();
                m_Girls.Add(new HellGirl());
                (m_Girls.Last().DataContext as HellGirl_ViewModel).GloriousRight_button_Click();
                m_Girls.Last().Show();
            };
            SummonGirls.MenuItems.Add(SummonAllItem);
            #endregion

            #region Lang setting menuItem
            MenuItem LangItem = new MenuItem()
            {
                Text = m_Language ? "언어" : "Language"
            };

            MenuItem LangKoreanItem = new MenuItem()
            {
                Text = "한국어"
            };
            LangKoreanItem.Click += (object o, EventArgs e) =>
            {
                if (!m_Language)
                {
                    (Application.Current as App).SaveLangInfo("Korean");
                    m_Language = true;
                    GenerateNotifyIcon();
                    foreach (HellGirl girl in m_Girls) (girl.DataContext as HellGirl_ViewModel).Naming(m_Language);
                }
            };
            LangItem.MenuItems.Add(LangKoreanItem);

            MenuItem LangEnglishItem = new MenuItem()
            {
                Text = "English"
            };
            LangEnglishItem.Click += (object o, EventArgs e) =>
            {
                if (m_Language)
                {
                    (Application.Current as App).SaveLangInfo("English");
                    m_Language = false;
                    GenerateNotifyIcon();
                    foreach (HellGirl girl in m_Girls) (girl.DataContext as HellGirl_ViewModel).Naming(m_Language);
                }
            };
            LangItem.MenuItems.Add(LangEnglishItem);

            if (m_Language) LangKoreanItem.Checked = true;
            else LangEnglishItem.Checked = true;
            #endregion

            #region BGM menuItem
            MenuItem BGMItem = new MenuItem()
            {
                Text = m_Language ? "배경음악" : "BGM"
            };

            MenuItem AproposItem = new MenuItem()
            {
                Text = "Apropos"
            };
            MenuItem VitalityItem = new MenuItem()
            {
                Text = "Vitality"
            };
            MenuItem EpitomizeItem = new MenuItem()
            {
                Text = "Epitomize"
            };
            MenuItem LuminescentItem = new MenuItem()
            {
                Text = "Luminescent"
            };

            AproposItem.Click += (object o, EventArgs e) =>
            {
                if (AproposItem.Checked)
                {
                    AproposItem.Checked = false;
                    m_Player.controls.stop();
                }
                else
                {
                    m_Player.URL = @"Resources\Mittsies - Apropos.mp3";
                    m_Player.controls.play();
                    m_Player.settings.setMode("loop", true);
                    AproposItem.Checked = true;
                    VitalityItem.Checked = false;
                    EpitomizeItem.Checked = false;
                    LuminescentItem.Checked = false;
                }
            };
            VitalityItem.Click += (object o, EventArgs e) =>
            {
                if (VitalityItem.Checked)
                {
                    VitalityItem.Checked = false;
                    m_Player.controls.stop();
                }
                else
                {
                    m_Player.URL = @"Resources\Mittsies - Vitality.mp3";
                    m_Player.controls.play();
                    m_Player.settings.setMode("loop", true);
                    AproposItem.Checked = false;
                    VitalityItem.Checked = true;
                    EpitomizeItem.Checked = false;
                    LuminescentItem.Checked = false;
                }
            };
            EpitomizeItem.Click += (object o, EventArgs e) =>
            {
                if (EpitomizeItem.Checked)
                {
                    EpitomizeItem.Checked = false;
                    m_Player.controls.stop();
                }
                else
                {
                    m_Player.URL = @"Resources\Mittsies - Epitomize.mp3";
                    m_Player.controls.play();
                    m_Player.settings.setMode("loop", true);
                    AproposItem.Checked = false;
                    VitalityItem.Checked = false;
                    EpitomizeItem.Checked = true;
                    LuminescentItem.Checked = false;
                }
            };
            LuminescentItem.Click += (object o, EventArgs e) =>
            {
                if (LuminescentItem.Checked)
                {
                    LuminescentItem.Checked = false;
                    m_Player.controls.stop();
                }
                else
                {
                    m_Player.URL = @"Resources\Mittsies - Luminescent.mp3";
                    m_Player.controls.play();
                    m_Player.settings.setMode("loop", true);
                    AproposItem.Checked = false;
                    VitalityItem.Checked = false;
                    EpitomizeItem.Checked = false;
                    LuminescentItem.Checked = true;
                }
            };

            MenuItem VolumeControlItem = new MenuItem() { Text = m_Language ? "음량" : "Volume" };

            MenuItem Volume10Item = new MenuItem() { Text = "10" };
            MenuItem Volume20Item = new MenuItem() { Text = "20" };
            MenuItem Volume30Item = new MenuItem() { Text = "30" };
            MenuItem Volume40Item = new MenuItem() { Text = "40" };
            MenuItem Volume50Item = new MenuItem() { Text = "50", Checked = true };
            MenuItem Volume60Item = new MenuItem() { Text = "60" };
            MenuItem Volume70Item = new MenuItem() { Text = "70" };
            MenuItem Volume80Item = new MenuItem() { Text = "80" };
            MenuItem Volume90Item = new MenuItem() { Text = "90" };
            MenuItem Volume100Item = new MenuItem() { Text = "100" };
            Volume10Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 10;
                Volume10Item.Checked = true; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume20Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 20;
                Volume10Item.Checked = false; Volume20Item.Checked = true; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume30Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 30;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = true; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume40Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 40;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = true; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume50Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 50;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = true;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume60Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 60;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = true; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume70Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 70;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = true; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume80Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 80;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = true; Volume90Item.Checked = false; Volume100Item.Checked = false;
            };
            Volume90Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 90;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = true; Volume100Item.Checked = false;
            };
            Volume100Item.Click += (object o, EventArgs e) => 
            { 
                m_Player.settings.volume = 100;
                Volume10Item.Checked = false; Volume20Item.Checked = false; Volume30Item.Checked = false; Volume40Item.Checked = false; Volume50Item.Checked = false;
                Volume60Item.Checked = false; Volume70Item.Checked = false; Volume80Item.Checked = false; Volume90Item.Checked = false; Volume100Item.Checked = true;
            };

            VolumeControlItem.MenuItems.Add(Volume10Item);
            VolumeControlItem.MenuItems.Add(Volume20Item);
            VolumeControlItem.MenuItems.Add(Volume30Item);
            VolumeControlItem.MenuItems.Add(Volume40Item);
            VolumeControlItem.MenuItems.Add(Volume50Item);
            VolumeControlItem.MenuItems.Add(Volume60Item);
            VolumeControlItem.MenuItems.Add(Volume70Item);
            VolumeControlItem.MenuItems.Add(Volume80Item);
            VolumeControlItem.MenuItems.Add(Volume90Item);
            VolumeControlItem.MenuItems.Add(Volume100Item);

            BGMItem.MenuItems.Add(AproposItem);
            BGMItem.MenuItems.Add(VitalityItem);
            BGMItem.MenuItems.Add(EpitomizeItem);
            BGMItem.MenuItems.Add(LuminescentItem);
            BGMItem.MenuItems.Add(VolumeControlItem);
            #endregion

            MenuItem ExitItem = new MenuItem()
            {
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

            Menu.MenuItems.Add(SummonGirls);
            Menu.MenuItems.Add(TopMostItem);
            Menu.MenuItems.Add(BGMItem);
            Menu.MenuItems.Add(LangItem);
            Menu.MenuItems.Add(ExitItem);
            Noti.ContextMenu = Menu;
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
