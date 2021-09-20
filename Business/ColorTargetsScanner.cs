using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

using Gench.Utils;

namespace Gench.Business
{
    public class ColorTargetsScanner
    {
        private IEnumerable<ColorTarget> _colorTargets;

        private IEnumerable<ColorTargetScannerMatch> FindMatches(IEnumerable<XAttribute> attributes, ModelPartInfo partInfo)
        {
            foreach(var attr in attributes)
            {
                foreach(var target in _colorTargets)
                {
                    if (partInfo.Group == target.OwnerGroup)
                    {
                        if (target.TargetColor == attr.Value)
                        {
                            yield return new ColorTargetScannerMatch(attr, partInfo.Name, partInfo.Group);
                        }
                    }
                }
            }

        }

        public ColorTargetsScanner(IEnumerable<ColorTarget> targets)
        {
            _colorTargets = targets;
        }

        public IEnumerable<ColorTargetScannerMatch> Scan(XDocument doc, ModelPartInfo partInfo)
        {
            IEnumerable<ColorTargetScannerMatch> output = null;
            try
            {
                output = Scan(doc.Root, partInfo);
            }
            catch (System.NullReferenceException)
            {
                Logger.LogText(this, System.Reflection.MethodBase.GetCurrentMethod(), "XDocument is null");
            }
            return output;
        }

        public IEnumerable<ColorTargetScannerMatch> Scan(XElement e, ModelPartInfo info)
        {
            var innerElements = e.Descendants();
            
            var attributes = e.Attributes();

            foreach(var attrAndName in FindMatches(attributes, info))
            {
                yield return attrAndName;
            }

            if (innerElements.Count() != 0)
            {
                var innerElementsAttributes = innerElements.Attributes();
                foreach(var attrAndName in FindMatches(innerElementsAttributes,info))
                {
                    yield return attrAndName;
                }
            }
        }
    }
}