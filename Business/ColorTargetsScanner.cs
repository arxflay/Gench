using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

using Gench.Utils;

namespace Gench.Business
{
    public class ColorTargetsSvgScanner
    {
        private List<ColorTarget> _colorTargets;

        public ColorTargetsSvgScanner(List<ColorTarget> targets)
        {
            _colorTargets = targets;
        }

        public IEnumerable<(XAttribute Attribute,string Name)> Scan(XDocument doc)
        {
            IEnumerable<(XAttribute Attribute,string Name)> output = null;
            try
            {
                output =  Scan(doc.Root);
            }
            catch (System.NullReferenceException e)
            {
                e.ToString(); //disables warning
                Logger.LogText(this, System.Reflection.MethodBase.GetCurrentMethod(), "XDocument is null");
            }
            return output;
        }

        private IEnumerable<(XAttribute Attribute,string Name)> Scan(XElement e)
        {
            var innerElements = e.Elements().ToList();
            if (innerElements.Count != 0)
            {
                foreach (var el in innerElements)
                {
                    var innerElementsInEl = Scan(el);
                    if (innerElementsInEl.Count() > 0)
                    {
                        foreach (var innerAttr in innerElementsInEl)
                        {
                            yield return innerAttr;
                        }
                    }
                }
            }
            
            var attributes = e.Attributes().ToList();

            foreach(var attr in attributes)
            {
                if (_colorTargets.Any(el => el.Color.ToString() == attr.Value))
                {
                    yield return (attr,_colorTargets.First(el => el.Color.ToString() == attr.Value).Name);
                }
            }
        }
    }
}