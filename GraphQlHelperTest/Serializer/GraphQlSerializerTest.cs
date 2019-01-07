using GraphQlHelper.Serializer;
using GraphQlHelperTest.MockData.Models;
using System.Collections.Generic;
using Xunit;

namespace GraphQlHelperTest.Serializer
{
    public class GraphQlSerializerTest
    {
        [Fact]
        public void SerializeTest()
        {
            // Arrange
            var expectation = @"{
IntProperty: 1
StringProperty: ""GraphQl""
BoolProperty: True
ComplexEnumerableProperty: [ {
IntProperty: 2
StringProperty: ""Nested""
}

{
IntProperty: 3
}

]
}
";
            var testObject = new SerializerTestModelOne
            {
                BoolProperty = true,
                IntProperty = 1,
                StringProperty = "GraphQl",
                ComplexEnumerableProperty = new List<SerializerTestModelTwo>
                {
                    new SerializerTestModelTwo
                    {
                        IntProperty = 2,
                        StringProperty = "Nested"
                    },
                    new SerializerTestModelTwo
                    {
                        IntProperty = 3
                    }
                }
            };
            var serializer = new GraphQlSerializer();

            // Act
            var result = serializer.Serialize(testObject);

            // Assert
            Assert.Equal(expectation, result);
        }
    }
}
