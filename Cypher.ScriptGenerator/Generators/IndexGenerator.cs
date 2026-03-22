using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{
    public class IndexGenerator : BaseGenerator, IIndexGenerator
    {
        public string Create(IndexDefinition index)
        {
            var sb = new StringBuilder("CREATE INDEX");

            if (!string.IsNullOrEmpty(index.Name))
                sb.Append(' ').Append(index.Name);

            sb.Append(" FOR ");

            string alias;
            if (index.IsRelationship)
            {
                alias = "r";
                sb.Append("()-[r:").Append(index.Label).Append("]-()");
            }
            else
            {
                alias = "n";
                sb.Append("(n:").Append(index.Label).Append(")");
            }

            sb.Append(" ON (");
            sb.Append(string.Join(", ", index.Properties.Select(p => $"{alias}.{p}")));
            sb.Append(")");

            return sb.ToString();
        }

        public string Drop(string indexName) =>
            $"DROP INDEX {indexName}";
    }
}
