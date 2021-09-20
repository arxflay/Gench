using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;
using Gench.Utils;

namespace Gench.Business
{
    public class ModelGenerator : ModelGeneratorBase
    {
        private void GenerateColors(XDocument originalSvg,
                                        IEnumerable<ColorTarget> targets,
                                        Dictionary<string, ColorTargetScannerMatch[]> sortedMatchesByKey)
        {
            PartColor generatedColor = new PartColor();
            byte r = 0;
            byte g = 0;
            byte b = 0;
            foreach (var target in targets)
            {
                if (!sortedMatchesByKey.ContainsKey(target.OwnerGroup))
                    continue;

                if (target.GenAffectedByPrevTarget)
                {
                    r = GenerateByte((byte)(r + target.MinGenColorRange.R),
                                     (byte)(r + target.MaxGenColorRange.R));

                    g = GenerateByte((byte)(g + target.MinGenColorRange.G),
                                     (byte)(g + target.MaxGenColorRange.G));

                    b = GenerateByte((byte)(b + target.MinGenColorRange.B),
                                     (byte)(b + target.MaxGenColorRange.B));
                }
                else
                {
                    r = GenerateByte(target.MinGenColorRange.R, target.MaxGenColorRange.R);
                    g = GenerateByte(target.MinGenColorRange.G, target.MaxGenColorRange.G);
                    b = GenerateByte(target.MinGenColorRange.B, target.MaxGenColorRange.B);
                }

                generatedColor.SetPartColor(r, g, b);

                foreach(var colorTargetScannerMatch in sortedMatchesByKey[target.OwnerGroup])
                {
                    colorTargetScannerMatch.Attribute.Value = generatedColor.ToString();
                }
            }
        }

        private IEnumerable<ModelGluingPart> RandomlySelectParts (Dictionary<string, ModelGluingPart[]> partsSortedByGroup)
        {
            if (partsSortedByGroup == null)
            {
                throw new NullReferenceException();
            }

            foreach (var key in partsSortedByGroup.Keys)
            {
                yield return partsSortedByGroup[key][_prngGenerator.Next(0, partsSortedByGroup[key].Length)];
            }
        }

        private XDocument GenerateModel(XDocument originalSvg,
                ColorTarget[] colorTargets,
                ModelPartInfo mainModelPartInfo,
                Dictionary<string, Dictionary<string, ColorTargetScannerMatch[]>> colorTargetMatchesByGroup,
                Dictionary<string, ModelGluingPart[]> gluingPartsSortedByGroups)
        {
            var selectedPartsAttrSorted = new Dictionary<string, ColorTargetScannerMatch[]>();
            XDocument model = null;
            ModelGluingPart[] gluedParts = null;

            if (colorTargetMatchesByGroup.ContainsKey(mainModelPartInfo.Group))
            {
                selectedPartsAttrSorted.Add(mainModelPartInfo.Group, colorTargetMatchesByGroup[mainModelPartInfo.Group][mainModelPartInfo.Name]);
            }
                
            if (gluingPartsSortedByGroups != null && gluingPartsSortedByGroups.Count > 0)
            {
                var selectedParts = RandomlySelectParts(gluingPartsSortedByGroups).ToArray();
                foreach (var part in selectedParts)
                {
                    if (colorTargetMatchesByGroup.ContainsKey(part.Info.Group))
                    {
                        if (colorTargetMatchesByGroup[part.Info.Group].ContainsKey(part.Info.Name))
                        {
                            selectedPartsAttrSorted[part.Info.Group] = colorTargetMatchesByGroup[part.Info.Group][part.Info.Name];
                        }
                    }
                }
                GenerateColors(originalSvg, colorTargets, selectedPartsAttrSorted);
                model = new XDocument(originalSvg);
                gluedParts = _gluer.GlueModelParts(model,selectedParts).ToArray();
            }
            else
            {
                GenerateColors(originalSvg, colorTargets, selectedPartsAttrSorted);
                model = new XDocument(originalSvg);
            }

            RestoreAttributes(colorTargets, selectedPartsAttrSorted);

            return model;
        }

