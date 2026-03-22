namespace Cypher.ScriptGenerator.Models
{

    public class CreateAndSearchRelationship : BaseModel
    {
        public CreateAndSearchRelationship() : base()
        {
            NodeLeft = new Node();
            NodeRight = new Node();
        }

        public Node NodeLeft { get; set; }
        public Node NodeRight { get; set; }
    }
}
