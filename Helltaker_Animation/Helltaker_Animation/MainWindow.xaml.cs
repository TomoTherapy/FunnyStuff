using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Helltaker_Animation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap original;
        Bitmap[] bitmapFrames = new Bitmap[12];
        ImageSource[] imgSourceFrames = new ImageSource[12];
        //string path = ;
        int frame = -1;

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public MainWindow()
        {
            InitializeComponent();

            //original = new Bitmap(path);
            //System.Drawing.Image.FromFile(path);
            //original = new Bitmap(.Properties.Resources.);

            //((App)Application.Current).Properties.

            //original = (Bitmap)System.Resources.ResourceManager.GetObject("");

            original = new Bitmap(@"C:\Users\crazy\source\repos\Helltaker\Helltaker_Animation\Helltaker_Animation\ImageSources\Azazel.png");

            for (int i = 0; i < 12; i++)
            {
                bitmapFrames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bitmapFrames[i]))
                {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100), new System.Drawing.Rectangle(i * 100, 0, 100, 100), GraphicsUnit.Pixel);
                }

                var handle = bitmapFrames[i].GetHbitmap();
                try
                {
                    imgSourceFrames[i] = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch
                {

                }
                finally
                {
                    DeleteObject(handle);
                }
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.0167 * 2);
            timer.Tick += NextFrame;
            timer.Start();
        }

        private void NextFrame(object sender, EventArgs e)
        {
            frame = (frame + 1) % 12;
            ImageControl.Source = imgSourceFrames[frame];
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
