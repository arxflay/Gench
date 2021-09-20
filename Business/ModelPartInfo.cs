namespace Gench.Business
{
    public class ModelPartInfo
    {
        public ModelPartInfo(string name = null
                        , string path = null
                        , string group = null
                        , bool relativePath = false
                        , PartPosition position = default(PartPosition)
                        , ColorTarget color = null)
        {
            Name = name;
            Path = path;
            Group = group;
            RelativePath = relativePath;
            Position = position;
            Color = color;
        }
        
        public string Name { get; }
        public string Path { get; }
        public string Group { get; }
        public bool RelativePath { get; }
        public PartPosition Position { get; }
        public ColorTarget Color { get; }
    }
}