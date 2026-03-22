using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Models
{
    public class IndexDefinition
    {
        public IndexDefinition()
        {
            Properties = new List<string>();
        }

        public string Name { get; set; }
        public string Label { get; set; }
        public List<string> Properties { get; set; }
        public bool IsRelationship { get; set; }
    }
}
