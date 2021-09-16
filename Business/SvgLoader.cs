using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Gench.Business
{
    public class SvgLoader
    {
        public XDocument LoadDocument(string path)
        {
            XDocument document = XDocument.Load(path);
            return document;
        }
    }
}