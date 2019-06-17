using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{
    public class RelationshipGenerator : BaseGenerator, IRelationshipGenerator
    {
        public string Create(IList<CreateRelationship> relationships)
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

        public string Create(CreateRelationship relationship) =>
            CREATE + GenerateRelationship(relationship);

        private string GenerateRelationship(CreateRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(relationship.NodeId1).Append(")-[");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.NodeId2).Append(")");

            return scriptBuilder.ToString();
        }

        public string CreateAndSearch(CreateAndSearchRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(relationship.Node1.Id))
                relationship.Node1.Id = "n1";
            if (string.IsNullOrEmpty(relationship.Node2.Id))
                relationship.Node2.Id = "n2";

            scriptBuilder.Append("MATCH ").Append(GenerateNode(relationship.Node1)).Append(", ").AppendLine(GenerateNode(relationship.Node2)).Append(CREATE).Append(GenerateRelationship(relationship));

            return scriptBuilder.ToString();
        }

        private string GenerateRelationship(CreateAndSearchRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(relationship.Node1.Id).Append(")-[");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.Node2.Id).Append(")");

            return scriptBuilder.ToString();
        }

        public string CreateAndSearch(IList<CreateAndSearchRelationship> relationships)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var relationship in relationships)
                scriptBuilder.AppendLine(CreateAndSearch(relationship));

            return scriptBuilder.ToString();
        }
    }
}
