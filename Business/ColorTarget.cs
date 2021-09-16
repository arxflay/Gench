

namespace Gench.Business
{
    public class ColorTarget
    {

        public ColorTarget(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public string Color { get; }
        public string Name { get; }
    }
}