using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Gench.Utils;

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
            System.Reflection.MethodBase currentMethod = null;
            
            if (svgs == null)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogText(this, currentMethod, $"SVG XDocument List is null");
                return;
            }

            int count = svgs.Count;

            for (int i = 0; i < count; i++)
            {
                try
                {
                    await Task.Run(() => AsyncWrite(path, i + ".svg", svgs[i]));
                }
                catch (System.NullReferenceException)
                {
                    currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                    Logger.LogText(this, currentMethod, $"SVG XDocument {i} in List null");
                }
            }
        }
    }
}