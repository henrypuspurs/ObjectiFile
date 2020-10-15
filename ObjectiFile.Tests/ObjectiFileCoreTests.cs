using ObjectiFile.Tests.Objects;
using System;
using System.Collections.Generic;
using Xunit;

namespace ObjectiFile.Tests
{
    public class ObjectiFileCoreTests
    {
        [Fact]
        public void CsvWritesSimpleFiles()
        {
            var obj = new BasicClass { Id = 5, Name = "John Smith", SecondString = "This is a string", State = true };

            ObjectiFile.Write("Hello");
            ObjectiFile.Write(1);
            ObjectiFile.Write(obj);

            Assert.True(obj.State);
        }

        [Fact]
        public void CsvWritesFilesFromIEnumerableOfClass()
        {
            var obj = new BasicClass { Id = 5, Name = "John Smith", SecondString = "This is a string", State = true };
            var objone = new BasicClass { Id = 26, Name = "Bilbo Baggins", SecondString = "This is also a string", State = false };
            var objtwo = new BasicClass { Id = 7, Name = "Luke Skywalker", SecondString = "This is still a string", State = false };
            var objthree = new BasicClass { Id = 14, Name = "Gandalf Greymane", SecondString = "Yup, This is a string", State = true };

            var objectList = new List<BasicClass> { obj, objone, objtwo, objthree };

            ObjectiFile.Write(objectList);

            Assert.True(obj.State);
        }

        [Fact]
        public void CsvWritesIEnumerablesAtDifferentLevels()
        {
            var obj = new ComplexClass { Id = 5, Name = "John Smith", SecondString = "This is a string", State = true };
            obj.IntArray = new int[] { 1, 2, 3, 4, 5 };
            obj.StringList = new List<string> { "this", "should", "be", "in", "order" };
            obj.NestedLists = new List<List<int>>
            {
                new List<int> { 1, 2, 3, 4, 5 },
                new List<int> { 0, 9, 8, 7, 6 }
            };
            obj.MultiArray = new int[,,] { { { 1, 2, 3 }, { 4, 5, 6 } }, { { 7, 8, 9 }, { 10, 11, 12 } } };
            ObjectiFile.Write(obj);

            Assert.True(obj.State);
        }
    }
}
