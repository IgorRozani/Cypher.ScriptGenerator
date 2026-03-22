using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{
    public class UnwindGenerator : BaseGenerator, IUnwindGenerator
    {
        public string MergeNodes(UnwindDefinition unwind, string label,
                                 string matchProperty, IList<string> setProperties)
            => MergeNodes(unwind, label, matchProperty, setProperties,
                          onCreateProperties: null, onMatchProperties: null);

        public string MergeNodes(UnwindDefinition unwind, string label,
                                 string matchProperty, IList<string> setProperties,
                                 IList<string> onCreateProperties, IList<string> onMatchProperties)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"UNWIND {unwind.Parameter} AS {unwind.Alias}");
            sb.Append($"MERGE (n:{label}");
            sb.Append(GetPropertyReferences(new[] { matchProperty }, unwind.Alias));
            sb.Append(")");

            if (setProperties?.Any() == true)
            {
                sb.AppendLine();
                sb.Append("SET ");
                sb.Append(string.Join(", ", setProperties.Select(p => $"n.{p} = {unwind.Alias}.{p}")));
            }

            AppendOnCreateAndMatchByReference(sb, "n", unwind.Alias, onCreateProperties, onMatchProperties);

            return sb.ToString();
        }

        public string MergeRelationships(UnwindDefinition unwind,
                                         string leftLabel,  string leftMatchProperty,
                                         string rightLabel, string rightMatchProperty,
                                         string relationshipLabel)
            => MergeRelationships(unwind, leftLabel, leftMatchProperty, rightLabel, rightMatchProperty,
                                  relationshipLabel, onCreateProperties: null, onMatchProperties: null);

        public string MergeRelationships(UnwindDefinition unwind,
                                         string leftLabel,  string leftMatchProperty,
                                         string rightLabel, string rightMatchProperty,
                                         string relationshipLabel,
                                         IList<string> onCreateProperties, IList<string> onMatchProperties)
        {
            bool needsVariable = onCreateProperties?.Any() == true || onMatchProperties?.Any() == true;

            var sb = new StringBuilder();
            sb.AppendLine($"UNWIND {unwind.Parameter} AS {unwind.Alias}");
            sb.AppendLine($"MATCH (a:{leftLabel}{GetPropertyReferences(new[] { leftMatchProperty }, unwind.Alias)})");
            sb.AppendLine($"MATCH (b:{rightLabel}{GetPropertyReferences(new[] { rightMatchProperty }, unwind.Alias)})");

            if (needsVariable)
                sb.Append($"MERGE (a)-[r:{relationshipLabel}]->(b)");
            else
                sb.Append($"MERGE (a)-[:{relationshipLabel}]->(b)");

            AppendOnCreateAndMatchByReference(sb, "r", unwind.Alias, onCreateProperties, onMatchProperties);

            return sb.ToString();
        }

        private void AppendOnCreateAndMatchByReference(StringBuilder sb, string nodeAlias, string rowAlias,
                                                        IList<string> onCreateProperties, IList<string> onMatchProperties)
        {
            if (onCreateProperties?.Any() == true)
            {
                sb.AppendLine();
                sb.Append("ON CREATE SET ");
                sb.Append(string.Join(", ", onCreateProperties.Select(p => $"{nodeAlias}.{p} = {rowAlias}.{p}")));
            }

            if (onMatchProperties?.Any() == true)
            {
                sb.AppendLine();
                sb.Append("ON MATCH SET ");
                sb.Append(string.Join(", ", onMatchProperties.Select(p => $"{nodeAlias}.{p} = {rowAlias}.{p}")));
            }
        }
    }
}
