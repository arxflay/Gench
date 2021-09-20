using System.Xml.Linq;
using System.Collections.Generic;
using Gench.Utils;

namespace Gench.Loaders
{
    public class SvgLoader
    {
        public XDocument LoadDocument(string path)
        {
            XDocument document = null;
            try
            {
                document = XDocument.Load(path);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }
            return document;
        }

        public IEnumerable<XDocument> LoadMultipleDocuments(string[] paths)
        {
            foreach(string path in paths)
            {
                yield return LoadDocument(path);
            }
        }
    }
}