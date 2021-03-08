using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Graph
{
    interface IGraph
    {
        //Add a node to the graph.
        bool AddVertex(string vertex);
        //Create an edge between any two nodes.
        void AddEdge(string fromVertex, string toVertex, int weight);
        //Check if a node exists in the graph.
        bool IsVertexPresent(string vertex);
        //Given a node, return it’s neighbors.
        IEnumerator<AdjencyListModal> GetAdjacentVertex(string vertex);
        //Return a list of all the nodes in the graph.
        IEnumerator<string> GetVertexes();
        //Return a list of all edges in the graph.
        IEnumerable<Tuple<string, string, int>> GetEdges();
    }
}
