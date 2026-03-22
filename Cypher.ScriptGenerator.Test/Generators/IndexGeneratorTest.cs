using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test.Generators
{
    public class IndexGeneratorTest
    {
        private readonly IndexGenerator _indexGenerator;

        public IndexGeneratorTest()
        {
            _indexGenerator = new IndexGenerator();
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "Without name")]
        public void CreateWithoutName()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Label = "Person",
                Properties = new List<string> { "name" }
            });

            Assert.Equal("CREATE INDEX FOR (n:Person) ON (n.name)", script);
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "With name")]
        public void CreateWithName()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Name = "person_name",
                Label = "Person",
                Properties = new List<string> { "name" }
            });

            Assert.Equal("CREATE INDEX person_name FOR (n:Person) ON (n.name)", script);
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "Composite index")]
        public void CreateComposite()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Label = "Person",
                Properties = new List<string> { "name", "age" }
            });

            Assert.Equal("CREATE INDEX FOR (n:Person) ON (n.name, n.age)", script);
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "Named composite index")]
        public void CreateNamedComposite()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Name = "person_name_age",
                Label = "Person",
                Properties = new List<string> { "name", "age" }
            });

            Assert.Equal("CREATE INDEX person_name_age FOR (n:Person) ON (n.name, n.age)", script);
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "Relationship index")]
        public void CreateForRelationship()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Label = "KNOWS",
                Properties = new List<string> { "since" },
                IsRelationship = true
            });

            Assert.Equal("CREATE INDEX FOR ()-[r:KNOWS]-() ON (r.since)", script);
        }

        [Trait("IndexGenerator", "Create index")]
        [Fact(DisplayName = "Named relationship index")]
        public void CreateNamedForRelationship()
        {
            var script = _indexGenerator.Create(new IndexDefinition
            {
                Name = "knows_since",
                Label = "KNOWS",
                Properties = new List<string> { "since" },
                IsRelationship = true
            });

            Assert.Equal("CREATE INDEX knows_since FOR ()-[r:KNOWS]-() ON (r.since)", script);
        }

        [Trait("IndexGenerator", "Drop index")]
        [Fact(DisplayName = "Drop by name")]
        public void Drop()
        {
            var script = _indexGenerator.Drop("person_name");

            Assert.Equal("DROP INDEX person_name", script);
        }
    }
}
