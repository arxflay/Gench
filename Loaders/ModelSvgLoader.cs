using System;
using System.Xml.Linq;
using Gench.Utils;

using Gench.Business;

namespace Gench.Loaders
{
    public class ModelSvgLoader : SvgLoader
    {
        public ModelSvgLoader()
        {
        }

        public XDocument LoadFullModel(ModelPart fullbody)
        {
            XDocument svg = null;
            System.Reflection.MethodBase currentMethod = null;
            try
            {
                if (fullbody.RelativePath)
                {
                    svg = LoadDocument(AppContext.BaseDirectory.ToString() + fullbody.Path);
                }
                else
                {
                    svg = LoadDocument(fullbody.Path);
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }
            catch (System.NullReferenceException e)
            {
                e.ToString();
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogText(this, currentMethod, "ModelPart is null");
            }

            return svg;
        }

    }
}