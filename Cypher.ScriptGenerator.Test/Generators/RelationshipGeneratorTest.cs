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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "Without properties and labels")]
        public void WithoutPropertiesAndLabels()
        {
            var script = _relationshipGenerator.CreateRelationship(
                    new Relationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                    });

            Assert.Equal("CREATE (evee)-[]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With one label")]
        public void WithOneLabel()
        {
            var script = _relationshipGenerator.CreateRelationship(
                    new Relationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                        Labels = new List<string> { "Evolve" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With multiple labels")]
        public void WithMultipleLabels()
        {
            var script = _relationshipGenerator.CreateRelationship(
                    new Relationship
                    {
                        NodeId1 = "evee",
                        NodeId2 = "vaporeon",
                        Labels = new List<string> { "Evolve", "Stone" },
                    });

            Assert.Equal("CREATE (evee)-[:Evolve:Stone]->(vaporeon)", script);
        }

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With string property")]
        public void WithStringProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With int tproperty")]
        public void WithIntProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With decimal tproperty")]
        public void WithDecimalProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With float property")]
        public void WithFloatProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With double property")]
        public void WithDoubleProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With long property")]
        public void WithLongProperty()
        {
            long price = 12;
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With datetime property")]
        public void WithDateTimeProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With bool property")]
        public void WithBoolProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationship")]
        [Fact(DisplayName = "With multiple properties")]
        public void WithMultipleProperties()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With id, label and property")]
        public void WithIdAndLabelAndProperty()
        {
            var script = _relationshipGenerator.CreateRelationship(new Relationship
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

        [Trait("RelationshipGenerator", "CreateRelationships")]
        [Fact(DisplayName = "Multiple nodes")]
        public void MultipleNodes()
        {
            var script = _relationshipGenerator.CreateRelationships(
                new List<Relationship> {
                    new Relationship
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
                    new Relationship
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
    }
}
