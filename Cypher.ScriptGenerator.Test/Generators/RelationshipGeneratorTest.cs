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

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "Without properties and labels")]
        public void WithoutPropertiesAndLabels()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                    });

            Assert.Equal("CREATE (evee)-[]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With one label")]
        public void WithOneLabel()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                        Labels = new List<string> { "Evolve" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With multiple labels")]
        public void WithMultipleLabels()
        {
            var script = _relationshipGenerator.Create(
                    new CreateRelationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                        Labels = new List<string> { "Evolve", "Stone" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve:Stone]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With string property")]
        public void WithStringProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "name","Pikachu" }
                }
            });

            Assert.Equal("CREATE (evee)-[ {name:\"Pikachu\"}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With int property")]
        public void WithIntProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "number",12 }
                }
            });

            Assert.Equal("CREATE (evee)-[ {number:12}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With decimal tproperty")]
        public void WithDecimalProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89m}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With float property")]
        public void WithFloatProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89f}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With double property")]
        public void WithDoubleProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12.89}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With long property")]
        public void WithLongProperty()
        {
            long price = 12;
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price",price}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:12}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With datetime property")]
        public void WithDateTimeProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "date",new DateTime(2019,5,13, 16,18,50)}
                }
            });

            Assert.Equal("CREATE (evee)-[ {date:datetime({year:2019, month:5, day:13, hour:16, minute:18, second:50})}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With bool property")]
        public void WithBoolProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "active",true}
                }
            });

            Assert.Equal("CREATE (evee)-[ {active:True}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With multiple properties")]
        public void WithMultipleProperties()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Properties = new Dictionary<string, object>
                {
                    { "price", 51.50m},
                    { "description", "glue"}
                }
            });

            Assert.Equal("CREATE (evee)-[ {price:51.50, description:\"glue\"}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create a relationship")]
        [Fact(DisplayName = "With id, label and property")]
        public void WithIdAndLabelAndProperty()
        {
            var script = _relationshipGenerator.Create(new CreateRelationship
            {
                NodeId1 = "evee",
                NodeId2 = "vaporeon",
                Labels = new List<string> { "Evolve", "Stone" },
                Properties = new Dictionary<string, object>
                {
                    { "stone", "Water stone" },
                    { "hasOtherForms", false }
                }
            });

            Assert.Equal("CREATE (evee)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "Create multiple relationships")]
        [Fact(DisplayName = "Multiple nodes")]
        public void MultipleNodes()
        {
            var script = _relationshipGenerator.Create(
                new List<CreateRelationship> {
                    new CreateRelationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                        Labels = new List<string>{"Evolve"},
                        Properties = new Dictionary<string, object>
                        {
                            { "stone", "Water stone" },
                            { "hasOtherForms", false }
                        }
                    },
                    new CreateRelationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "jolteon",
                        Labels = new List<string>{"Evolve"},
                        Properties = new Dictionary<string, object>
                        {
                             { "stone", "Thunder stone" },
                            { "hasOtherForms", false }
                        }
                    },
                });

            Assert.Equal("CREATE \r\n(evee)-[:Evolve {stone:\"Water stone\", hasOtherForms:False}]->(vaporeon), \r\n(evee)-[:Evolve {stone:\"Thunder stone\", hasOtherForms:False}]->(jolteon)", script);
        }

        [Trait("RelationshipGenerator", "Create and search relationship")]
        [Fact(DisplayName = "Two nodes with property")]
        public void TwoNodesWithProperty()
        {
            var script = _relationshipGenerator.CreateAndSearch(new CreateAndSearchRelationship
            {
                Node1 = new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                Node2 = new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                Labels = new List<string> { "Evolve", "Stone" },
                Properties = new Dictionary<string, object>
                    {
                        { "stone", "Water stone" },
                        { "hasOtherForms", false }
                    }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\r\nCREATE (n1)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(n2)", script);
        }


        [Trait("RelationshipGenerator", "Create and search multiple relationships")]
        [Fact(DisplayName = "Two nodes with property")]
        public void MultiplesNodesAndRelationships()
        {
            var script = _relationshipGenerator.CreateAndSearch(new List<CreateAndSearchRelationship>{
                new CreateAndSearchRelationship
                {
                    Node1 = new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    Node2 = new Node { Properties = new Dictionary<string, object> { { "name", "Vaporeon" } } },
                    Labels = new List<string> { "Evolve", "Stone" },
                    Properties = new Dictionary<string, object>
                        {
                            { "stone", "Water stone" },
                            { "hasOtherForms", false }
                        }
                },
                new CreateAndSearchRelationship
                {
                    Node1 = new Node { Properties = new Dictionary<string, object> { { "name", "Evee" } } },
                    Node2 = new Node { Properties = new Dictionary<string, object> { { "name", "Jolteon" } } },
                    Labels = new List<string> { "Evolve", "Stone" },
                    Properties = new Dictionary<string, object>
                        {
                            { "stone", "Light stone" },
                            { "hasOtherForms", false }
                        }
                }
            });

            Assert.Equal("MATCH (n1 {name:\"Evee\"}), (n2 {name:\"Vaporeon\"})\r\nCREATE (n1)-[:Evolve:Stone {stone:\"Water stone\", hasOtherForms:False}]->(n2)\r\nMATCH (n1 {name:\"Evee\"}), (n2 {name:\"Jolteon\"})\r\nCREATE (n1)-[:Evolve:Stone {stone:\"Light stone\", hasOtherForms:False}]->(n2)\r\n", script);
        }
    }
}
