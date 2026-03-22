using Cypher.ScriptGenerator.Models;

namespace Cypher.ScriptGenerator.Interfaces
{
    public interface IIndexGenerator
    {
        string Create(IndexDefinition index);
        string Drop(string indexName);
    }
}
