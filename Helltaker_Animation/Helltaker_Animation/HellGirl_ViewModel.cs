using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Bitmap m_Original;
        private Bitmap[] m_BitmapFrames = new Bitmap[24];
        private ImageSource[] m_ImgSourceFrames = new ImageSource[24];

        private ImageSource _finalSource;
        public ImageSource FinalSource { get => _finalSource; set { _finalSource = value; RaisePropertyChanged(nameof(FinalSource)); } }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        private HellGirl m_Window;

        public HellGirl_ViewModel(HellGirl window)
        {
            m_Window = window;
            CreateSpriteCollection("Lucifer");
        }

        private void CreateSpriteCollection(string girl)
        {
            if (girl.Equals("Lucifer_Apron"))
            {
                m_Original = new Bitmap(@"Resources\Lucifer.png");

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

            m_Original = new Bitmap(@"Resources\" + girl + ".png");

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

        internal void Lucifer_button_Click()
        {
            CreateSpriteCollection("Lucifer");
        }

        internal void Lucifer_Apron_button_Click()
        {
            CreateSpriteCollection("Lucifer_Apron");
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

        internal void Glorious_Left_button_Click()
        {
            CreateSpriteCollection("Glorious_success_left");
        }

        internal void Glorious_Right_button_Click()
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

        internal void Dismiss_button_Click()
        {
            m_Window.Close();
            (Application.Current as App).Girls.Remove(m_Window);
        }

        public void NextFrame(int frame)
        {
            FinalSource = m_ImgSourceFrames[frame];
        }

    }
}
