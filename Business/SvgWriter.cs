using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Gench.Business
{
    public class SvgWriter
    {
        public void Write(string path, string filename, XDocument svg)
        {
            svg.Save(path + filename);
        }

        public async Task AsyncWrite(string path, string filename, XDocument svg)
        {
            await Task.Run(() => Write(path, filename, svg));
        }

        public async Task AsyncWriteMultiple(string path, List<XDocument> svgs)
        {
            int count = svgs.Count;
            for (int i = 0; i < count; i++)
            {
                await Task.Run(() => AsyncWrite(path, i + ".svg", svgs[i]));
            }
        }
    }
}