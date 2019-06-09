using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{

    public class NodeGenerator : BaseGenerator, INodeGenerator
    {
        public string CreateNodes(IList<Node> nodes)
        {
            var scriptStringBuilder = new StringBuilder();
            scriptStringBuilder.AppendLine(CREATE);
            foreach (var node in nodes)
            {
                scriptStringBuilder.Append(GenerateNode(node));
                if (node != nodes.LastOrDefault())
                    scriptStringBuilder.AppendLine(", ");
            }
            return scriptStringBuilder.ToString();
        }

        public string CreateNode(Node node) =>
            CREATE + GenerateNode(node);

        private string GenerateNode(Node node)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(node.Id);

            foreach (var label in node.Labels)
                scriptBuilder.Append(':').Append(label);

            if (node.Properties.Any())
            {
                scriptBuilder.Append(" {");

                foreach (var property in node.Properties)
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

                    if (property.Key != node.Properties.Keys.LastOrDefault())
                        scriptBuilder.Append(", ");
                }

                scriptBuilder.Append("}");
            }

            scriptBuilder.Append(")");

            return scriptBuilder.ToString();
        }

    }
}
