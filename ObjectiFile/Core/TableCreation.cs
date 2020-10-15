using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectiFile.Core
{
    class TableCreation
    {
        internal static string GetCollectionBody<T>(T obj)
        {
            var enumObj = obj as IEnumerable<object>;
            if (enumObj == null)
            {
                return GetValueCollectionBody(obj);
            }
            else
            {
                var arrayDimensions = new int[] { enumObj.Count() + 1, enumObj.FirstOrDefault().GetType().GetProperties().Count() };
                var rowList = new string[arrayDimensions[0], arrayDimensions[1]];

                for (int y = 0; y < arrayDimensions[0] - 1; y++)
                {
                    var currentObject = enumObj.ElementAt(y);
                    if (!ObjectProcessing.ImplementsIEnumerable(currentObject))
                    {
                        for (int x = 0; x < arrayDimensions[1]; x++)
                        {
                            var currentProperty = currentObject.GetType().GetProperties().ElementAt(x);
                            rowList[y + 1, x] = currentProperty.GetValue(currentObject).ToString();
                            if (y == 0)
                            {
                                rowList[y, x] = currentProperty.Name;
                            }
                        }
                    }
                    else
                    {
                        ObjectProcessing.ObjectQueue.Enqueue(currentObject);
                    }
                }
                return ArrayToString(rowList);
            }
        }

        private static string GetValueCollectionBody<T>(T obj)
        {
            throw new NotImplementedException();
        }

        internal static string GetObjectBody<T>(T obj)
        {
            var rowValues = "";
            var members = new Dictionary<string, string>();
            foreach (var propInfo in obj.GetType().GetProperties())
            {
                var newObject = propInfo.GetValue(obj);
                if (ObjectProcessing.ImplementsIEnumerable(newObject) && !(newObject is string))
                {
                    ObjectProcessing.ObjectQueue.Enqueue(newObject);
                }
                else
                {
                    members.Add(propInfo.Name, propInfo.GetValue(obj).ToString());
                }
            }

            var columnNames = GetColumnNames(members);
            for (int i = 0; i < members.Count; i++)
            {
                if (string.IsNullOrEmpty(rowValues))
                {
                    rowValues = string.Join("", rowValues, ObjectProcessing.CheckForComma(members.ElementAt(i).Value));
                }
                else
                {
                    rowValues = string.Join(",", rowValues, ObjectProcessing.CheckForComma(members.ElementAt(i).Value));
                }
            }

            return string.Join(Environment.NewLine, columnNames, rowValues);
        }

        private static string GetColumnNames(Dictionary<string, string> members)
        {
            string columnNames = "";
            for (int i = 0; i < members.Count; i++)
            {
                if (string.IsNullOrEmpty(columnNames))
                {
                    columnNames = string.Join("", columnNames, members.ElementAt(i).Key);
                }
                else
                {
                    columnNames = string.Join(",", columnNames, members.ElementAt(i).Key);
                }
            }
            return columnNames;
        }

        private static string ArrayToString(string[,] rowList)
        {
            var textBody = "";
            for (int i = 0; i < rowList.GetLength(0); i++)
            {
                for (int z = 0; z < rowList.GetLength(1); z++)
                {
                    var stringToAdd = ObjectProcessing.CheckForComma(rowList[i, z]);

                    if (z == 0)
                    {
                        textBody = string.Join("", textBody, stringToAdd);
                    }
                    else
                    {
                        textBody = string.Join(",", textBody, stringToAdd);
                    }

                    if (z == rowList.GetLength(1) - 1)
                    {
                        textBody = string.Join("", textBody, Environment.NewLine);
                    }
                }
            }
            return textBody;
        }
    }
}
