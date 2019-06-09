using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test
{
    public class NodeGeneratorTest
    {
        private NodeGenerator _nodeGenerator;

        public NodeGeneratorTest()
        {
            _nodeGenerator = new NodeGenerator();
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "Empty node")]
        public void EmptyNode()
        {
            var script = _nodeGenerator.CreateNode(new Node());

            Assert.Equal("CREATE ()", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With id")]
        public void WithId()
        {
            var script = _nodeGenerator.CreateNode(new Node { Id = "pikachu" });

            Assert.Equal("CREATE (pikachu)", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With one label")]
        public void WithOneLabel()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Labels = new List<string> { "Pokemon" }
            });

            Assert.Equal("CREATE (:Pokemon)", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With multiple labels")]
        public void WithMultipleLabels()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Labels = new List<string> { "Pokemon", "Electric" }
            });

            Assert.Equal("CREATE (:Pokemon:Electric)", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With string property")]
        public void WithStringProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "name","Pikachu" }
                }
            });

            Assert.Equal("CREATE ( {name:\"Pikachu\"})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With int tproperty")]
        public void WithIntProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "number",12 }
                }
            });

            Assert.Equal("CREATE ( {number:12})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With decimal tproperty")]
        public void WithDecimalProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89m}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With float property")]
        public void WithFloatProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89f}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With double property")]
        public void WithDoubleProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",12.89}
                }
            });

            Assert.Equal("CREATE ( {price:12.89})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With long property")]
        public void WithLongProperty()
        {
            long price = 12;
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price",price}
                }
            });

            Assert.Equal("CREATE ( {price:12})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With datetime property")]
        public void WithDateTimeProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "date",new DateTime(2019,5,13, 16,18,50)}
                }
            });

            Assert.Equal("CREATE ( {date:datetime({year:2019, month:5, day:13, hour:16, minute:18, second:50})})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With multiple properties")]
        public void WithMultipleProperties()
        {
            var script = _nodeGenerator.CreateNode(new Node
            {
                Properties = new Dictionary<string, object>
                {
                    { "price", 51.50m},
                    { "description", "glue"}
                }
            });

            Assert.Equal("CREATE ( {price:51.50, description:\"glue\"})", script);
        }

        [Trait("NodeGenerator", "CreateNode")]
        [Fact(DisplayName = "With id, label and property")]
        public void WithIdAndLabelAndProperty()
        {
            var script = _nodeGenerator.CreateNode(new Node
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


        [Trait("NodeGenerator", "CreateNodes")]
        [Fact(DisplayName = "Multiple nodes")]
        public void MultipleNodes()
        {
            var script = _nodeGenerator.CreateNodes(
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
                }

                );

            Assert.Equal("CREATE \r\n(pikachu:Pokemon:Electric {name:\"Pikachu\", number:25}), \r\n(evee:Pokemon:Normal {name:\"Evee\", number:133})", script);
        }
    }
}
