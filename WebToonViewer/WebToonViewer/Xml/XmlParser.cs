using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebToonViewer.Xml
{
    public class XmlParser
    {
        XmlSerializer SettingSerializer;
        public XmlParser()
        {
            SettingSerializer = new XmlSerializer(typeof(Settings));
        }
    }

    public class Settings
    {

    }
}
