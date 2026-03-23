using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{

    public class NodeGenerator : BaseGenerator, INodeGenerator
    {
        public string Create(IList<Node> nodes)
        {
            var scriptStringBuilder = new StringBuilder();
            scriptStringBuilder.Append(CREATE + "\n");
            foreach (var node in nodes)
            {
                scriptStringBuilder.Append(GenerateNode(node));
                if (node != nodes.LastOrDefault())
                    scriptStringBuilder.Append(", \n");
            }
            return scriptStringBuilder.ToString();
        }

        public string Create(Node node) =>
            CREATE + GenerateNode(node);

        public string Merge(IList<Node> nodes)
        {
            var scriptStringBuilder = new StringBuilder();
            scriptStringBuilder.Append(MERGE + "\n");
            foreach (var node in nodes)
            {
                scriptStringBuilder.Append(GenerateNode(node));
                if (node != nodes.LastOrDefault())
                    scriptStringBuilder.Append(", \n");
            }
            return scriptStringBuilder.ToString();
        }

        public string Merge(Node node)
        {
            var sb = new StringBuilder(MERGE + GenerateNode(node));
            AppendOnCreateAndMatch(sb, node.Id, node.OnCreateProperties, node.OnMatchProperties);
            return sb.ToString();
        }

        public string Delete(IList<Node> nodes, bool detach = true)
        {
            var scriptStringBuilder = new StringBuilder();
            foreach (var node in nodes)
                scriptStringBuilder.Append(Delete(node, detach) + "\n");
            return scriptStringBuilder.ToString();
        }

        public string Delete(Node node, bool detach = true)
        {
            var deleteCommand = detach ? DETACH_DELETE : DELETE;
            return MATCH + GenerateNode(node) + " " + deleteCommand + node.Id;
        }
    }
}
