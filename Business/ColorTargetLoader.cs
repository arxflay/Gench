using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gench.Business
{
    public class ColorTargetLoader : LoaderBase
    {
        public List<ColorTarget> LoadColorTargets(string filename)
        {
            List<ColorTarget> targets = null;
            string content = null;

            content = GetFileContent(filename);
            targets = JsonConvert.DeserializeObject<List<ColorTarget>>(content);
 
            return targets;
        }
    }
}