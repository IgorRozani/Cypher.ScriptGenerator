using Cypher.ScriptGenerator.Models;

namespace Cypher.ScriptGenerator.Interfaces
{
    public interface IConstraintGenerator
    {
        string Create(ConstraintDefinition constraint);
        string Drop(string constraintName);
    }
}
