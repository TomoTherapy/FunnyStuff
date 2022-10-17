using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLapseScreenCapturer.Properties;

namespace TimeLapseScreenCapturer
{
    internal class JsonParser
    {
        private JsonSerializer serializer;
        private Settings settings;
        public Settings Settings { get => settings; set => settings = value; }

        public JsonParser()
        {
            settings = new Settings();
            serializer = new JsonSerializer();
        }

        public void SerializeSettings()
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TimeLapseScreenCapturerDoc";
            Directory.CreateDirectory(path);

            using (StreamWriter sw = new StreamWriter(path + @"\TimeLapseScreenCapturerSettings.json"))
            {
                serializer.Serialize(sw, settings);
            }
        }

        public void DeserializeSettings()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TimeLapseScreenCapturerDoc";
            Directory.CreateDirectory(path);

            if (File.Exists(path + @"\TimeLapseScreenCapturerSettings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                string json = "";
                using (StreamReader sr = new StreamReader(path + @"\TimeLapseScreenCapturerSettings.json"))
                {
                    json = sr.ReadToEnd();
                }

                settings = JsonConvert.DeserializeObject<Settings>(json);
            }

            if (settings == null)
            {
                settings = new Settings();
            }
        }
    }

    public class Settings
    {
        public int Interval { get; set; }
        public string SavePath { get; set; }

        public Settings()
        {
            Interval = 5000;
            SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Capture");
        }
    }
}
