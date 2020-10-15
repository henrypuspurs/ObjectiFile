using ObjectiFile.Data;
using ObjectiFile.Core;

namespace ObjectiFile
{
    public class ObjectiFile
    {
        public static void Write<T>(T obj)
        {
            var queue = ObjectProcessing.ObjectQueue;
            queue.Enqueue(obj);
            while (queue.Count > 0)
            {
                var (Name, Body) = ObjectProcessing.GetInformation(queue.Dequeue());
                Csv.WriteFile(Name, Body);
            }
        }
    }
}
