using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface IRelationshipGenerator
    {
        string Create(IList<CreateRelationship> relationships);

        string Create(CreateRelationship relationship);

        string CreateAndSearch(CreateAndSearchRelationship relationship);

        string CreateAndSearch(IList<CreateAndSearchRelationship> relationships);
    }
}
