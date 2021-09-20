using System.Xml.Linq;

namespace Gench.Business
{
    public class ColorTargetScannerMatch
    {
        public ColorTargetScannerMatch(XAttribute attribute, string partName, string ownerGroup )
        {
            Attribute = attribute;
            PartName = partName;
            OwnerGroup = ownerGroup;
        }

        public XAttribute Attribute { get; set; }
        public string PartName { get; }
        public string OwnerGroup { get; }
    }
}