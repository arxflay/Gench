using System;
using System.Xml.Linq;
using System.Collections.Generic;
using Gench.Utils;

using Gench.Business;

namespace Gench.Loaders
{
    public class ModelPartSvgLoader
    {
        public ModelPartSvgLoader()
        {
            _svgLoader = new SvgLoader();
        }
        SvgLoader _svgLoader;

        public XDocument LoadModelSvg(ModelPartInfo modelPart)
        {
            XDocument svg = null;
            System.Reflection.MethodBase currentMethod = null;
            try
            {
                if (modelPart.RelativePath)
                {
                    svg = _svgLoader.LoadDocument(AppContext.BaseDirectory.ToString() + modelPart.Path);
                }
                else
                {
                    svg = _svgLoader.LoadDocument(modelPart.Path);
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }
            catch (System.NullReferenceException)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogText(this, currentMethod, "ModelPart is null");
            }

            return svg;
        }

        public IEnumerable<XDocument> LoadMultipleModelsSvg(IEnumerable<ModelPartInfo> modelParts)
        {
            foreach (var model in modelParts)
            {      
                yield return LoadModelSvg(model);
            }
        }

    }
}