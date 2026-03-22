using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Models
{
    public enum ConstraintType
    {
        Unique,
        NodeKey,
        NotNull
    }

    public class ConstraintDefinition
    {
        public ConstraintDefinition()
        {
            Properties = new List<string>();
        }

        public string Name { get; set; }
        public string Label { get; set; }
        public List<string> Properties { get; set; }
        public ConstraintType Type { get; set; }
        public bool IsRelationship { get; set; }
    }
}
