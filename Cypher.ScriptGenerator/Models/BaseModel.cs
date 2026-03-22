using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Labels = new List<string>();
            Properties = new Dictionary<string, object>();
            OnCreateProperties = new Dictionary<string, object>();
            OnMatchProperties = new Dictionary<string, object>();
        }

        public List<string> Labels { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Dictionary<string, object> OnCreateProperties { get; set; }
        public Dictionary<string, object> OnMatchProperties { get; set; }
    }
}
