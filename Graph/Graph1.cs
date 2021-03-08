using ConsoleApp1.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class AdjencyListModal
    {
        public string vertex;
        public int weight;
        public AdjencyListModal(string vertex, int weight = 1)
        {
            this.vertex = vertex;
            this.weight = weight;
        }
    }

    public class Graph1: IGraph
    {
        public Dictionary<string, LinkedList<AdjencyListModal>> adjencyList;
        public Graph1()
        {
            adjencyList = new Dictionary<string, LinkedList<AdjencyListModal>>();
        }

        public void AddEdge(string fromVertex, string toVertex, int weight)
        {
            if (IsVertexPresent(fromVertex) && IsVertexPresent(toVertex))
            {
                var adList = adjencyList[fromVertex];
                adList.AddLast(new AdjencyListModal(toVertex, weight));
                // undirected graph
                //adList = adjencyList[toVertex];
                //adList.AddLast(new AdjencyListModal(fromVertex, weight));
            }
        }

        public bool AddVertex(string vertex)
        {
            if (IsVertexPresent(vertex))
            {
                return false;
            }
            else
            {
                adjencyList.Add(vertex, new LinkedList<AdjencyListModal>());
                return true;
            }
        }

        public IEnumerator<AdjencyListModal> GetAdjacentVertex(string vertex)
        {
            var adList = adjencyList[vertex];
            return adList.GetEnumerator();
        }

        public IEnumerator<string> GetVertexes()
        {
            return adjencyList.Keys.GetEnumerator();
        }

        public bool IsVertexPresent(string vertex)
        {
            return adjencyList.ContainsKey(vertex);
        }

        public IEnumerable<Tuple<string, string, int>> GetEdges()
        {
            foreach(var key in adjencyList.Keys)
            {
                var iterator = GetAdjacentVertex(key);
                while (iterator.MoveNext())
                {
                    yield return Tuple.Create(key, iterator.Current.vertex, iterator.Current.weight);
                }
            }
        }

        public void BFS(string vertex)
        {
            var visitedVertex = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(vertex);
            while (queue.Count > 0)
            {
                string ver = queue.Dequeue();
                if (!visitedVertex.Contains(ver))
                {
                    Console.WriteLine(ver);
                    visitedVertex.Add(ver);
                    var iterator = GetAdjacentVertex(ver);
                    while (iterator.MoveNext())
                    {
                        queue.Enqueue(iterator.Current.vertex);
                    }
                }
            }
        }

        public void DFS(string vertex)
        {
            var visitedVertex = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(vertex);
            while (stack.Count > 0)
            {
                string ver = stack.Pop();
                if (!visitedVertex.Contains(ver))
                {
                    Console.WriteLine(ver);
                    visitedVertex.Add(ver);
                    var iterator = GetAdjacentVertex(ver);
                    while (iterator.MoveNext())
                    {
                        stack.Push(iterator.Current.vertex);
                    }
                }
            }
        }

        public void Initialization()
        {
            Graph1 graph1 = new Graph1();
            graph1.AddVertex("A");
            graph1.AddVertex("B");
            graph1.AddVertex("C");
            graph1.AddVertex("D");
            graph1.AddVertex("E");
            graph1.AddEdge("A", "D", 2);
            graph1.AddEdge("D", "A", 10);
            graph1.AddEdge("D", "C", 7);
            graph1.AddEdge("C", "A", 12);
            graph1.AddEdge("C", "B", 4);
            graph1.AddEdge("E", "C", 3);

            foreach(var item in graph1.GetEdges())
            {
                Console.WriteLine("Edge from {0} to {1} with weight {2}", item.Item1, item.Item2, item.Item3);
            }
            graph1.BFS("E");
            graph1.DFS("E");
            Console.ReadKey();
        }
    }
}
