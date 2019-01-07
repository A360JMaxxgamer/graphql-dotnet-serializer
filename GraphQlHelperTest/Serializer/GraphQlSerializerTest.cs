using GraphQlHelper.Serializer;
using GraphQlHelperTest.MockData.Assertions;
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
                        IntProperty = 3,
                        StringProperty = "Twice"
                    }
                }
            };
            var serializer = new GraphQlSerializer();

            // Act
            var result = serializer.Serialize(testObject);

            // Assert
            Assert.Equal(SerializerTestExpectations.Serialize, result);
        }
    }
}
