using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface INodeGenerator
    {
        string CreateNodes(IList<Node> nodes);

        string CreateNode(Node node);
    }
}
