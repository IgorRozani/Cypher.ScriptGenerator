using Cypher.ScriptGenerator.Interfaces;
using Cypher.ScriptGenerator.Models;
using System.Linq;
using System.Text;

namespace Cypher.ScriptGenerator.Generators
{
    public class ConstraintGenerator : BaseGenerator, IConstraintGenerator
    {
        public string Create(ConstraintDefinition constraint)
        {
            var sb = new StringBuilder("CREATE CONSTRAINT");

            if (!string.IsNullOrEmpty(constraint.Name))
                sb.Append(' ').Append(constraint.Name);

            sb.Append(" FOR ");

            string alias;
            if (constraint.IsRelationship)
            {
                alias = "r";
                sb.Append("()-[r:").Append(constraint.Label).Append("]-()");
            }
            else
            {
                alias = "n";
                sb.Append("(n:").Append(constraint.Label).Append(")");
            }

            sb.Append(" ASSERT ");

            switch (constraint.Type)
            {
                case ConstraintType.Unique:
                    sb.Append($"{alias}.{constraint.Properties[0]} IS UNIQUE");
                    break;

                case ConstraintType.NodeKey:
                    sb.Append("(");
                    sb.Append(string.Join(", ", constraint.Properties.Select(p => $"{alias}.{p}")));
                    sb.Append(") IS NODE KEY");
                    break;

                case ConstraintType.NotNull:
                    sb.Append($"{alias}.{constraint.Properties[0]} IS NOT NULL");
                    break;
            }

            return sb.ToString();
        }

        public string Drop(string constraintName) =>
            $"DROP CONSTRAINT {constraintName}";
    }
}
