using Cypher.ScriptGenerator.Generators;
using Cypher.ScriptGenerator.Models;
using System.Collections.Generic;
using Xunit;

namespace Cypher.ScriptGenerator.Test.Generators
{
    public class UnwindGeneratorTest
    {
        private readonly UnwindGenerator _unwindGenerator;

        public UnwindGeneratorTest()
        {
            _unwindGenerator = new UnwindGenerator();
        }

        #region Merge Nodes

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with match property and set properties")]
        public void MergeNodesWithMatchAndSet()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
                label:          "Person",
                matchProperty:  "id",
                setProperties:  new List<string> { "name", "age" }
            );

            Assert.Equal(
                "UNWIND $nodes AS row\r\nMERGE (n:Person {id: row.id})\r\nSET n.name = row.name, n.age = row.age",
                script);
        }

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with match property only")]
        public void MergeNodesWithMatchOnly()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
                label:         "Person",
                matchProperty: "id",
                setProperties: null
            );

            Assert.Equal(
                "UNWIND $nodes AS row\r\nMERGE (n:Person {id: row.id})",
                script);
        }

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with on create set only")]
        public void MergeNodesWithOnCreateSet()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
                label:               "Person",
                matchProperty:       "id",
                setProperties:       null,
                onCreateProperties:  new List<string> { "createdAt" },
                onMatchProperties:   null
            );

            Assert.Equal(
                "UNWIND $nodes AS row\r\nMERGE (n:Person {id: row.id})\r\nON CREATE SET n.createdAt = row.createdAt",
                script);
        }

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with on match set only")]
        public void MergeNodesWithOnMatchSet()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
                label:              "Person",
                matchProperty:      "id",
                setProperties:      null,
                onCreateProperties: null,
                onMatchProperties:  new List<string> { "updatedAt" }
            );

            Assert.Equal(
                "UNWIND $nodes AS row\r\nMERGE (n:Person {id: row.id})\r\nON MATCH SET n.updatedAt = row.updatedAt",
                script);
        }

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with set, on create set and on match set")]
        public void MergeNodesWithAllClauses()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
                label:              "Person",
                matchProperty:      "id",
                setProperties:      new List<string> { "name" },
                onCreateProperties: new List<string> { "createdAt" },
                onMatchProperties:  new List<string> { "updatedAt" }
            );

            Assert.Equal(
                "UNWIND $nodes AS row\r\nMERGE (n:Person {id: row.id})\r\nSET n.name = row.name\r\nON CREATE SET n.createdAt = row.createdAt\r\nON MATCH SET n.updatedAt = row.updatedAt",
                script);
        }

        [Trait("UnwindGenerator", "Merge nodes")]
        [Fact(DisplayName = "Merge nodes - with custom alias")]
        public void MergeNodesWithCustomAlias()
        {
            var script = _unwindGenerator.MergeNodes(
                new UnwindDefinition { Parameter = "$items", Alias = "item" },
                label:         "Product",
                matchProperty: "sku",
                setProperties: new List<string> { "name", "price" }
            );

            Assert.Equal(
                "UNWIND $items AS item\r\nMERGE (n:Product {sku: item.sku})\r\nSET n.name = item.name, n.price = item.price",
                script);
        }

        #endregion

        #region Merge Relationships

        [Trait("UnwindGenerator", "Merge relationships")]
        [Fact(DisplayName = "Merge relationships - without on create or on match set")]
        public void MergeRelationshipsSimple()
        {
            var script = _unwindGenerator.MergeRelationships(
                new UnwindDefinition { Parameter = "$rows", Alias = "row" },
                leftLabel:         "Person",  leftMatchProperty:  "id",
                rightLabel:        "Company", rightMatchProperty: "id",
                relationshipLabel: "WORKS_AT"
            );

            Assert.Equal(
                "UNWIND $rows AS row\r\nMATCH (a:Person {id: row.id})\r\nMATCH (b:Company {id: row.id})\r\nMERGE (a)-[:WORKS_AT]->(b)",
                script);
        }

        [Trait("UnwindGenerator", "Merge relationships")]
        [Fact(DisplayName = "Merge relationships - with on create set only")]
        public void MergeRelationshipsWithOnCreateSet()
        {
            var script = _unwindGenerator.MergeRelationships(
                new UnwindDefinition { Parameter = "$rows", Alias = "row" },
                leftLabel:          "Person",  leftMatchProperty:  "id",
                rightLabel:         "Company", rightMatchProperty: "id",
                relationshipLabel:  "WORKS_AT",
                onCreateProperties: new List<string> { "since" },
                onMatchProperties:  null
            );

            Assert.Equal(
                "UNWIND $rows AS row\r\nMATCH (a:Person {id: row.id})\r\nMATCH (b:Company {id: row.id})\r\nMERGE (a)-[r:WORKS_AT]->(b)\r\nON CREATE SET r.since = row.since",
                script);
        }

        [Trait("UnwindGenerator", "Merge relationships")]
        [Fact(DisplayName = "Merge relationships - with on match set only")]
        public void MergeRelationshipsWithOnMatchSet()
        {
            var script = _unwindGenerator.MergeRelationships(
                new UnwindDefinition { Parameter = "$rows", Alias = "row" },
                leftLabel:          "Person",  leftMatchProperty:  "id",
                rightLabel:         "Company", rightMatchProperty: "id",
                relationshipLabel:  "WORKS_AT",
                onCreateProperties: null,
                onMatchProperties:  new List<string> { "updatedAt" }
            );

            Assert.Equal(
                "UNWIND $rows AS row\r\nMATCH (a:Person {id: row.id})\r\nMATCH (b:Company {id: row.id})\r\nMERGE (a)-[r:WORKS_AT]->(b)\r\nON MATCH SET r.updatedAt = row.updatedAt",
                script);
        }

        [Trait("UnwindGenerator", "Merge relationships")]
        [Fact(DisplayName = "Merge relationships - with both on create and on match set")]
        public void MergeRelationshipsWithBothSets()
        {
            var script = _unwindGenerator.MergeRelationships(
                new UnwindDefinition { Parameter = "$rows", Alias = "row" },
                leftLabel:          "Person",  leftMatchProperty:  "id",
                rightLabel:         "Company", rightMatchProperty: "id",
                relationshipLabel:  "WORKS_AT",
                onCreateProperties: new List<string> { "since" },
                onMatchProperties:  new List<string> { "updatedAt" }
            );

            Assert.Equal(
                "UNWIND $rows AS row\r\nMATCH (a:Person {id: row.id})\r\nMATCH (b:Company {id: row.id})\r\nMERGE (a)-[r:WORKS_AT]->(b)\r\nON CREATE SET r.since = row.since\r\nON MATCH SET r.updatedAt = row.updatedAt",
                script);
        }

        #endregion
    }
}
