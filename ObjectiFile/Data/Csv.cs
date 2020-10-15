using System;
using System.IO;

namespace ObjectiFile.Data
{
    public class Csv
    {
        internal static void WriteFile(string objName, string textBody)
        {
            int count = 0;
            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Output\\");
            string dir = $"{AppDomain.CurrentDomain.BaseDirectory}\\Output\\";
            string fileName = $"{ objName }_{ DateTime.Now:dd-MMM-yyyy_HH-mm-ss}.csv";
            string fullPath = $"{dir}{fileName}";
            while (File.Exists(fullPath))
            {
                count++;
                fileName = $"{ objName }_{ DateTime.Now:dd-MMM-yyyy_HH-mm-ss}_{count}.csv";
                fullPath = $"{dir}{fileName}";
            }
            File.WriteAllText(fullPath, textBody);
        }
    }
}
