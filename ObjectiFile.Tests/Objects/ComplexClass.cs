using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiFile.Tests.Objects
{
    class ComplexClass : BasicClass
    {
        public List<string> StringList { get; set; }
        public int[] IntArray { get; set; }
        public List<List<int>> NestedLists { get; set; }
        public int[,,] MultiArray { get; set; }
    }
}
