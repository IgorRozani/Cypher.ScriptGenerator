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

        string Merge(IList<CreateRelationship> relationships);
        string Merge(CreateRelationship relationship);
        string MergeAndSearch(CreateAndSearchRelationship relationship);
        string MergeAndSearch(IList<CreateAndSearchRelationship> relationships);

        string Delete(IList<CreateRelationship> relationships);
        string Delete(CreateRelationship relationship);
        string DeleteAndSearch(CreateAndSearchRelationship relationship);
        string DeleteAndSearch(IList<CreateAndSearchRelationship> relationships);
    }
}