        private void LoadPartsAndColorTargets (ModelPartInfo[] modelPartsInfo,
                                              ColorTargetsScanner scanner,
                                              ref Dictionary<string, ModelGluingPart[]> gluingPartsSortedByGroups, 
                                              ref Dictionary<string, Dictionary<string, ColorTargetScannerMatch[]>> colorTargetMatchesByGroup)
        {
            colorTargetMatchesByGroup = new Dictionary<string, Dictionary<string, ColorTargetScannerMatch[]>>();
            XDocument[] modelsSvg = _modelPartSvgLoader
                .LoadMultipleModelsSvg(modelPartsInfo)
                .ToArray();
            var gluingParts = _gluer.PreloadGluingParts(modelsSvg, modelPartsInfo);
            gluingPartsSortedByGroups = gluingParts
                .GroupBy(part => part.Info.Group)
                .Select(group => group.ToArray())
                .ToDictionary(arr => arr.First().Info.Group);

            foreach (var gluingPart in gluingParts)
            {
                var colorTargetMatches = scanner.Scan(gluingPart.Content, gluingPart.Info)
                    .ToArray();
                if (colorTargetMatches.Length > 0)
                {
                    if (!colorTargetMatchesByGroup.ContainsKey(gluingPart.Info.Group))
                    {
                        var group = new Dictionary<string, ColorTargetScannerMatch[]>();
                        group.Add(gluingPart.Info.Name, colorTargetMatches);
                        colorTargetMatchesByGroup.Add(gluingPart.Info.Group, group);
                    }
                    else
                    {
                        colorTargetMatchesByGroup[gluingPart.Info.Group].Add(gluingPart.Info.Name, colorTargetMatches);
                    }
                }
            }
        }

        public ModelGenerator() : base()
        {
        }

        public List<XDocument> GenerateModels(int count,
                                              string mainPartInfoFilename,
                                              string partsInfoFilename,
                                              string colorTargetFilename)
        {
            ModelPartInfo mainModelPartInfo = _partInfoLoader.LoadOne(mainPartInfoFilename);
            ModelPartInfo[] modelPartsInfo = _partInfoLoader
                .LoadMultiple(partsInfoFilename)
                .ToArray();
            ColorTarget[] colorTargets = _colorTargetLoader
                .LoadMultiple(colorTargetFilename)
                .ToArray();

            try
            {
                _validator.ModelPartValidation(mainModelPartInfo, modelPartsInfo);
            }
            catch (ModelPartInfoCollisionExpection e)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
                throw;
            }

            XDocument originalSvg = _modelPartSvgLoader.LoadModelSvg(mainModelPartInfo);

            Dictionary<string, ModelGluingPart[]> gluingPartsSortedByGroups = null;
            Dictionary<string, Dictionary<string, ColorTargetScannerMatch[]>> colorTargetMatchesByGroup = null;
            ColorTargetsScanner scanner = new ColorTargetsScanner(colorTargets);
            if (modelPartsInfo.Length > 0)
            {
                LoadPartsAndColorTargets(modelPartsInfo, 
                                         scanner, 
                                         ref gluingPartsSortedByGroups,
                                         ref colorTargetMatchesByGroup);
            }
            
            var mainAttrs = scanner.Scan(originalSvg, mainModelPartInfo).ToArray();
            if (mainAttrs != null && mainAttrs.Length > 0)
            {
                var innerDictionary = new Dictionary<string, ColorTargetScannerMatch[]>();
                innerDictionary.Add(mainModelPartInfo.Name, mainAttrs);
                colorTargetMatchesByGroup.Add(mainModelPartInfo.Group, innerDictionary);
            }

            List<XDocument> models = new List<XDocument>();
            for (int i = 0; i < count; i++)
            {
                models.Add(GenerateModel(originalSvg, 
                                         colorTargets, 
                                         mainModelPartInfo, 
                                         colorTargetMatchesByGroup, 
                                         gluingPartsSortedByGroups));
            }

            return models;
        }

    }
}