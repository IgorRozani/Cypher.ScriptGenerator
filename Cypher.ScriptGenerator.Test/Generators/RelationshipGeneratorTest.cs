using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test.Generators
{
    public class RelationshipGeneratorTest
    {
        private RelationshipGenerator _relationshipGenerator;

        public RelationshipGeneratorTest()
        {
            _relationshipGenerator = new RelationshipGenerator();
        }

        #region Create

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - without label and properties")]
        public void WithoutPropertiesAndLabels()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeIdLeft = "evee",
                        NodeIdRight = "vaporeon",
                    });

            Assert.Equal("CREATE (evee)-[]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with one label")]
        public void WithOneLabel()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeIdLeft = "evee",
                        NodeIdRight = "vaporeon",
                        Labels = new List<string> { "Evolve" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with multiple labels")]
        public void WithMultipleLabels()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeIdLeft = "evee",
                        NodeIdRight = "vaporeon",
                        Labels = new List<string> { "Evolve", "Stone" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve:Stone]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with string property")]
        public void WithStringProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "name","Pikachu" }
                }
            });

            Assert.Equal("CREATE (evee)-[ {name:\"Pikachu\"}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with int property")]
        public void WithIntProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "number",12 }
                }
            });

            Assert.Equal("CREATE (evee)-[ {number:12}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with decimal property")]
        public void WithDecimalProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89m}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with float property")]
        public void WithFloatProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89f}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with double property")]
        public void WithDoubleProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with long property")]
        public void WithLongProperty()
        {
            long price = 12;
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",price}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with datetime property")]
        public void WithDateTimeProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "date",new DateTime(2019,5,13, 16,18,50)}
                }
            });

            Assert.Equal("CREATE (evee)-[ {date:datetime({year:2019, month:5, day:13, hour:16, minute:18, second:50})}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with bool property")]
        public void WithBoolProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "active",true}
                }
            });

            Assert.Equal("CREATE (evee)-[ {active:True}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with multiple properties")]
        public void WithMultipleProperties()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price", 51.50m},
                    { "description", "glue"}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:51.50, description:\"glue\"}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - with multiple labels and properties")]
        public void WithIdAndLabelAndProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve", "Stone" },
                Properties = new Dictionary<string, object>
                {
                    { "stone", "Water stone" },
                    { "hasOtherForms", false }
                }
            });

            Assert.Equal("CREATE (evee)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create")]
        [Fact(DisplayName = "Create - multiple relationships")]
        public void MultipleRelationships()
        {
            var script = _relationshipGenerator.Create(
                new List<CreateRelationship> {
                    new CreateRelationship
                    {
                        NodeIdLeft = "evee",
                        NodeIdRight = "vaporeon",
                        Labels = new List<string>{"Evolve"},
                        Properties = new Dictionary<string, object>
                        {
                            { "stone", "Water stone" },
                            { "hasOtherForms", false }
                        }
                    },
                    new CreateRelationship
                    {
                        NodeIdLeft = "evee",
                        NodeIdRight = "jolteon",
                        Labels = new List<string>{"Evolve"},
                        Properties = new Dictionary<string, object>
                        {
                             { "stone", "Thunder stone" },
                            { "hasOtherForms", false }
                        }
                    },
                });

            Assert.Equal("CREATE \n(evee)-[:Evolve {stone:\"Water stone\", hasOtherForms:False}]->(vaporeon), \n(evee)-[:Evolve {stone:\"Thunder stone\", hasOtherForms:False}]->(jolteon)", script);
        }

        #endregion

        #region Create and Search

        [Trait("RelationshipGenerator", "Create and Search")]
        [Fact(DisplayName = "Create and search - single relationship with label and properties")]
        public void TwoNodesWithProperty()
        {
            var script = _relationshipGenerator.CreateAndSearch(new CreateAndSearchRelationship
            {
                NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve", "Stone" },
                Properties = new Dictionary<string, object>
                    {
                        { "stone", "Water stone" },
                        { "hasOtherForms", false }
                    }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nCREATE (n1)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(n2)", script);
        }

        [Trait("RelationshipGenerator", "Create and Search")]
        [Fact(DisplayName = "Create and search - multiple relationships with label and properties")]
        public void MultiplesNodesAndRelationships()
        {
            var script = _relationshipGenerator.CreateAndSearch(new List<CreateAndSearchRelationship>{
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                    Labels = new List<string> { "Evolve", "Stone" },
                    Properties = new Dictionary<string, object>
                        {
                            { "stone", "Water stone" },
                            { "hasOtherForms", false }
                        }
                },
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Jolteon" } } },
                    Labels = new List<string> { "Evolve", "Stone" },
                    Properties = new Dictionary<string, object>
                        {
                            { "stone", "Light stone" },
                            { "hasOtherForms", false }
                        }
                }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nCREATE (n1)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(n2)\nMATCH (n1 {name:\"Evee\"}), (n2 {name:\"Jolteon\"})\nCREATE (n1)-[:Evolve:Stone {stone:\"Light stone\", hasOtherForms:False}]->(n2)\n", script);
        }

        #endregion

        #region Merge

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - without label and properties")]
        public void MergeWithoutPropertiesAndLabels()
        {
            var script = _relationshipGenerator.Merge(
                new CreateRelationship
                {
                    NodeIdLeft = "evee",
                    NodeIdRight = "vaporeon",
                });

            Assert.Equal("MERGE (evee)-[]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with label and property")]
        public void MergeWithLabelAndProperty()
        {
            var script = _relationshipGenerator.Merge(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve" },
                Properties = new Dictionary<string, object> { { "stone", "Water stone" } }
            });

            Assert.Equal("MERGE (evee)-[:Evolve {stone:\"Water stone\"}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with on create set only")]
        public void MergeWithOnCreateSet()
        {
            var script = _relationshipGenerator.Merge(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve" },
                OnCreateProperties = new Dictionary<string, object> { { "since", 2020 } }
            });

            Assert.Equal("MERGE (evee)-[r:Evolve]->(vaporeon) ON CREATE SET r.since = 2020", script);
        }

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with on match set only")]
        public void MergeWithOnMatchSet()
        {
            var script = _relationshipGenerator.Merge(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve" },
                OnMatchProperties = new Dictionary<string, object> { { "updated", "2024" } }
            });

            Assert.Equal("MERGE (evee)-[r:Evolve]->(vaporeon) ON MATCH SET r.updated = \"2024\"", script);
        }

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with both on create and on match set")]
        public void MergeWithBothOnCreateAndOnMatchSet()
        {
            var script = _relationshipGenerator.Merge(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve" },
                OnCreateProperties = new Dictionary<string, object> { { "since", 2020 }, { "type", "water" } },
                OnMatchProperties = new Dictionary<string, object> { { "updated", "2024" } }
            });

            Assert.Equal("MERGE (evee)-[r:Evolve]->(vaporeon) ON CREATE SET r.since = 2020, r.type = \"water\" ON MATCH SET r.updated = \"2024\"", script);
        }

        [Trait("RelationshipGenerator", "Merge")]
        [Fact(DisplayName = "Merge - multiple relationships")]
        public void MergeMultipleRelationships()
        {
            var script = _relationshipGenerator.Merge(
                new List<CreateRelationship>
                {
                    new CreateRelationship { NodeIdLeft = "evee", NodeIdRight = "vaporeon", Labels = new List<string> { "Evolve" } },
                    new CreateRelationship { NodeIdLeft = "evee", NodeIdRight = "jolteon", Labels = new List<string> { "Evolve" } },
                });

            Assert.Equal("MERGE \n(evee)-[:Evolve]->(vaporeon), \n(evee)-[:Evolve]->(jolteon)", script);
        }

        #endregion

        #region Merge and Search

        [Trait("RelationshipGenerator", "Merge and Search")]
        [Fact(DisplayName = "Merge and search - single relationship with label and property")]
        public void MergeAndSearchTwoNodesWithProperty()
        {
            var script = _relationshipGenerator.MergeAndSearch(new CreateAndSearchRelationship
            {
                NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve" },
                Properties = new Dictionary<string, object> { { "stone", "Water stone" } }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMERGE (n1)-[:Evolve {stone:\"Water stone\"}]->(n2)", script);
        }

        [Trait("RelationshipGenerator", "Merge and Search")]
        [Fact(DisplayName = "Merge and search - single relationship with on create set only")]
        public void MergeAndSearchWithOnCreateSet()
        {
            var script = _relationshipGenerator.MergeAndSearch(new CreateAndSearchRelationship
            {
                NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve" },
                OnCreateProperties = new Dictionary<string, object> { { "since", 2020 } }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMERGE (n1)-[r:Evolve]->(n2) ON CREATE SET r.since = 2020", script);
        }

        [Trait("RelationshipGenerator", "Merge and Search")]
        [Fact(DisplayName = "Merge and search - single relationship with both on create and on match set")]
        public void MergeAndSearchWithBothSets()
        {
            var script = _relationshipGenerator.MergeAndSearch(new CreateAndSearchRelationship
            {
                NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve" },
                OnCreateProperties = new Dictionary<string, object> { { "since", 2020 } },
                OnMatchProperties = new Dictionary<string, object> { { "updated", "2024" } }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMERGE (n1)-[r:Evolve]->(n2) ON CREATE SET r.since = 2020 ON MATCH SET r.updated = \"2024\"", script);
        }

        [Trait("RelationshipGenerator", "Merge and Search")]
        [Fact(DisplayName = "Merge and search - multiple relationships with label")]
        public void MergeAndSearchMultipleRelationships()
        {
            var script = _relationshipGenerator.MergeAndSearch(new List<CreateAndSearchRelationship>
            {
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                    Labels = new List<string> { "Evolve" }
                },
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Jolteon" } } },
                    Labels = new List<string> { "Evolve" }
                }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMERGE (n1)-[:Evolve]->(n2)\nMATCH (n1 {name:\"Evee\"}), (n2 {name:\"Jolteon\"})\nMERGE (n1)-[:Evolve]->(n2)\n", script);
        }

        #endregion

        #region Delete

        [Trait("RelationshipGenerator", "Delete")]
        [Fact(DisplayName = "Delete - single relationship without label and properties")]
        public void DeleteWithoutPropertiesAndLabels()
        {
            var script = _relationshipGenerator.Delete(
                new CreateRelationship
                {
                    NodeIdLeft = "evee",
                    NodeIdRight = "vaporeon",
                });

            Assert.Equal("MATCH (evee)-[r]->(vaporeon) DELETE r", script);
        }

        [Trait("RelationshipGenerator", "Delete")]
        [Fact(DisplayName = "Delete - single relationship with label and property")]
        public void DeleteWithLabelAndProperty()
        {
            var script = _relationshipGenerator.Delete(new CreateRelationship
            {
                NodeIdLeft = "evee",
                NodeIdRight = "vaporeon",
                Labels = new List<string> { "Evolve" },
                Properties = new Dictionary<string, object> { { "stone", "Water stone" } }
            });

            Assert.Equal("MATCH (evee)-[r:Evolve {stone:\"Water stone\"}]->(vaporeon) DELETE r", script);
        }

        [Trait("RelationshipGenerator", "Delete")]
        [Fact(DisplayName = "Delete - multiple relationships with label")]
        public void DeleteMultipleRelationships()
        {
            var script = _relationshipGenerator.Delete(
                new List<CreateRelationship>
                {
                    new CreateRelationship { NodeIdLeft = "evee", NodeIdRight = "vaporeon", Labels = new List<string> { "Evolve" } },
                    new CreateRelationship { NodeIdLeft = "evee", NodeIdRight = "jolteon",  Labels = new List<string> { "Evolve" } },
                });

            Assert.Equal("MATCH (evee)-[r:Evolve]->(vaporeon) DELETE r\nMATCH (evee)-[r:Evolve]->(jolteon) DELETE r\n", script);
        }

        #endregion

        #region Delete and Search

        [Trait("RelationshipGenerator", "Delete and Search")]
        [Fact(DisplayName = "Delete and search - single relationship with label")]
        public void DeleteAndSearchTwoNodesWithProperty()
        {
            var script = _relationshipGenerator.DeleteAndSearch(new CreateAndSearchRelationship
            {
                NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve" }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMATCH (n1)-[r:Evolve]->(n2) DELETE r", script);
        }

        [Trait("RelationshipGenerator", "Delete and Search")]
        [Fact(DisplayName = "Delete and search - multiple relationships with label")]
        public void DeleteAndSearchMultipleRelationships()
        {
            var script = _relationshipGenerator.DeleteAndSearch(new List<CreateAndSearchRelationship>
            {
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                    Labels = new List<string> { "Evolve" }
                },
                new CreateAndSearchRelationship
                {
                    NodeLeft =new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    NodeRight =new Node { Properties = new Dictionary<string, object> { { "name", "Jolteon" } } },
                    Labels = new List<string> { "Evolve" }
                }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\nMATCH (n1)-[r:Evolve]->(n2) DELETE r\nMATCH (n1 {name:\"Evee\"}), (n2 {name:\"Jolteon\"})\nMATCH (n1)-[r:Evolve]->(n2) DELETE r\n", script);
        }

        #endregion
    }
}
