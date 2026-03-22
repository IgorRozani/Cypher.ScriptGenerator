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
            scriptBuilder.Append('(').Append(relationship.NodeIdLeft).Append(")-[");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.NodeIdRight).Append(")");

            return scriptBuilder.ToString();
        }

        public string CreateAndSearch(CreateAndSearchRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(relationship.NodeLeft.Id))
                relationship.NodeLeft.Id = "n1";
            if (string.IsNullOrEmpty(relationship.NodeRight.Id))
                relationship.NodeRight.Id = "n2";

            scriptBuilder.Append("MATCH ").Append(GenerateNode(relationship.NodeLeft)).Append(", ").AppendLine(GenerateNode(relationship.NodeRight)).Append(CREATE).Append(GenerateRelationship(relationship));

            return scriptBuilder.ToString();
        }

        private string GenerateRelationship(CreateAndSearchRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(relationship.NodeLeft.Id).Append(")-[");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.NodeRight.Id).Append(")");

            return scriptBuilder.ToString();
        }

        public string CreateAndSearch(IList<CreateAndSearchRelationship> relationships)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var relationship in relationships)
                scriptBuilder.AppendLine(CreateAndSearch(relationship));

            return scriptBuilder.ToString();
        }

        public string Merge(IList<CreateRelationship> relationships)
        {
            var scriptStringBuilder = new StringBuilder();
            scriptStringBuilder.AppendLine(MERGE);
            foreach (var relationship in relationships)
            {
                scriptStringBuilder.Append(GenerateRelationship(relationship));
                if (relationship != relationships.LastOrDefault())
                    scriptStringBuilder.AppendLine(", ");
            }
            return scriptStringBuilder.ToString();
        }

        public string Merge(CreateRelationship relationship)
        {
            bool needsVariable = relationship.OnCreateProperties?.Any() == true
                              || relationship.OnMatchProperties?.Any() == true;

            var sb = new StringBuilder(MERGE);
            sb.Append(needsVariable
                ? GenerateRelationshipWithVariable(relationship)
                : GenerateRelationship(relationship));

            AppendOnCreateAndMatch(sb, "r", relationship.OnCreateProperties, relationship.OnMatchProperties);
            return sb.ToString();
        }

        public string MergeAndSearch(CreateAndSearchRelationship relationship)
        {
            if (string.IsNullOrEmpty(relationship.NodeLeft.Id))
                relationship.NodeLeft.Id = "n1";
            if (string.IsNullOrEmpty(relationship.NodeRight.Id))
                relationship.NodeRight.Id = "n2";

            bool needsVariable = relationship.OnCreateProperties?.Any() == true
                              || relationship.OnMatchProperties?.Any() == true;

            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(MATCH).Append(GenerateNode(relationship.NodeLeft)).Append(", ").AppendLine(GenerateNode(relationship.NodeRight))
                         .Append(MERGE).Append(needsVariable
                             ? GenerateRelationshipWithVariable(relationship, relationship.NodeLeft.Id, relationship.NodeRight.Id)
                             : GenerateRelationship(relationship));

            AppendOnCreateAndMatch(scriptBuilder, "r", relationship.OnCreateProperties, relationship.OnMatchProperties);
            return scriptBuilder.ToString();
        }

        public string MergeAndSearch(IList<CreateAndSearchRelationship> relationships)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var relationship in relationships)
                scriptBuilder.AppendLine(MergeAndSearch(relationship));

            return scriptBuilder.ToString();
        }

        public string Delete(IList<CreateRelationship> relationships)
        {
            var scriptBuilder = new StringBuilder();
            foreach (var relationship in relationships)
                scriptBuilder.AppendLine(Delete(relationship));
            return scriptBuilder.ToString();
        }

        public string Delete(CreateRelationship relationship) =>
            MATCH + GenerateRelationshipWithVariable(relationship) + " " + DELETE + "r";

        public string DeleteAndSearch(CreateAndSearchRelationship relationship)
        {
            if (string.IsNullOrEmpty(relationship.NodeLeft.Id))
                relationship.NodeLeft.Id = "n1";
            if (string.IsNullOrEmpty(relationship.NodeRight.Id))
                relationship.NodeRight.Id = "n2";

            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(MATCH).Append(GenerateNode(relationship.NodeLeft)).Append(", ").AppendLine(GenerateNode(relationship.NodeRight))
                         .Append(MATCH).Append(GenerateRelationshipWithVariable(relationship, relationship.NodeLeft.Id, relationship.NodeRight.Id))
                         .Append(" ").Append(DELETE).Append("r");

            return scriptBuilder.ToString();
        }

        public string DeleteAndSearch(IList<CreateAndSearchRelationship> relationships)
        {
            var scriptBuilder = new StringBuilder();

            foreach (var relationship in relationships)
                scriptBuilder.AppendLine(DeleteAndSearch(relationship));

            return scriptBuilder.ToString();
        }

        private string GenerateRelationshipWithVariable(CreateRelationship relationship)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(relationship.NodeIdLeft).Append(")-[r");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(relationship.NodeIdRight).Append(")");

            return scriptBuilder.ToString();
        }

        private string GenerateRelationshipWithVariable(CreateAndSearchRelationship relationship, string nodeId1, string nodeId2)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append('(').Append(nodeId1).Append(")-[r");

            scriptBuilder.Append(GetLabels(relationship.Labels));

            if (relationship.Properties.Any())
                scriptBuilder.Append(GetProperties(relationship.Properties));

            scriptBuilder.Append("]->(").Append(nodeId2).Append(")");

            return scriptBuilder.ToString();
        }
    }
}
