using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectiFile.Core
{
    class TableCreation
    {
        internal static string[][] GetCollectionBody<T>(T obj)
        {
            if (obj is not IEnumerable<object>)
            {
                return new string[][] { new string[] { obj as string } };
            }
            else if (obj is IEnumerable<string> stringEnum)
            {
                return GetValueCollectionBody(stringEnum);
            }
            else if (obj is IEnumerable<ValueType> valueEnum)
            {
                return GetValueCollectionBody(valueEnum);
            }
            else
            {
                return GetObjectCollectionBody(obj);
            }
        }

        private static string[][] GetObjectCollectionBody<T>(T obj)
        {
            var objCol = obj as IEnumerable<object>;
            var arrayDimensions = new int[] { objCol.Count() + 1, objCol.FirstOrDefault().GetType().GetProperties().Length };
            var rowList = new string[arrayDimensions[0]][];
            var currentArray = new string[arrayDimensions[1]];

            for (int y = 0; y < arrayDimensions[0] - 1; y++)
            {
                var currentObject = objCol.ElementAt(y);
                
                if (!ObjectProcessing.ImplementsIEnumerable(currentObject))
                {
                    for (int x = 0; x < arrayDimensions[1]; x++)
                    {
                        var currentProperty = currentObject.GetType().GetProperties().ElementAt(x);
                        if (y != 0)
                        {
                            currentArray[x] = currentProperty.GetValue(currentObject).ToString();
                        }
                        else
                        {
                            currentArray[x] = currentProperty.Name;
                        }
                    }
                }
                else
                {
                    ObjectProcessing.ObjectQueue.Enqueue(currentObject);
                }
                rowList[y] = currentArray;
            }
            return rowList;
        }

        private static string[][] GetValueCollectionBody<T>(IEnumerable<T> obj)
        {
            var result = new string[obj.GetType().GetProperties().Length];
            int count = 0;
            foreach (var inner in obj)
            {
                result[count] = inner as string;
                count++;
            }
            return new string[][] { result };
        }

        internal static string[][] GetObjectBody<T>(T obj)
        {
            var properties = obj.GetType().GetProperties();
            var results = new string[properties.Length][];
            var row = new string[2];
            for (int i = 0; i < properties.Length - 1; i++)
            {
                var currentObj = properties.ElementAt(i);
                row[0] = currentObj.Name;
                row[1] = currentObj.GetValue(currentObj).ToString();
                if (ObjectProcessing.ImplementsIEnumerable(currentObj))
                {
                    ObjectProcessing.ObjectQueue.Enqueue(currentObj);
                }
            }
            return results;
        }
    }
}
