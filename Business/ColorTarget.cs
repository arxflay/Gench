

namespace Gench.Business
{
    public class ColorTarget
    {

        public ColorTarget(string name, 
                           string targetColor, 
                           string ownerGroup, 
                           bool genAffectedByPrevTarget,
                           PartColor minGenColorRange, 
                           PartColor maxGenColorRange)
        {
            Name = name;
            TargetColor = targetColor;
            OwnerGroup = ownerGroup;
            GenAffectedByPrevTarget = genAffectedByPrevTarget;
            MinGenColorRange = minGenColorRange;
            MaxGenColorRange = maxGenColorRange;
        }

        public string TargetColor { get; }
        public string Name { get; }
        public string OwnerGroup { get; }
        public bool GenAffectedByPrevTarget { get; }
        public PartColor MinGenColorRange { get; set; }
        public PartColor MaxGenColorRange { get; set; }
    }
}