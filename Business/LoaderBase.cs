using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace Gench.Business
{
    public interface ILoader
    {
        string GetFileContent(string path);
    }
    public class LoaderBase : ILoader
    {
        public string GetFileContent(string path) 
        {
            TextReader rd = null;
            string content = null;

            try
            {
                rd = new StreamReader(path);
                content = rd.ReadToEnd();

            }
            catch 
            {
                Debug.WriteLine("Failed to open file");
            }
            finally
            {
                if (rd != null) rd.Close();
            }

            return content;
        }

    }
}