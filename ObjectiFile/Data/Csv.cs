using System;
using System.IO;

namespace ObjectiFile.Data
{
    public class Csv
    {
        internal static void WriteFile(string objName, string[][] body)
        {
            int count = 0;
            string dir = ObjectiFile.GetFilePath();
            string fileName = $"{ objName }_{ DateTime.Now:dd-MMM-yyyy_HH-mm-ss}.csv";
            string fullPath = $"{dir}{fileName}";
            while (File.Exists(fullPath))
            {
                count++;
                fileName = $"{ objName }_{ DateTime.Now:dd-MMM-yyyy_HH-mm-ss}_{count}.csv";
                fullPath = $"{dir}{fileName}";
            }
            File.WriteAllText(fullPath, ConvertToTextBody(body));
        }

        private static string ConvertToTextBody(string[][] body)
        {
            string textBody = "";
            foreach (string[] array in body)
            {
                var row = string.Join(", ", array);
                if (array != body[0])
                {
                    textBody = string.Join(Environment.NewLine, textBody, row);
                }
                else
                {
                    textBody = string.Join("", textBody, row);
                }
            }
            return textBody;
        }

        internal static string CheckForComma(string stringToAdd)
        {
            string checkedString = "";
            if (stringToAdd is not null)
            {
                if (stringToAdd.Contains(","))
                {
                    checkedString = $"\"{stringToAdd}\"";
                }
                else
                {
                    checkedString = stringToAdd;
                }
            }
            return checkedString;
        }
    }
}
