using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test.Generators
{
    public class ConstraintGeneratorTest
    {
        private readonly ConstraintGenerator _constraintGenerator;

        public ConstraintGeneratorTest()
        {
            _constraintGenerator = new ConstraintGenerator();
        }

        [Trait("ConstraintGenerator", "Create unique constraint")]
        [Fact(DisplayName = "Without name")]
        public void CreateUniqueWithoutName()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Label = "Person",
                Properties = new List<string> { "name" },
                Type = ConstraintType.Unique
            });

            Assert.Equal("CREATE CONSTRAINT FOR (n:Person) ASSERT n.name IS UNIQUE", script);
        }

        [Trait("ConstraintGenerator", "Create unique constraint")]
        [Fact(DisplayName = "With name")]
        public void CreateUniqueWithName()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Name = "person_name_unique",
                Label = "Person",
                Properties = new List<string> { "name" },
                Type = ConstraintType.Unique
            });

            Assert.Equal("CREATE CONSTRAINT person_name_unique FOR (n:Person) ASSERT n.name IS UNIQUE", script);
        }

        [Trait("ConstraintGenerator", "Create node key constraint")]
        [Fact(DisplayName = "Single property")]
        public void CreateNodeKeySingleProperty()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Label = "Person",
                Properties = new List<string> { "email" },
                Type = ConstraintType.NodeKey
            });

            Assert.Equal("CREATE CONSTRAINT FOR (n:Person) ASSERT (n.email) IS NODE KEY", script);
        }

        [Trait("ConstraintGenerator", "Create node key constraint")]
        [Fact(DisplayName = "Multiple properties")]
        public void CreateNodeKeyMultipleProperties()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Label = "Person",
                Properties = new List<string> { "name", "email" },
                Type = ConstraintType.NodeKey
            });

            Assert.Equal("CREATE CONSTRAINT FOR (n:Person) ASSERT (n.name, n.email) IS NODE KEY", script);
        }

        [Trait("ConstraintGenerator", "Create node key constraint")]
        [Fact(DisplayName = "With name and multiple properties")]
        public void CreateNamedNodeKeyMultipleProperties()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Name = "person_key",
                Label = "Person",
                Properties = new List<string> { "name", "email" },
                Type = ConstraintType.NodeKey
            });

            Assert.Equal("CREATE CONSTRAINT person_key FOR (n:Person) ASSERT (n.name, n.email) IS NODE KEY", script);
        }

        [Trait("ConstraintGenerator", "Create not null constraint")]
        [Fact(DisplayName = "Node property")]
        public void CreateNotNullNode()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Label = "Person",
                Properties = new List<string> { "name" },
                Type = ConstraintType.NotNull
            });

            Assert.Equal("CREATE CONSTRAINT FOR (n:Person) ASSERT n.name IS NOT NULL", script);
        }

        [Trait("ConstraintGenerator", "Create not null constraint")]
        [Fact(DisplayName = "Relationship property")]
        public void CreateNotNullRelationship()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Label = "KNOWS",
                Properties = new List<string> { "since" },
                Type = ConstraintType.NotNull,
                IsRelationship = true
            });

            Assert.Equal("CREATE CONSTRAINT FOR ()-[r:KNOWS]-() ASSERT r.since IS NOT NULL", script);
        }

        [Trait("ConstraintGenerator", "Create not null constraint")]
        [Fact(DisplayName = "With name")]
        public void CreateNotNullWithName()
        {
            var script = _constraintGenerator.Create(new ConstraintDefinition
            {
                Name = "person_name_not_null",
                Label = "Person",
                Properties = new List<string> { "name" },
                Type = ConstraintType.NotNull
            });

            Assert.Equal("CREATE CONSTRAINT person_name_not_null FOR (n:Person) ASSERT n.name IS NOT NULL", script);
        }

        [Trait("ConstraintGenerator", "Drop constraint")]
        [Fact(DisplayName = "Drop by name")]
        public void Drop()
        {
            var script = _constraintGenerator.Drop("person_name_unique");

            Assert.Equal("DROP CONSTRAINT person_name_unique", script);
        }
    }
}
