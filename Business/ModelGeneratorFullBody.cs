using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace Gench.Business
{
    public class ModelGeneratorFullBody : ModelGeneratorBase
    {
        public ModelGeneratorFullBody() : base()
        {
        }

        public List<XDocument> GenerateModels(int count, string modelpartFilename, string colorTargetFilename)
        {
            List<XDocument> models = new List<XDocument>();
            PartColor generatedColor = new PartColor();

            ModelPart fullbody = _partLoader.LoadPart(modelpartFilename);
            List<ColorTarget> targets = _colorTargetLoader.LoadColorTargets(colorTargetFilename);
            ColorTargetsSvgScanner scanner = new ColorTargetsSvgScanner(targets);

            XDocument svg = null;
            if (fullbody.RelativePath)
            {
                svg = _svgLoader.LoadDocument(AppContext.BaseDirectory.ToString() + fullbody.Path);
            }
            else
            {
                svg = _svgLoader.LoadDocument(fullbody.Path);
            }

            XDocument svgCopy;

            for (int i = 0; i < count; i++)
            {
                svgCopy = new XDocument(svg);
                
                byte r = GenerateByte(0, 150);
                byte g = GenerateByte(0, 150);
                byte b = GenerateByte(0, 150);
                generatedColor.SetPartColor(r, g, b);

                var attributes = scanner.Scan(svgCopy.Root);
                
                foreach (var target in targets)
                {
                    foreach (var attr in attributes)
                    {
                        if (attr.Value == target.Color.ToString())
                        {
                            attr.Value = generatedColor.ToString();
                        }

                    }
                    generatedColor.SetPartColor(GenerateByte((byte)(r + 5), (byte)(r + 20)), GenerateByte((byte)(g + 5), (byte)(g + 20)), GenerateByte((byte)(b + 5), (byte)(b + 20)));
                }
                models.Add(svgCopy);
            }

            return models;
        }
    }
}