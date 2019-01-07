using System.Collections.Generic;

namespace GraphQlHelperTest.MockData.Models
{
    public class SerializerTestModelOne
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public IEnumerable<SerializerTestModelTwo> ComplexEnumerableProperty { get; set; }
    }

    public class SerializerTestModelTwo
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
    }
}
