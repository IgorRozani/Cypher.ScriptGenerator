# Cypher.ScriptGenerator
A script generator for cypher

This project is based in the [Cypher manual from the Neo4j site](https://neo4j.com/docs/cypher-manual/3.5/).

## Nuget.org
Include this in your project using the nuget package: [Cypher.ScriptGenerator](https://www.nuget.org/packages/Cypher.ScriptGenerator/).

```
Install-Package Cypher.ScriptGenerator
```

## How to use

The package has two main files, the RelationshipGenerator and NodeGenerator.

### Configure Depedency injection

You can configure the dependency injection for both classes because both classes implement an interface.

| Class                  | Interface              |
|------------------------|------------------------|
| RelationshipGenerator  | IRelationshipGenerator |
| NodeGenerator          | INodeGenerator         |

### Create nodes

To create a node utilize the class NodeGenerator or the interface INodeGenerator. This class has two methods:

| Method | Params      | Description |
|--------|-------------|-------------|
| Create | IList<Node> | To create multiple nodes |
| Create | Node        | To create a single node |


Both methods recieve the Node class, that represents the data from the node.

| Property   | Type                       |Description|
|------------|----------------------------|-----------|
| Id         | string                     |Node id to be used in the same script|
| Labels     | List<string>               |Node labels|
| Properties | Dictionary<string, object> |The node properties|

### Create relationships

To create a relationship utilize the class RelationshipGenerator or the interface IRelationshipGenerator. This class has four methods:

| Method          | Params                             | Description |
|-----------------|------------------------------------|-------------|
| Create          | IList<CreateRelationship>          | To create multiple relationships |
| Create          | CreateRelationship                 | To create a single relationship |
| CreateAndSearch | CreateAndSearchRelationship        | To create and search a single relationship |
| CreateAndSearch | IList<CreateAndSearchRelationship> | To create and search multiple relationship |

#### Create

Both Create methods recieve the CreateRelationship class, that represents the data from the relationship.
| Property   | Type                       |Description|
|------------|----------------------------|-----------|
| NodeId1    | string                     |Node id to be used in the left side from the relationship|
| NodeId2    | string                     |Node id to be used in the right side from the relationship|
| Labels     | List<string>               |Node labels|
| Properties | Dictionary<string, object> |The node properties|

#### CreateAndSearch

Both CreateAndSearch methods recieve the CreateAndSearchRelationship class, that represents the data from the relationship.
| Property   | Type                       |Description|
|------------|----------------------------|-----------|
| Node1      | Node                       |Node id to be searched and used in the left side from the relationship|
| Node2      | Node                       |Node id to be searched and used in the right side from the relationship|
| Labels     | List<string>               |Node labels|
| Properties | Dictionary<string, object> |The node properties|
The class node utilized in this method is the same class utilized in the method Create from the class [NodeGenerator](#create-nodes).

This project has unity tests and they cover all the cases, check it to see more details. [Click here for the node tests](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/NodeGeneratorTest.cs) and [here for the relationship tests](https://github.com/IgorRozani/Cypher.ScriptGenerator/blob/master/Cypher.ScriptGenerator.Test/Generators/RelationshipGeneratorTest.cs).
