using System;
using System.IO;
using System.Threading.Tasks;

using Gench.Business;
using Gench.Utils;

namespace Gench
{
    class Program
    {
        private static string baseDirectory = AppContext.BaseDirectory.ToString();
        private static string dataPath = baseDirectory + "Data/";
        private static string targetsPath = baseDirectory + "targets/";
        private static string modelsPath = dataPath + "models/";
        private static string outputPath = baseDirectory + "output/";

        static async Task Main(string[] args)
        {
            int count = 0;
            Console.WriteLine("Character Generator");

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            if (!Directory.Exists(targetsPath))
            {
                Directory.CreateDirectory(targetsPath);
            }

            try
            {
                Console.Write("Enter N: ");
                count = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid count");
                return;
            }
            ModelGeneratorFullModel gen = new ModelGeneratorFullModel();
            SvgWriter writer = new SvgWriter();
            var models = gen.GenerateModels(count, targetsPath + "parts.json", targetsPath + "color_targets.json");
            await writer.AsyncWriteMultiple(outputPath, models);
        }
    }
}
