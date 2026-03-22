using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface INodeGenerator
    {
        string Create(IList<Node> nodes);
        string Create(Node node);

        string Merge(IList<Node> nodes);
        string Merge(Node node);

        string Delete(IList<Node> nodes, bool detach = true);
        string Delete(Node node, bool detach = true);
    }
}
