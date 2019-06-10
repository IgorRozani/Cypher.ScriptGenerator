namespace Cypher.ScriptGenerator.Models
{

    public class CreateAndSearchRelationship : BaseModel
    {
        public CreateAndSearchRelationship() : base()
        {
            Node1 = new Node();
            Node2 = new Node();
        }

        public Node Node1 { get; set; }
        public Node Node2 { get; set; }
    }
}
