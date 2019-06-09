using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{

    public class RelationshipGenerator : BaseGenerator, IRelationshipGenerator
    {
        public string CreateRelationships(IList<Relationship> relationships)
        {
            var scriptStringBuilder = new StringBuilder();
            scriptStringBuilder.AppendLine(CREATE);
            foreach (var relationship in relationships)
            {
                scriptStringBuilder.Append(GenerateRelationship(relationship));
                if (relationship != relationships.LastOrDefault())
                    scriptStringBuilder.AppendLine(", ");
            }
            return scriptStringBuilder.ToString();
        }

        public string CreateRelationship(Relationship relationship) =>
            CREATE + GenerateRelationship(relationship);

        private string GenerateRelationship(Relationship relationship)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(relationship.NodeId1).Append(")-[");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.NodeId2).Append(")");

            return scriptBuilder.ToString();
        }
    }
}
