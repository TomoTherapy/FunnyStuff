using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebToonViewer.Devices
{
    class SpeedyScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty SpeedFactorProperty =
        DependencyProperty.Register(nameof(SpeedFactor),
                                    typeof(double),
                                    typeof(SpeedyScrollViewer),
                                    new PropertyMetadata(2.5));

        public double SpeedFactor
        {
            get { return (double)GetValue(SpeedFactorProperty); }
            set { SetValue(SpeedFactorProperty, value); }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (!e.Handled &&
                ScrollInfo is ScrollContentPresenter scp &&
                ComputedVerticalScrollBarVisibility == Visibility.Visible)
            {
                scp.SetVerticalOffset(VerticalOffset - e.Delta * SpeedFactor);
                e.Handled = true;
            }
        }
    }
}
