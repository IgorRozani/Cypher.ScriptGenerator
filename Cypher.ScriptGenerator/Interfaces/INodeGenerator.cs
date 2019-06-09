using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface INodeGenerator
    {
        string Create(IList<Node> nodes);

        string Create(Node node);
    }
}
