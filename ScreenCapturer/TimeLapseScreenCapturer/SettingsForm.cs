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
            SavePath_textBox.Text = context.SavePath.ToString();
        }

        private void Interval_textBox_TextChanged(object sender, EventArgs e)
        {
            int result = -1;
            if (!int.TryParse(Interval_textBox.Text, out result))
            {
                if (Interval_textBox.Text == "") return;
                MessageBox.Show("NaN!");
                Interval_textBox.Text = Interval_textBox.Text.Substring(0, Interval_textBox.Text.Length - 1);
                return;
            }

            context.Interval = result;
        }

        private void SavePath_textBox_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            SavePath_textBox.Text = dlg.SelectedPath;
            context.SavePath = dlg.SelectedPath;
        }
    }
}
