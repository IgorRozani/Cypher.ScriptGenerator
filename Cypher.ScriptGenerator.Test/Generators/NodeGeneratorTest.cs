using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test.Generators
{
    public class NodeGeneratorTest
    {
        private NodeGenerator _nodeGenerator;

        public NodeGeneratorTest()
        {
            _nodeGenerator = new NodeGenerator();
        }

        #region Create

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - empty node")]
        public void EmptyNode()
        {
            var script = _nodeGenerator.Create(new Node());

            Assert.Equal("CREATE ()", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with id")]
        public void WithId()
        {
            var script = _nodeGenerator.Create(new Node { Id = "pikachu" });

            Assert.Equal("CREATE (pikachu)", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with one label")]
        public void WithOneLabel()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Labels = new List<string> { "Pokemon" }
            });

            Assert.Equal("CREATE (:Pokemon)", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with multiple labels")]
        public void WithMultipleLabels()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Labels = new List<string> { "Pokemon", "Electric" }
            });

            Assert.Equal("CREATE (:Pokemon:Electric)", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with string property")]
        public void WithStringProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "name","Pikachu" }
                }
            });

            Assert.Equal("CREATE ( {name:\"Pikachu\"})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with int property")]
        public void WithIntProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "number",12 }
                }
            });

            Assert.Equal("CREATE ( {number:12})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with decimal property")]
        public void WithDecimalProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89m}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with float property")]
        public void WithFloatProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89f}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with double property")]
        public void WithDoubleProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with long property")]
        public void WithLongProperty()
        {
            long price = 12;
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",price}
                }
            });

            Assert.Equal("CREATE ( {price:12})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with datetime property")]
        public void WithDateTimeProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "date",new DateTime(2019,5,13, 16,18,50)}
                }
            });

            Assert.Equal("CREATE ( {date:datetime({year:2019, month:5, day:13, hour:16, minute:18, second:50})})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with bool property")]
        public void WithBoolProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "active",true}
                }
            });

            Assert.Equal("CREATE ( {active:True})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with multiple properties")]
        public void WithMultipleProperties()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price", 51.50m},
                    { "description", "glue"}
                }
            });

            Assert.Equal("CREATE ( {price:51.50, description:\"glue\"})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - with id, label and property")]
        public void WithIdAndLabelAndProperty()
        {
            var script = _nodeGenerator.Create(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon", "Electric" },
                Properties = new Dictionary<string, object>
                {
                    { "name", "Pikachu"},
                    { "number", 25}
                }
            });

            Assert.Equal("CREATE (pikachu:Pokemon:Electric {name:\"Pikachu\", number:25})", script);
        }

        [Trait("NodeGenerator", "Create")]
        [Fact(DisplayName = "Create - multiple nodes")]
        public void MultipleNodes()
        {
            var script = _nodeGenerator.Create(
                new List<Node>
                {
                    new Node
                    {
                        Id = "pikachu",
                        Labels = new List<string> { "Pokemon", "Electric" },
                        Properties = new Dictionary<string, object>
                        {
                            { "name", "Pikachu"},
                            { "number", 25}
                        }
                    },
                    new Node
                    {
                        Id = "evee",
                        Labels = new List<string> { "Pokemon", "Normal" },
                        Properties = new Dictionary<string, object>
                        {
                            { "name", "Evee"},
                            { "number", 133}
                        }
                    },
                });

            Assert.Equal("CREATE \n(pikachu:Pokemon:Electric {name:\"Pikachu\", number:25}), \n(evee:Pokemon:Normal {name:\"Evee\", number:133})", script);
        }

        #endregion

        #region Merge

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - empty node")]
        public void MergeEmptyNode()
        {
            var script = _nodeGenerator.Merge(new Node());

            Assert.Equal("MERGE ()", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with id")]
        public void MergeWithId()
        {
            var script = _nodeGenerator.Merge(new Node { Id = "pikachu" });

            Assert.Equal("MERGE (pikachu)", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with one label")]
        public void MergeWithOneLabel()
        {
            var script = _nodeGenerator.Merge(new Node
            {
                Labels = new List<string> { "Pokemon" }
            });

            Assert.Equal("MERGE (:Pokemon)", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with id, label and property")]
        public void MergeWithIdAndLabelAndProperty()
        {
            var script = _nodeGenerator.Merge(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon", "Electric" },
                Properties = new Dictionary<string, object>
                {
                    { "name", "Pikachu"},
                    { "number", 25}
                }
            });

            Assert.Equal("MERGE (pikachu:Pokemon:Electric {name:\"Pikachu\", number:25})", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with on create set only")]
        public void MergeWithOnCreateSet()
        {
            var script = _nodeGenerator.Merge(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon" },
                Properties = new Dictionary<string, object> { { "name", "Pikachu" } },
                OnCreateProperties = new Dictionary<string, object> { { "created", "2024" } }
            });

            Assert.Equal("MERGE (pikachu:Pokemon {name:\"Pikachu\"}) ON CREATE SET pikachu.created = \"2024\"", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with on match set only")]
        public void MergeWithOnMatchSet()
        {
            var script = _nodeGenerator.Merge(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon" },
                Properties = new Dictionary<string, object> { { "name", "Pikachu" } },
                OnMatchProperties = new Dictionary<string, object> { { "updated", "2024" } }
            });

            Assert.Equal("MERGE (pikachu:Pokemon {name:\"Pikachu\"}) ON MATCH SET pikachu.updated = \"2024\"", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - with both on create and on match set")]
        public void MergeWithBothOnCreateAndOnMatchSet()
        {
            var script = _nodeGenerator.Merge(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon" },
                Properties = new Dictionary<string, object> { { "name", "Pikachu" } },
                OnCreateProperties = new Dictionary<string, object> { { "created", "2024" }, { "number", 25 } },
                OnMatchProperties = new Dictionary<string, object> { { "updated", "2024" } }
            });

            Assert.Equal("MERGE (pikachu:Pokemon {name:\"Pikachu\"}) ON CREATE SET pikachu.created = \"2024\", pikachu.number = 25 ON MATCH SET pikachu.updated = \"2024\"", script);
        }

        [Trait("NodeGenerator", "Merge")]
        [Fact(DisplayName = "Merge - multiple nodes")]
        public void MergeMultipleNodes()
        {
            var script = _nodeGenerator.Merge(
                new List<Node>
                {
                    new Node
                    {
                        Id = "pikachu",
                        Labels = new List<string> { "Pokemon", "Electric" },
                        Properties = new Dictionary<string, object>
                        {
                            { "name", "Pikachu"},
                            { "number", 25}
                        }
                    },
                    new Node
                    {
                        Id = "evee",
                        Labels = new List<string> { "Pokemon", "Normal" },
                        Properties = new Dictionary<string, object>
                        {
                            { "name", "Evee"},
                            { "number", 133}
                        }
                    },
                });

            Assert.Equal("MERGE \n(pikachu:Pokemon:Electric {name:\"Pikachu\", number:25}), \n(evee:Pokemon:Normal {name:\"Evee\", number:133})", script);
        }

        #endregion

        #region Delete

        [Trait("NodeGenerator", "Delete")]
        [Fact(DisplayName = "Delete - single node with detach delete")]
        public void DeleteWithDetach()
        {
            var script = _nodeGenerator.Delete(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon" },
                Properties = new Dictionary<string, object> { { "name", "Pikachu" } }
            });

            Assert.Equal("MATCH (pikachu:Pokemon {name:\"Pikachu\"}) DETACH DELETE pikachu", script);
        }

        [Trait("NodeGenerator", "Delete")]
        [Fact(DisplayName = "Delete - single node without detach")]
        public void DeleteWithoutDetach()
        {
            var script = _nodeGenerator.Delete(new Node
            {
                Id = "pikachu",
                Labels = new List<string> { "Pokemon" }
            }, detach: false);

            Assert.Equal("MATCH (pikachu:Pokemon) DELETE pikachu", script);
        }

        [Trait("NodeGenerator", "Delete")]
        [Fact(DisplayName = "Delete - multiple nodes with detach delete")]
        public void DeleteMultipleNodes()
        {
            var script = _nodeGenerator.Delete(
                new List<Node>
                {
                    new Node { Id = "pikachu", Labels = new List<string> { "Pokemon" } },
                    new Node { Id = "evee",    Labels = new List<string> { "Pokemon" } },
                });

            Assert.Equal("MATCH (pikachu:Pokemon) DETACH DELETE pikachu\nMATCH (evee:Pokemon) DETACH DELETE evee\n", script);
        }

        #endregion
    }
}
