using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MousePointerChaser.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        public int PositionX { get => Cursor.Position.X; set { PositionX = Cursor.Position.X; RaisePropertyChanged(nameof(PositionX)); } }
        public int PositionY { get => Cursor.Position.Y; set { PositionY = Cursor.Position.Y; RaisePropertyChanged(nameof(PositionY)); } }

        public MainWindow_ViewModel()
        {
            //Cursor.Position.X
        }

    }
}
