using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeLapseScreenCapturer.Properties;

namespace TimeLapseScreenCapturer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }

        public class MyCustomApplicationContext : ApplicationContext
        {
            private NotifyIcon trayIcon;
            private Timer timer;
            private bool isRunning;

            public MyCustomApplicationContext()
            {
                isRunning = false;

                // Initialize Tray Icon
                trayIcon = new NotifyIcon() { Icon = new Icon("Resources/icon.ico"), Visible = true };

                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add(new MenuItem("TimeLapse Capture", StartTimeLapseCapture) { Name = "TimeLapse Capture" });
                contextMenu.MenuItems.Add(new MenuItem("Exit", Exit) { Name = "Exit" });
                trayIcon.ContextMenu = contextMenu;
            }

            private void StartTimeLapseCapture(object sender, EventArgs e)
            {
                isRunning = !isRunning;

                if (isRunning)
                {
                    trayIcon.ContextMenu.MenuItems["TimeLapse Capture"].Checked = true;

                    timer = new Timer() { Interval = 10000 };
                    timer.Tick += new EventHandler(OnTimerEvent);
                    timer.Start();
                }
                else
                {
                    trayIcon.ContextMenu.MenuItems["TimeLapse Capture"].Checked = false;
                    timer.Stop();
                    timer.Dispose();
                }
            }

            private void OnTimerEvent(object sender, EventArgs e)
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var gfxScreenShot = Graphics.FromImage(bmp);

                gfxScreenShot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Captures", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
                bmp.Save(path, ImageFormat.Png);
                bmp.Dispose();
            }

            private void Exit(object sender, EventArgs e)
            {
                trayIcon.Visible = false;
                Application.Exit();
            }
        }
    }
}
