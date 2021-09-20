using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

using Gench.Utils;

namespace Gench.Loaders
{
    public class Loader<T>
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
            catch (FileNotFoundException e)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }
            finally
            {
                if (rd != null) rd.Close();
            }

            return content;
        }

        public IEnumerable<T> LoadMultiple(string filename)
        {
            IEnumerable<T> parts = null;
            string content = null;
            System.Reflection.MethodBase currentMethod = null;
            
            try
            {
                content = GetFileContent(filename);
                parts = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
            }
            catch (System.ArgumentNullException)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogText(this, currentMethod, @"string from file is null");
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }

            return parts;
        }

        public T LoadOne(string filename)
        {
            T modelpart = default(T);
            string content = null;
            System.Reflection.MethodBase currentMethod = null;

            try
            {
                content = GetFileContent(filename);
                modelpart = JsonConvert.DeserializeObject<T>(content);
            }
            catch (System.ArgumentNullException)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogText(this, currentMethod, @"String from file is null");
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                Logger.LogException(this, currentMethod, e);
            }

            return modelpart;
        }
        
    }
}