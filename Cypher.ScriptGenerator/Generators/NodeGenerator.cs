using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
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

            scriptBuilder.Append(GetLabels(node.Labels));

            if (node.Properties.Any())
                scriptBuilder.Append(GetProperties(node.Properties));

            scriptBuilder.Append(")");

            return scriptBuilder.ToString();
        }

    }
}
