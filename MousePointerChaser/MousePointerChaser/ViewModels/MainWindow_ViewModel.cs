using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MousePointerChaser.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        private Thread MousePositionTrackerThread;
        private Thread WindowPositionMouseChasingThread;
        private bool IsOn;
        private MainWindow Window;

        private int _mPositionX;
        private int _mPositionY;

        public int MPositionX { get => _mPositionX; set { _mPositionX = value; RaisePropertyChanged(nameof(MPositionX)); } }
        public int MPositionY { get => _mPositionY; set { _mPositionY = value; RaisePropertyChanged(nameof(MPositionY)); } }

        public MainWindow_ViewModel(MainWindow window)
        {
            Window = window;
            IsOn = true;
            MousePositionTrackerThread = new Thread(new ThreadStart(MousePositionTrackerThreadMethod));
            WindowPositionMouseChasingThread = new Thread(new ThreadStart(WindowPositionMouseChasingThreadMethod));

            MousePositionTrackerThread.Start();
            WindowPositionMouseChasingThread.Start();
        }


        public void MousePositionTrackerThreadMethod()
        {
            while (IsOn)
            {
                MPositionX = Cursor.Position.X;
                MPositionY = Cursor.Position.Y;

                Thread.Sleep(20);
            }
        }

        public void WindowPositionMouseChasingThreadMethod()
        {
            while (IsOn)
            {
                Window.Dispatcher.Invoke(new Action(delegate
                {
                    if (MPositionX > Window.Left)
                    {
                        if (MPositionX - Window.Left > 200)
                        {
                            if (MPositionX - Window.Left > 400)
                            {
                                Window.Left = Window.Left + 1.5;
                            }
                            Window.Left = Window.Left + 1;
                        }
                    }
                    else
                    {
                        if (Window.Left - MPositionX > 100)
                        {
                            if (Window.Left - MPositionX > 300)
                            {
                                Window.Left = Window.Left - 1.5;
                            }
                            Window.Left = Window.Left - 1;
                        }
                    }

                    if (MPositionY > Window.Top)
                    {
                        Window.Top++;
                    }
                    else
                    {
                        Window.Top--;
                    }
                }));

                Thread.Sleep(5);
            }

        }
    }
}
