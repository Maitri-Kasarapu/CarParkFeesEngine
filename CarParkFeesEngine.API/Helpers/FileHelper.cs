using System;
using System.IO;

namespace CarParkFeesEngine.API.Helpers
{
    public class FileHelper
    {
        public static string GetContentFromFile(string filePath)
        {
            try
            {
                StreamReader stream = new StreamReader(filePath);
                return stream.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
