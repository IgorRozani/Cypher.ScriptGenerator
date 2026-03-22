# Cypher.ScriptGenerator

A .NET library for generating [Cypher](https://neo4j.com/docs/cypher-manual/current/) scripts for Neo4j — supporting **CREATE**, **MERGE**, **DELETE**, **UNWIND**, **indexes**, and **constraints**.

## Installation

Install via NuGet: [Cypher.ScriptGenerator](https://www.nuget.org/packages/Cypher.ScriptGenerator/)

```bash
dotnet add package Cypher.ScriptGenerator
```

```powershell
Install-Package Cypher.ScriptGenerator
```

---

## Dependency Injection

All generators implement an interface, making them easy to register with DI:

```csharp
services.AddSingleton<INodeGenerator, NodeGenerator>();
services.AddSingleton<IRelationshipGenerator, RelationshipGenerator>();
services.AddSingleton<IIndexGenerator, IndexGenerator>();
services.AddSingleton<IConstraintGenerator, ConstraintGenerator>();
services.AddSingleton<IUnwindGenerator, UnwindGenerator>();
```

| Class                   | Interface               |
|-------------------------|-------------------------|
| `NodeGenerator`         | `INodeGenerator`        |
| `RelationshipGenerator` | `IRelationshipGenerator`|
| `IndexGenerator`        | `IIndexGenerator`       |
| `ConstraintGenerator`   | `IConstraintGenerator`  |
| `UnwindGenerator`       | `IUnwindGenerator`      |

---

## Nodes

Use `NodeGenerator` (or `INodeGenerator`) to generate node scripts.

### Node model

| Property              | Type                          | Description                                        |
|-----------------------|-------------------------------|----------------------------------------------------|
| `Id`                  | `string`                      | Variable name used to reference the node in script |
| `Labels`              | `List<string>`                | Node labels                                        |
| `Properties`          | `Dictionary<string, object>`  | Node properties                                    |
| `OnCreateProperties`  | `Dictionary<string, object>`  | Properties set only on CREATE (used with MERGE)    |
| `OnMatchProperties`   | `Dictionary<string, object>`  | Properties set only on MATCH (used with MERGE)     |

### CREATE

```csharp
var generator = new NodeGenerator();

// Single node
generator.Create(new Node
{
    Id = "pikachu",
    Labels = new List<string> { "Pokemon", "Electric" },
    Properties = new Dictionary<string, object>
    {
        { "name", "Pikachu" },
        { "number", 25 }
    }
});
// → CREATE (pikachu:Pokemon:Electric {name:"Pikachu", number:25})

// Multiple nodes
generator.Create(new List<Node> { node1, node2 });
// → CREATE
//   (pikachu:Pokemon:Electric {name:"Pikachu", number:25}),
//   (evee:Pokemon:Normal {name:"Evee", number:133})
```

### MERGE

```csharp
// Simple merge
generator.Merge(new Node
{
    Id = "pikachu",
    Labels = new List<string> { "Pokemon" },
    Properties = new Dictionary<string, object> { { "name", "Pikachu" } }
});
// → MERGE (pikachu:Pokemon {name:"Pikachu"})

// With ON CREATE SET and ON MATCH SET
generator.Merge(new Node
{
    Id = "pikachu",
    Labels = new List<string> { "Pokemon" },
    Properties = new Dictionary<string, object> { { "name", "Pikachu" } },
    OnCreateProperties = new Dictionary<string, object> { { "created", "2024" }, { "number", 25 } },
    OnMatchProperties  = new Dictionary<string, object> { { "updated", "2024" } }
});
// → MERGE (pikachu:Pokemon {name:"Pikachu"})
//   ON CREATE SET pikachu.created = "2024", pikachu.number = 25
//   ON MATCH SET pikachu.updated = "2024"
```

### DELETE

```csharp
// Detach delete (default) — also removes connected relationships
generator.Delete(new Node
{
    Id = "pikachu",
    Labels = new List<string> { "Pokemon" },
    Properties = new Dictionary<string, object> { { "name", "Pikachu" } }
});
// → MATCH (pikachu:Pokemon {name:"Pikachu"}) DETACH DELETE pikachu

// Delete without detach
generator.Delete(node, detach: false);
// → MATCH (pikachu:Pokemon) DELETE pikachu

// Delete multiple nodes
generator.Delete(new List<Node> { node1, node2 });
// → MATCH (pikachu:Pokemon) DETACH DELETE pikachu
//   MATCH (evee:Pokemon) DETACH DELETE evee
```

---

## Relationships

Use `RelationshipGenerator` (or `IRelationshipGenerator`) to generate relationship scripts.

### Models

**`CreateRelationship`** — used when both nodes already exist in the script (referenced by ID):

| Property              | Type                          | Description                                      |
|-----------------------|-------------------------------|--------------------------------------------------|
| `NodeIdLeft`             | `string`                      | Variable name for the left-side node             |
| `NodeIdRight`             | `string`                      | Variable name for the right-side node            |
| `Labels`              | `List<string>`                | Relationship type/labels                         |
| `Properties`          | `Dictionary<string, object>`  | Relationship properties                          |
| `OnCreateProperties`  | `Dictionary<string, object>`  | Properties set only on CREATE (used with MERGE)  |
| `OnMatchProperties`   | `Dictionary<string, object>`  | Properties set only on MATCH (used with MERGE)   |

**`CreateAndSearchRelationship`** — used when the nodes need to be matched first:

| Property              | Type                          | Description                                      |
|-----------------------|-------------------------------|--------------------------------------------------|
| `NodeLeft`            | `Node`                        | Left-side node (will be MATCHed)                 |
| `NodeRight`           | `Node`                        | Right-side node (will be MATCHed)                |
| `Labels`              | `List<string>`                | Relationship type/labels                         |
| `Properties`          | `Dictionary<string, object>`  | Relationship properties                          |
| `OnCreateProperties`  | `Dictionary<string, object>`  | Properties set only on CREATE (used with MERGE)  |
| `OnMatchProperties`   | `Dictionary<string, object>`  | Properties set only on MATCH (used with MERGE)   |

### CREATE

```csharp
var generator = new RelationshipGenerator();

// Using node IDs already present in the script
generator.Create(new CreateRelationship
{
    NodeIdLeft = "pikachu",
    NodeIdRight = "ash",
    Labels  = new List<string> { "OWNED_BY" }
});
// → CREATE (pikachu)-[:OWNED_BY]->(ash)

// Matching nodes first, then creating relationship
generator.CreateAndSearch(new CreateAndSearchRelationship
{
    NodeLeft   = new Node { Labels = new List<string> { "Pokemon" }, Properties = new Dictionary<string, object> { { "name", "Pikachu" } } },
    NodeRight  = new Node { Labels = new List<string> { "Trainer" }, Properties = new Dictionary<string, object> { { "name", "Ash" } } },
    Labels     = new List<string> { "OWNED_BY" },
    Properties = new Dictionary<string, object> { { "since", 1997 } }
});
// → MATCH (n1:Pokemon {name:"Pikachu"}), (n2:Trainer {name:"Ash"})
//   CREATE (n1)-[:OWNED_BY {since:1997}]->(n2)
```

### MERGE

```csharp
// Simple merge
generator.Merge(new CreateRelationship
{
    NodeIdLeft = "pikachu",
    NodeIdRight = "ash",
    Labels  = new List<string> { "OWNED_BY" }
});
// → MERGE (pikachu)-[:OWNED_BY]->(ash)

// With ON CREATE SET and ON MATCH SET
generator.Merge(new CreateRelationship
{
    NodeIdLeft              = "pikachu",
    NodeIdRight              = "ash",
    Labels               = new List<string> { "OWNED_BY" },
    OnCreateProperties   = new Dictionary<string, object> { { "since", 1997 } },
    OnMatchProperties    = new Dictionary<string, object> { { "updated", "2024" } }
});
// → MERGE (pikachu)-[r:OWNED_BY]->(ash)
//   ON CREATE SET r.since = 1997
//   ON MATCH SET r.updated = "2024"

// Matching nodes first, then merging
generator.MergeAndSearch(new CreateAndSearchRelationship { ... });
```

### DELETE

```csharp
// Delete a relationship (nodes referenced by ID)
generator.Delete(new CreateRelationship
{
    NodeIdLeft = "pikachu",
    NodeIdRight = "ash",
    Labels  = new List<string> { "OWNED_BY" }
});
// → MATCH (pikachu)-[r:OWNED_BY]->(ash) DELETE r

// Match nodes first, then delete relationship
generator.DeleteAndSearch(new CreateAndSearchRelationship
{
    NodeLeft  = new Node { Labels = new List<string> { "Pokemon" }, Properties = new Dictionary<string, object> { { "name", "Pikachu" } } },
    NodeRight = new Node { Labels = new List<string> { "Trainer" }, Properties = new Dictionary<string, object> { { "name", "Ash" } } },
    Labels = new List<string> { "OWNED_BY" }
});
// → MATCH (n1:Pokemon {name:"Pikachu"}), (n2:Trainer {name:"Ash"})
//   MATCH (n1)-[r:OWNED_BY]->(n2) DELETE r
```

---

## Indexes

Use `IndexGenerator` (or `IIndexGenerator`) to generate index scripts.

### IndexDefinition model

| Property         | Type            | Description                                   |
|------------------|-----------------|-----------------------------------------------|
| `Name`           | `string`        | Optional index name                           |
| `Label`          | `string`        | Node label or relationship type               |
| `Properties`     | `List<string>`  | Properties to index                           |
| `IsRelationship` | `bool`          | `true` to create an index on a relationship   |

### CREATE INDEX

```csharp
var generator = new IndexGenerator();

// Node index
generator.Create(new IndexDefinition
{
    Label      = "Person",
    Properties = new List<string> { "name" }
});
// → CREATE INDEX FOR (n:Person) ON (n.name)

// Named composite index
generator.Create(new IndexDefinition
{
    Name       = "person_name_age",
    Label      = "Person",
    Properties = new List<string> { "name", "age" }
});
// → CREATE INDEX person_name_age FOR (n:Person) ON (n.name, n.age)

// Relationship index
generator.Create(new IndexDefinition
{
    Label         = "KNOWS",
    Properties    = new List<string> { "since" },
    IsRelationship = true
});
// → CREATE INDEX FOR ()-[r:KNOWS]-() ON (r.since)
```

### DROP INDEX

```csharp
generator.Drop("person_name_age");
// → DROP INDEX person_name_age
```

---

## Constraints

Use `ConstraintGenerator` (or `IConstraintGenerator`) to generate constraint scripts.

### ConstraintDefinition model

| Property         | Type              | Description                                          |
|------------------|-------------------|------------------------------------------------------|
| `Name`           | `string`          | Optional constraint name                             |
| `Label`          | `string`          | Node label or relationship type                      |
| `Properties`     | `List<string>`    | Properties targeted by the constraint                |
| `Type`           | `ConstraintType`  | `Unique`, `NodeKey`, or `NotNull`                    |
| `IsRelationship` | `bool`            | `true` to apply the constraint to a relationship     |

### CREATE CONSTRAINT

```csharp
var generator = new ConstraintGenerator();

// Unique constraint
generator.Create(new ConstraintDefinition
{
    Name       = "person_name_unique",
    Label      = "Person",
    Properties = new List<string> { "name" },
    Type       = ConstraintType.Unique
});
// → CREATE CONSTRAINT person_name_unique FOR (n:Person) ASSERT n.name IS UNIQUE

// Node key (composite)
generator.Create(new ConstraintDefinition
{
    Label      = "Person",
    Properties = new List<string> { "name", "email" },
    Type       = ConstraintType.NodeKey
});
// → CREATE CONSTRAINT FOR (n:Person) ASSERT (n.name, n.email) IS NODE KEY

// Not null — node property
generator.Create(new ConstraintDefinition
{
    Label      = "Person",
    Properties = new List<string> { "name" },
    Type       = ConstraintType.NotNull
});
// → CREATE CONSTRAINT FOR (n:Person) ASSERT n.name IS NOT NULL

// Not null — relationship property
generator.Create(new ConstraintDefinition
{
    Label          = "KNOWS",
    Properties     = new List<string> { "since" },
    Type           = ConstraintType.NotNull,
    IsRelationship = true
});
// → CREATE CONSTRAINT FOR ()-[r:KNOWS]-() ASSERT r.since IS NOT NULL
```

### DROP CONSTRAINT

```csharp
generator.Drop("person_name_unique");
// → DROP CONSTRAINT person_name_unique
```

---

## UNWIND

Use `UnwindGenerator` (or `IUnwindGenerator`) para operações em lote — itera sobre uma lista e aplica `MERGE` em cada elemento. Ideal para importações de dados.

### UnwindDefinition model

| Property    | Type     | Description                                                        |
|-------------|----------|--------------------------------------------------------------------|
| `Parameter` | `string` | Lista a iterar: parâmetro (`"$nodes"`) ou literal (`"[1, 2, 3]"`) |
| `Alias`     | `string` | Nome da variável para cada elemento (`"row"`, `"item"`, `"node"`)  |

### UNWIND + MERGE nodes

```csharp
var generator = new UnwindGenerator();

// Merge simples com SET
generator.MergeNodes(
    new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
    label:         "Person",
    matchProperty: "id",
    setProperties: new List<string> { "name", "age" }
);
// → UNWIND $nodes AS row
//   MERGE (n:Person {id: row.id})
//   SET n.name = row.name, n.age = row.age

// Com ON CREATE SET e ON MATCH SET
generator.MergeNodes(
    new UnwindDefinition { Parameter = "$nodes", Alias = "row" },
    label:              "Person",
    matchProperty:      "id",
    setProperties:      new List<string> { "name" },
    onCreateProperties: new List<string> { "createdAt" },
    onMatchProperties:  new List<string> { "updatedAt" }
);
// → UNWIND $nodes AS row
//   MERGE (n:Person {id: row.id})
//   SET n.name = row.name
//   ON CREATE SET n.createdAt = row.createdAt
//   ON MATCH SET n.updatedAt = row.updatedAt
```

### UNWIND + MERGE relationships

```csharp
// Merge simples de relacionamentos
generator.MergeRelationships(
    new UnwindDefinition { Parameter = "$rows", Alias = "row" },
    leftLabel:         "Person",  leftMatchProperty:  "id",
    rightLabel:        "Company", rightMatchProperty: "id",
    relationshipLabel: "WORKS_AT"
);
// → UNWIND $rows AS row
//   MATCH (a:Person {id: row.id})
//   MATCH (b:Company {id: row.id})
//   MERGE (a)-[:WORKS_AT]->(b)

// Com ON CREATE SET e ON MATCH SET
generator.MergeRelationships(
    new UnwindDefinition { Parameter = "$rows", Alias = "row" },
    leftLabel:          "Person",  leftMatchProperty:  "id",
    rightLabel:         "Company", rightMatchProperty: "id",
    relationshipLabel:  "WORKS_AT",
    onCreateProperties: new List<string> { "since" },
    onMatchProperties:  new List<string> { "updatedAt" }
);
// → UNWIND $rows AS row
//   MATCH (a:Person {id: row.id})
//   MATCH (b:Company {id: row.id})
//   MERGE (a)-[r:WORKS_AT]->(b)
//   ON CREATE SET r.since = row.since
//   ON MATCH SET r.updatedAt = row.updatedAt
```

---

## Supported property types

The following C# types are supported as property values:

| C# Type    | Cypher output example                                           |
|------------|-----------------------------------------------------------------|
| `string`   | `name:"Pikachu"`                                                |
| `int`      | `number:25`                                                     |
| `long`     | `price:12`                                                      |
| `float`    | `price:12.89`                                                   |
| `double`   | `price:12.89`                                                   |
| `decimal`  | `price:12.89`                                                   |
| `bool`     | `active:True`                                                   |
| `DateTime` | `date:datetime({year:2019, month:5, day:13, hour:16, minute:18, second:50})` |

---

## Tests

The project has full unit test coverage. See the test files for all scenarios:

- [NodeGeneratorTest.cs](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/NodeGeneratorTest.cs)
- [RelationshipGeneratorTest.cs](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/RelationshipGeneratorTest.cs)
- [IndexGeneratorTest.cs](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/IndexGeneratorTest.cs)
- [ConstraintGeneratorTest.cs](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/ConstraintGeneratorTest.cs)
- [UnwindGeneratorTest.cs](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/UnwindGeneratorTest.cs)
