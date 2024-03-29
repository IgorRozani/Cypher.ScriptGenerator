﻿using Cypher.ScriptGenerator.Interfaces;
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
            scriptStringBuilder.AppendLine(CREATE);
            foreach (var node in nodes)
            {
                scriptStringBuilder.Append(GenerateNode(node));
                if (node != nodes.LastOrDefault())
                    scriptStringBuilder.AppendLine(", ");
            }
            return scriptStringBuilder.ToString();
        }

        public string Create(Node node) =>
            CREATE + GenerateNode(node);
    }
}
