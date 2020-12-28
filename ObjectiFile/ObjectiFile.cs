using ObjectiFile.Data;
using ObjectiFile.Core;
using System.IO;
using System;

namespace ObjectiFile
{
    public static class ObjectiFile
    {
        public static int Write<T>(T obj, FileType? outputType = FileType.CSV)
        {
            var queue = ObjectProcessing.ObjectQueue;
            queue.Enqueue(obj);
            int count = 0;
            while (queue.Count > 0)
            {
                count++;
                var (Name, Body) = ObjectProcessing.GetInformation(queue.Dequeue());
                switch (outputType)
                {
                    case FileType.CSV:
                        Csv.WriteFile(Name, Body);
                        break;
                    case FileType.Excel:
                        Excel.WriteFile(Name, Body);
                        break;
                    case FileType.Json:
                        Json.WriteFile(Name, Body);
                        break;
                    case FileType.XML:
                        Xml.WriteFile(Name, Body);
                        break;
                    default:
                        break;
                };
            }
            return count;
        }

        internal static string GetFilePath()
        {
            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Output\\");
            return $"{AppDomain.CurrentDomain.BaseDirectory}\\Output\\";
        }
    }


    public enum FileType
    {
        CSV,
        Excel,
        Json,
        XML
    }
}