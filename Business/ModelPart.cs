
namespace Gench.Business
{
    public class ModelPart
    {
        public ModelPart()
        {
            Name = "";
            Path = "";
            RelativePath = false;
        }
        public string Name{ get; set; }
        public string Path{ get; set; }
        public bool RelativePath {get; set;}
        public PartPosition Position{ get; set; }
        public ColorTarget Color{ get; set; }
    }
}