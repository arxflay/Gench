using System.Linq;
namespace Gench.Business
{
    public class ModelGeneratorValidator
    {
        public void ModelPartValidation(ModelPartInfo mainModelPartInfo, ModelPartInfo[] modelPartsInfo)
        {
            if (modelPartsInfo.Any(el => mainModelPartInfo.Name == el.Name))
            {
                throw new ModelPartInfoCollisionExpection("ModelParts contains element with same Name as main part");
            }
            else if (modelPartsInfo.Any(el => mainModelPartInfo.Group == el.Group))
            {
                throw new ModelPartInfoCollisionExpection("ModelParts contains element with same Group as main part");
            }
            else if (modelPartsInfo.GroupBy(el => el.Name).Any(group => group.Count() > 1))
            {
                throw new ModelPartInfoCollisionExpection("Multiple elements with same Name");
            }
            else if (modelPartsInfo.GroupBy(el => el.Path).Any(group => group.Count() > 1))
            {
                throw new ModelPartInfoCollisionExpection("Multiple elements with same Path");
            }
        }
    }
}