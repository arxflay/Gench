using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Gench.Business;
using Gench.Loaders;
using Gench.Utils;
namespace Gench.Business
{
    public class ModelSvgGluer
    {

        public IEnumerable<ModelGluingPart> PreloadGluingParts (XDocument[] svgs, ModelPartInfo[] partsInfo)
        {
            for (int i = 0; i < svgs.Length; i++)
            {
                XElement gElement = svgs[i].Root.Element(SvgTagPaths.GPath);
                XElement useElement = XElement.Parse($"<use id=\"{partsInfo[i].Name + i}\" " + 
                                             $"x=\"{partsInfo[i].Position.X}\" "             +
                                             $"y=\"{partsInfo[i].Position.Y}\" "             +
                                             $"href=\"#{partsInfo[i].Name}\" />");
                                             
                yield return new ModelGluingPart(useElement, gElement, partsInfo[i]);
            }
        }
        //otherSvgs and othersParts must be in the same order
        public IEnumerable<ModelGluingPart> GlueModelParts(XDocument mainSvg, ModelGluingPart[] gluingParts)
        {
            var rootEl = mainSvg.Root;
            var useStorageEl = rootEl.Element(SvgTagPaths.GPath);
            var contentStorageEl = rootEl.Element(SvgTagPaths.DefsPath);

            for (int i = 0; i < gluingParts.Length; i++)
            {
                if (gluingParts[i] == null)
                {
                    throw new System.NullReferenceException();
                }

                ModelGluingPart gluingPart = new ModelGluingPart(gluingParts[i]);
                useStorageEl.Add(gluingPart.UseTag);
                contentStorageEl.Add(gluingPart.Content);

                yield return gluingPart;
            }
        }

        public void UnglueModelParts(ModelGluingPart[] e)
        {
            for (int i = 0; i < e.Length; i++)
            {
                e[i].Remove();
            }
        }
    }
}