﻿using System;
using System.Collections.Generic;

namespace ObjectiFile.Core
{
    class ObjectProcessing
    {
        internal static Queue<object> ObjectQueue { get; set; } = new Queue<object>();

        internal static (string Name, string[][] Body) GetInformation<T>(T obj)
        {
            switch (obj)
            {
                case string str:
                    return ("Single String", new string[][] { new string[] { str } });
                case ValueType _:
                    return (obj.GetType().Name, new string[][] { new string[] { obj.ToString() } });
                case null:
                    return (null, null);
                default:
                    if (ImplementsIEnumerable(obj))
                    {
                        return (obj.GetType().Name, TableCreation.GetCollectionBody(obj));
                    }
                    else
                    {
                        return (obj.GetType().Name, TableCreation.GetObjectBody(obj));
                    }
            }
        }

        internal static bool ImplementsIEnumerable<T>(T obj)
        {
            var type = obj.GetType();
            if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return true;
            }
            foreach (Type interfaceUsed in type.GetInterfaces())
            {
                if (interfaceUsed.IsGenericType
                    && interfaceUsed.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
