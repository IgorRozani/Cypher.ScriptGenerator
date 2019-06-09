using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cypher.ScriptGenerator.Interfaces
{

    public interface IRelationshipGenerator
    {
        string CreateRelationships(IList<Relationship> relationships);

        string CreateRelationship(Relationship relationship);
    }
}
