using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WebToonViewer.Xml
{
    public class XmlParser
    {
        public Settings Settings;
        private XmlSerializer SettingSerializer;
        public XmlParser()
        {
            SettingSerializer = new XmlSerializer(typeof(Settings));
            SettingsLoad();
        }

        public void SettingsLoad()
        {
            try
            {
                string path = @"ViewerSettings.xml";
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        Settings = SettingSerializer.Deserialize(reader) as Settings;
                    }
                }
                else
                {
                    Settings = new Settings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Settings = new Settings();
            }
        }

        public void SettingsSave()
        {
            try
            {
                string path = @"ViewerSettings.xml";

                using (StreamWriter writer = new StreamWriter(path))
                {
                    SettingSerializer.Serialize(writer, Settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class Settings
    {
        public int Width { get; set; }
        public bool IsLevel2 { get; set; }
        public double SpeedFactor { get; set; }

        public Settings()
        {
            Width = 700;
            IsLevel2 = true;
            SpeedFactor = 2.5;
        }
    }
}
