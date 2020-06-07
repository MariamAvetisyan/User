using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UserApp.Common
{
    public class XmlWorker : IXmlWorker
    {
        private XDocument _document;

        public XmlWorker(string path)
        {
            if (!File.Exists(path))
            {
                new XDocument(new XElement("Users")).Save(path);
            }
            _document = XDocument.Load(path);
        }

        public T Deserilize<T>(string xml) where T : class
        {
            T t = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Users"));

            using (TextReader reader = new StringReader(xml))
            {
                t = (T)serializer.Deserialize(reader);
            }

            return t;
        }

        public string Read()
        {
            string xml = _document.ToString();
            return xml;
        }

        public void Save(string xml, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, xml, Encoding.Unicode);
        }

        public string Serialize<T>(T t)
        {
            string xml = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(T), new  XmlRootAttribute("Users"));

            using (TextWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, t);
                xml = sw.ToString();
            }

            return xml;
        }
    }
}
