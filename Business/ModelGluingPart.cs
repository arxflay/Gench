using System.Linq;
using System.Xml.Linq;
namespace Gench.Business
{
    public class ModelGluingPart
    {
        public ModelGluingPart(XElement useTag, XElement content, ModelPartInfo info)
        {
            UseTag = useTag;
            Content = content;
            Info = info;
        }

        public ModelGluingPart(ModelGluingPart part)
        {
            UseTag = new XElement(part.UseTag);
            Content = new XElement(part.Content);
            Info = part.Info;
        }
        
        //removes elements from parent
        public void Remove()
        {
            UseTag.Remove();
            Content.Remove();
        }

        public XElement UseTag { get; }
        public XElement Content { get; }
        public ModelPartInfo Info { get; }
    }
}