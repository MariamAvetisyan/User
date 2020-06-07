using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UserApp.Common
{
    public interface IXmlWorker
    {
        string Serialize<T>(T t);

        T Deserilize<T>(string xml) where T : class;

        string Read();

        void Save(string xml, string path);
    }
}
