namespace Cypher.ScriptGenerator.Models
{
    public class CreateRelationship : BaseModel
    {
        public string NodeIdLeft { get; set; }
        public string NodeIdRight { get; set; }
    }
}
