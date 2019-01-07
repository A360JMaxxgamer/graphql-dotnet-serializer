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
        /// <param name="propertyNameToLowerCase">An optional parameter to create the property names in lower case</param>
        /// <returns>A serialized version of the <paramref name="obj"/>.</returns>
        public string Serialize(object obj, bool propertyNameToLowerCase = false)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");
            foreach (var property in obj.GetType().GetProperties())
            {
                var name = propertyNameToLowerCase 
                    ? property.Name.ToLower() 
                    : property.Name;
                var propertyValue = property.GetValue(obj);
                if (propertyValue == null) continue;
                var value = GetPropertyValueAsString(propertyValue, propertyNameToLowerCase);
                stringBuilder.AppendLine(name + ": " +  value);
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Determines the type and returns a serialized value of the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyNameToLowerCase">A parameter to create the property names in lower case</param>
        /// <returns></returns>
        private string GetPropertyValueAsString(object obj, bool propertyNameToLowerCase)
        {
            if (obj is IEnumerable<object> enumerable)
            {
                return GetEnumerableValueAsString(enumerable, propertyNameToLowerCase);
            }
            if (obj is string stringValue)
            {
                return "\"" + stringValue + "\"";
            }
            if (obj is bool || obj is int || obj is double || obj is float)
            {
                return obj.ToString();
            }
            return Serialize(obj, propertyNameToLowerCase);
        }

        /// <summary>
        /// Returns a serialized string of the <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="propertyNameToLowerCase">A parameter to create the property names in lower case</param>
        /// <returns></returns>
        private string GetEnumerableValueAsString(IEnumerable<object> enumerable, bool propertyNameToLowerCase)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[ ");
            foreach (var serializedItem in enumerable.Select(i => GetPropertyValueAsString(i, propertyNameToLowerCase)))
            {
                stringBuilder.AppendLine(serializedItem);
            }
            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}
