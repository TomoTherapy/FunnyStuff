using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Helltaker_Animation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool Language { get; set; }
        public List<HellGirl> Girls { get; set; }
        public App()
        {
            if (File.Exists(@"lang.txt"))
            {
                using (StreamReader sr = new StreamReader(@"lang.txt"))
                {
                    string lang = sr.ReadLine();
                    if (lang.Equals("Korean"))
                        Language = true;
                    else
                        Language = false;
                    sr.Close();
                }
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("KR"))
                {
                    SaveLangInfo("Korean");
                    Language = true;
                }
                else
                {
                    SaveLangInfo("English");
                    Language = false;
                }
            }

            Girls = new List<HellGirl>();
        }

        public void SaveLangInfo(string lang)
        {
            using (StreamWriter sw = new StreamWriter(@"lang.txt"))
            {
                sw.Write(lang);
                sw.Close();
            }
        }
    }
}
