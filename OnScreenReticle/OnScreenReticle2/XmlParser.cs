using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OnScreenReticle2
{
    public class XmlParser
    {
        private string path = @"Settings.xml";
        private XmlSerializer SettingsSerializer;
        public Settings settings { get; set; }

        public XmlParser()
        {
            SettingsSerializer = new XmlSerializer(typeof(Settings));
        }

        public void SaveSettings()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    SettingsSerializer.Serialize(writer, settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadSettings()
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        settings = SettingsSerializer.Deserialize(reader) as Settings;
                    }
                }
                else
                {
                    settings = new Settings();
                    NewSettings();
                }
            }
            catch
            {
                settings = new Settings();
                NewSettings();
            }
        }

        private void NewSettings()
        {
            settings.ColorR = 255;
            settings.ColorG = 50;
            settings.ColorB = 50;
            settings.ColorA = 255;
            settings.ReticleSize = 6;
            settings.WindowTop = Screen.PrimaryScreen.Bounds.Height * 0.5 - 50;
            settings.WindowLeft = Screen.PrimaryScreen.Bounds.Width * 0.5 - 50;
            settings.Visibility = true;
        }

    }

    public class Settings
    {
        public double ReticleSize { get; set; }
        public double WindowTop { get; set; }
        public double WindowLeft { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }
        public int ColorA { get; set; }
        public bool Visibility { get; set; }
    }
}
