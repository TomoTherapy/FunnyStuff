using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Helltaker_Animation
{
    class HellGirl_ViewModel : ViewModelBase
    {
        private bool m_Language;
        private HellGirl m_Window;
        private Bitmap m_Original;
        private Bitmap[] m_BitmapFrames = new Bitmap[24];
        private ImageSource[] m_ImgSourceFrames = new ImageSource[24];

        private ImageSource _finalSource;
        private string _girlsName;
        public ImageSource FinalSource { get => _finalSource; set { _finalSource = value; RaisePropertyChanged(nameof(FinalSource)); } }
        public string GirlsName { get => _girlsName; set { _girlsName = value; RaisePropertyChanged(nameof(GirlsName)); } }

        #region name properties
        public string Which { get; set; }
        public string Dismiss { get; set; }
        public string Azazel { get; set; }
        public string Cerberus { get; set; }
        public string Judgement { get; set; }
        public string Justice { get; set; }
        public string Lucifer { get; set; }
        public string LuciferApron { get; set; }
        public string Malina { get; set; }
        public string Modeus { get; set; }
        public string Pandemonica { get; set; }
        public string Zdrada { get; set; }
        public string Helltaker { get; set; }
        public string HelltakerApron { get; set; }
        public string GloriousLeft { get; set; }
        public string GloriousRight { get; set; }
        #endregion

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);


        public HellGirl_ViewModel(HellGirl window)
        {
            m_Language = (Application.Current as App).Language;
            m_Window = window;
            CreateSpriteCollection("Lucifer");

            Naming(m_Language);
        }

        private void CreateSpriteCollection(string girl)
        {
            GirlsName = m_Language ? EnglishToKorean(girl) : girl;

            if (girl.Equals("LuciferApron"))
            {
                string path = @"Resources\Lucifer.png";
                if (!File.Exists(path)) return;
                m_Original = new Bitmap(path);

                for (int i = 0; i < 24; i++)
                {
                    m_BitmapFrames[i] = new Bitmap(100, 100);
                    using (Graphics g = Graphics.FromImage(m_BitmapFrames[i]))
                    {
                        g.DrawImage(m_Original, new Rectangle(0, 0, 100, 100), new Rectangle(i * 100, 100, 100, 100), GraphicsUnit.Pixel);
                    }

                    var handle = m_BitmapFrames[i].GetHbitmap();
                    try
                    {
                        m_ImgSourceFrames[i] = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                    catch
                    {
                        MessageBox.Show("Fucked");
                        break;
                    }
                    finally
                    {
                        DeleteObject(handle);
                    }
                }

                return;
            }

            string path1 = @"Resources\" + girl + ".png";
            if (!File.Exists(path1)) return;
            m_Original = new Bitmap(path1);

            for (int i = 0; i < 24; i++)
            {
                m_BitmapFrames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(m_BitmapFrames[i]))
                {
                    g.DrawImage(m_Original, new Rectangle(0, 0, 100, 100), new Rectangle(i * 100, 0, 100, 100), GraphicsUnit.Pixel);
                }

                var handle = m_BitmapFrames[i].GetHbitmap();
                try
                {
                    m_ImgSourceFrames[i] = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch
                {
                    MessageBox.Show("Fucked");
                    break;
                }
                finally
                {
                    DeleteObject(handle);
                }
            }
        }

        internal void Dismiss_button_Click()
        {
            m_Window.Close();
            (Application.Current as App).Girls.Remove(m_Window);
        }

        public void NextFrame(int frame)
        {
            FinalSource = m_ImgSourceFrames[frame];
        }

        #region 노가다
        internal void Helltaker_button_Click()
        {
            CreateSpriteCollection("Helltaker");
        }

        internal void HelltakerApron_button_Click()
        {
            CreateSpriteCollection("HelltakerApron");
        }

        internal void Lucifer_button_Click()
        {
            CreateSpriteCollection("Lucifer");
        }

        internal void LuciferApron_button_Click()
        {
            CreateSpriteCollection("LuciferApron");
        }

        internal void Malina_button_Click()
        {
            CreateSpriteCollection("Malina");
        }

        internal void Modeus_button_Click()
        {
            CreateSpriteCollection("Modeus");
        }

        internal void Justice_button_Click()
        {
            CreateSpriteCollection("Justice");
        }

        internal void Pandemonica_button_Click()
        {
            CreateSpriteCollection("Pandemonica");
        }

        internal void Zdrada_button_Click()
        {
            CreateSpriteCollection("Zdrada");
        }

        internal void Judgement_button_Click()
        {
            CreateSpriteCollection("Judgement");
        }

        internal void GloriousLeft_button_Click()
        {
            CreateSpriteCollection("Glorious_success_left");
        }

        internal void GloriousRight_button_Click()
        {
            CreateSpriteCollection("Glorious_success_right");
        }

        internal void Cerberus_button_Click()
        {
            CreateSpriteCollection("Cerberus");
        }

        internal void Azazel_button_Click()
        {
            CreateSpriteCollection("Azazel");
        }

        private string EnglishToKorean(string girl)
        {
            string koreanName = "";
            switch (girl)
            {
                case "Azazel": koreanName = "아자젤"; break;
                case "Cerberus": koreanName = "케르베로스"; break;
                case "Lucifer": koreanName = "루시퍼"; break;
                case "Lucifer_Apron": koreanName = "앞치마 루시퍼"; break;
                case "Malina": koreanName = "말리나"; break;
                case "Modeus": koreanName = "모데우스"; break;
                case "Justice": koreanName = "저스티스"; break;
                case "Judgement": koreanName = "젓지먼트"; break;
                case "Pandemonica": koreanName = "판데모니카"; break;
                case "Zdrada": koreanName = "즈드라다"; break;
                case "Helltaker": koreanName = "헬테이커"; break;
                case "Helltaker_Apron": koreanName = "앞치마 헬테이커"; break;
                default: koreanName = girl; break;
            }
            return koreanName;
        }

        private void Naming(bool lang)
        {
            Which = lang ? "누구?" : "Which girl?";
            Dismiss = lang ? "탈출" : "Dismiss";
            Azazel = lang ? "아자젤" : "Azazel";
            Cerberus = lang ? "케르베로스" : "Cerberus";
            Judgement = lang ? "저지먼트" : "Judgement";
            Justice = lang ? "저스티스" : "Justice";
            Lucifer = lang ? "루시퍼" : "Lucifer";
            LuciferApron = lang ? "앞치마 루시퍼" : "Lucifer Apron";
            Malina = lang ? "말리나" : "Malina";
            Modeus = lang ? "모데우스" : "Modeus";
            Pandemonica = lang ? "판데모니카" : "Pandemonica";
            Zdrada = lang ? "크드라다" : "Zdrada";
            Helltaker = lang ? "헬테이커" : "Helltaker";
            HelltakerApron = lang ? "앞치마 헬테이커" : "Helltaker Apron";
            GloriousLeft = lang ? "Glorious 왼쪽" : "Glorious Left";
            GloriousRight = lang ? "Glorious 오른쪽" : "Glorious Right";

            RaisePropertyChanged(nameof(Which));
            RaisePropertyChanged(nameof(Dismiss));
            RaisePropertyChanged(nameof(Azazel));
            RaisePropertyChanged(nameof(Cerberus));
            RaisePropertyChanged(nameof(Judgement));
            RaisePropertyChanged(nameof(Justice));
            RaisePropertyChanged(nameof(Lucifer));
            RaisePropertyChanged(nameof(LuciferApron));
            RaisePropertyChanged(nameof(Malina));
            RaisePropertyChanged(nameof(Modeus));
            RaisePropertyChanged(nameof(Pandemonica));
            RaisePropertyChanged(nameof(Zdrada));
            RaisePropertyChanged(nameof(Helltaker));
            RaisePropertyChanged(nameof(HelltakerApron));
            RaisePropertyChanged(nameof(GloriousLeft));
            RaisePropertyChanged(nameof(GloriousRight));
        }
        #endregion
    }
}
