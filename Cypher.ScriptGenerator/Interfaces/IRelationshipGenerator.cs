using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface IRelationshipGenerator
    {
        string Create(IList<Relationship> relationships);

        string Create(Relationship relationship);
    }
}
