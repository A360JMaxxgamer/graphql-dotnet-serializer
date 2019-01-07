using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace GraphQlHelper.Serializer
{
    /// <summary>
    /// Serializes c# objects to an string which can be passed to graphql-dotnet as an input type.
    /// </summary>
    public class GraphQlSerializer
    {
        /// <summary>
        /// Serializes the <paramref name="obj"/> to a string representation which can be used as an input for graphql-dotnet.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>A serialized version of the <paramref name="obj"/>.</returns>
        public string Serialize(object obj)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");
            foreach (var property in obj.GetType().GetProperties())
            {
                var name = property.Name;
                var propertyValue = property.GetValue(obj);
                var value = GetPropertyValueAsString(propertyValue);
                stringBuilder.AppendLine(name + ": " +  value);
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Determines the type and returns a serialized value of the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetPropertyValueAsString(object obj)
        {
            if (obj is IEnumerable<object> enumerable)
            {
                return GetEnumerableValueAsString(enumerable);
            }
            if (obj is string stringValue)
            {
                return "\"" + stringValue + "\"";
            }
            if (obj is bool || obj is int || obj is double || obj is float)
            {
                return obj.ToString();
            }
            return Serialize(obj);
        }

        /// <summary>
        /// Returns a serialized string of the <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        private string GetEnumerableValueAsString(IEnumerable<object> enumerable)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[ ");
            foreach (var serializedItem in enumerable.Select(i => GetPropertyValueAsString(i)))
            {
                stringBuilder.AppendLine(serializedItem);
            }
            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}
