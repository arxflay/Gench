using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Gench.Business
{
    public class ColorTargetsSvgScanner
    {
        private List<ColorTarget> _colorTargets;
        public ColorTargetsSvgScanner(List<ColorTarget> targets)
        {
            _colorTargets = targets;
        }

        //add Tuple with return type of string (name of Attribute in color targets)
        public IEnumerable<XAttribute> Scan(XElement e)
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
                    yield return attr;
                }
            }
        }
    }
}