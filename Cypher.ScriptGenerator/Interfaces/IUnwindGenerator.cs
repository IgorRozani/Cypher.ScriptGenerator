using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;

namespace Cypher.ScriptGenerator.Interfaces
{
    public interface IUnwindGenerator
    {
        /// <summary>
        /// UNWIND ... MERGE (n:Label {matchProp: alias.matchProp}) SET n.p1 = alias.p1, ...
        /// </summary>
        string MergeNodes(UnwindDefinition unwind, string label,
                          string matchProperty, IList<string> setProperties);

        /// <summary>
        /// UNWIND ... MERGE (n:Label {matchProp: alias.matchProp}) ON CREATE SET ... ON MATCH SET ...
        /// </summary>
        string MergeNodes(UnwindDefinition unwind, string label,
                          string matchProperty, IList<string> setProperties,
                          IList<string> onCreateProperties, IList<string> onMatchProperties);

        /// <summary>
        /// UNWIND ... MATCH (a:LeftLabel {lProp: alias.lProp}) MATCH (b:RightLabel {rProp: alias.rProp}) MERGE (a)-[:REL]->(b)
        /// </summary>
        string MergeRelationships(UnwindDefinition unwind,
                                  string leftLabel,  string leftMatchProperty,
                                  string rightLabel, string rightMatchProperty,
                                  string relationshipLabel);

        /// <summary>
        /// UNWIND ... MATCH (a:LeftLabel {lProp: alias.lProp}) MATCH (b:RightLabel {rProp: alias.rProp}) MERGE (a)-[r:REL]->(b) ON CREATE SET ... ON MATCH SET ...
        /// </summary>
        string MergeRelationships(UnwindDefinition unwind,
                                  string leftLabel,  string leftMatchProperty,
                                  string rightLabel, string rightMatchProperty,
                                  string relationshipLabel,
                                  IList<string> onCreateProperties, IList<string> onMatchProperties);
    }
}
