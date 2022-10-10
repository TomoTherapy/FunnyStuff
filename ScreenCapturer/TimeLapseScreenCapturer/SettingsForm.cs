using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLapseScreenCapturer
{
    public partial class SettingsForm : Form
    {
        Context context;

        public SettingsForm(Context cont)
        {
            InitializeComponent();

            context = cont;

            Interval_textBox.Text = context.Interval.ToString();
        }

        private void Interval_textBox_TextChanged(object sender, EventArgs e)
        {
            int result = -1;
            if (!int.TryParse(Interval_textBox.Text, out result))
            {
                MessageBox.Show("It's not a number!");
                Interval_textBox.Text = "";
                return;
            }

            context.Interval = result;
        }
    }
}
