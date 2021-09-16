using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Gench.Business
{
    public class ModelPartLoader : LoaderBase
    {
        public List<ModelPart> LoadModelParts(string filename)
        {
            List<ModelPart> parts = null;
            string content = null;
            
            try
            {
                content = GetFileContent(filename);
                parts = JsonConvert.DeserializeObject<List<ModelPart>>(content);
            }
            catch
            {

            }

            return parts;
        }

        public ModelPart LoadPart(string filename)
        {
            ModelPart modelpart = null;
            string content = null;

            try
            {
                content = GetFileContent(filename);
                modelpart = JsonConvert.DeserializeObject<ModelPart>(content);
            }
            catch
            {
               
            }

            return modelpart;
        }
    }
}