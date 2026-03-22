namespace Cypher.ScriptGenerator.Models
{
    public class UnwindDefinition
    {
        /// <summary>
        /// The list to iterate over. Can be a parameter (e.g. "$nodes") or an inline list (e.g. "[1, 2, 3]").
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// The alias for each element in the list (e.g. "row", "item", "node").
        /// </summary>
        public string Alias { get; set; }
    }
}
