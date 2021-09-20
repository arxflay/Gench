using System;
using System.Diagnostics;
using System.Reflection;

namespace Gench.Utils
{
    public static class Logger
    {
        public static void LogException(object caller, MethodBase method, Exception e)
        {
            Debug.WriteLine($"{DateTime.Now} from {caller.ToString()} in {method.Name}: {e.Message}");
        }

        public static void LogException(object caller, Exception e)
        {
            Debug.WriteLine($"{DateTime.Now} from {caller.ToString()}: {e.Message}");
        }

        public static void LogText(object caller, MethodBase method, string text)
        {
            Debug.WriteLine($"{DateTime.Now} from {caller.ToString()} in {method.Name}: {text}");
        }
        
        public static void LogText(object caller, string text)
        {
            Debug.WriteLine($"{DateTime.Now} from {caller.ToString()}: {text}");
        }

    }
}