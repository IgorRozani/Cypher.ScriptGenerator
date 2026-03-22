using Cypher.ScriptGenerator.Models;
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
        protected const string MERGE = "MERGE ";
        protected const string MATCH = "MATCH ";
        protected const string DELETE = "DELETE ";
        protected const string DETACH_DELETE = "DETACH DELETE ";

        protected string GetLabels(List<string> labels)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var label in labels)
                scriptBuilder.Append(':').Append(label);

            return scriptBuilder.ToString();
        }

        protected string FormatValue(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.String:
                    return "\"" + value + "\"";
                case TypeCode.DateTime:
                    var dt = (DateTime)value;
                    return $"datetime({{year:{dt.Year}, month:{dt.Month}, day:{dt.Day}, hour:{dt.Hour}, minute:{dt.Minute}, second:{dt.Second}}})";
                default:
                    return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        protected string GetProperties(Dictionary<string, object> properties)
        {
            var scriptBuilder = new StringBuilder(" {");

            foreach (var property in properties)
            {
                if (property.Value == null)
                    continue;

                scriptBuilder.Append(property.Key).Append(":").Append(FormatValue(property.Value));

                if (property.Key != properties.Keys.LastOrDefault())
                    scriptBuilder.Append(", ");
            }

            return scriptBuilder.Append("}").ToString();
        }

        protected void AppendOnCreateAndMatch(StringBuilder sb, string alias, Dictionary<string, object> onCreateProperties, Dictionary<string, object> onMatchProperties)
        {
            if (onCreateProperties?.Any() == true)
            {
                sb.Append(" ON CREATE SET ");
                sb.Append(string.Join(", ", onCreateProperties
                    .Where(p => p.Value != null)
                    .Select(p => $"{alias}.{p.Key} = {FormatValue(p.Value)}")));
            }

            if (onMatchProperties?.Any() == true)
            {
                sb.Append(" ON MATCH SET ");
                sb.Append(string.Join(", ", onMatchProperties
                    .Where(p => p.Value != null)
                    .Select(p => $"{alias}.{p.Key} = {FormatValue(p.Value)}")));
            }
        }

        protected string GetPropertyReferences(IList<string> properties, string alias)
        {
            return " {" + string.Join(", ", properties.Select(p => $"{p}: {alias}.{p}")) + "}";
        }

        protected string GenerateNode(Node node)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(node.Id);

            scriptBuilder.Append(GetLabels(node.Labels));

            if (node.Properties.Any())
                scriptBuilder.Append(GetProperties(node.Properties));

            scriptBuilder.Append(")");

            return scriptBuilder.ToString();
        }
    }
}
