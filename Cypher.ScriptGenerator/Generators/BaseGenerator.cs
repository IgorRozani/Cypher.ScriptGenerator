using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{
    public class BaseGenerator
    {
        protected const string CREATE = "CREATE ";

        protected string GetLabels(List<string> labels)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var label in labels)
                scriptBuilder.Append(':').Append(label);

            return scriptBuilder.ToString();
        }

        protected string GetProperties(Dictionary<string, object> properties)
        {
            var scriptBuilder = new StringBuilder(" {");

            foreach (var property in properties)
            {
                if (property.Value == null)
                    continue;

                scriptBuilder.Append(property.Key).Append(":");

                switch (Type.GetTypeCode(property.Value.GetType()))
                {
                    case TypeCode.String:
                        scriptBuilder.Append("\"").Append(property.Value).Append("\"");
                        break;
                    case TypeCode.DateTime:
                        var datetime = (DateTime)property.Value;
                        scriptBuilder.Append("datetime({year:").Append(datetime.Year).Append(", month:").Append(datetime.Month).Append(", day:").Append(datetime.Day).Append(", hour:").Append(datetime.Hour).Append(", minute:").Append(datetime.Minute).Append(", second:").Append(datetime.Second).Append("})");
                        break;
                    default:
                        scriptBuilder.Append(Convert.ToString(property.Value, CultureInfo.InvariantCulture));
                        break;
                }

                if (property.Key != properties.Keys.LastOrDefault())
                    scriptBuilder.Append(", ");
            }

            return scriptBuilder.Append("}").ToString();
        }
    }
}
