using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

using Gench.Utils;

namespace Gench.Business
{
    public class ModelGeneratorFullModel : ModelGeneratorBase
    {
        public ModelGeneratorFullModel() : base()
        {
        }

        private XDocument GenerateModel(XDocument originalSvg,
                                            List<ColorTarget> targets,
                                            Dictionary<string, XAttribute[]> sortedAttrByKey)
        {
            PartColor generatedColor = new PartColor();
            byte r = GenerateByte(0, 150);
            byte g = GenerateByte(0, 150);
            byte b = GenerateByte(0, 150);
            generatedColor.SetPartColor(r, g, b);

            foreach (var target in targets)
            {
                foreach(var attr in sortedAttrByKey[target.Name])
                {
                    attr.Value = generatedColor.ToString();
                }
                generatedColor.SetPartColor( GenerateByte((byte)(r + 5), (byte)(r + 20))
                                               , GenerateByte((byte)(g + 5), (byte)(g + 20))
                                               , GenerateByte((byte)(b + 5), (byte)(b + 20)) );
            }

            return new XDocument(originalSvg);
        }

        public List<XDocument> GenerateModels(int count,
                                              string modelpartFilename,
                                              string colorTargetFilename)
        {
            ModelPart fullmodel = _partLoader.LoadOne(modelpartFilename);
            List<ColorTarget> targets = _colorTargetLoader.LoadMultiple(colorTargetFilename);
            XDocument originalSvg = _svgLoader.LoadFullModel(fullmodel);
            
            ColorTargetsSvgScanner scanner = new ColorTargetsSvgScanner(targets);
            var attributes = scanner.Scan(originalSvg);

            var sortedAttrByKey = new Dictionary<string, XAttribute[]>();

            try
            {
                foreach(var target in targets)
                {
                    sortedAttrByKey[target.Name] = attributes
                                                .Where(el => el.Name == target.Name)
                                                .Select(el => el.Attribute)
                                                .ToArray();
                }
            }
            catch (System.ArgumentNullException)
            {
                Logger.LogText(this, 
                                    System.Reflection.MethodBase.GetCurrentMethod(), 
                                    "sortedAttr is null or contains null elements");
                return null;
            }

            List<XDocument> models = new List<XDocument>();
            PartColor generatedColor = new PartColor();
            
            for (int i = 0; i < count; i++)
            {       
                models.Add(GenerateModel(originalSvg, targets, sortedAttrByKey));

                RestoreDocumentAttributes(targets, sortedAttrByKey);
                //restore to default;
            }

            return models;
        }
    }
}