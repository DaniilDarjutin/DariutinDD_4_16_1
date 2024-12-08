using System.Diagnostics;

namespace FordFulkerson
{
    public class Graph
    {
        public int[,] Capacity { get; }
        public int VertexCount => Capacity.GetLength(0);

        public Graph(int[,] capacity)
        {
            Capacity = capacity;
        }
    }

    public class MaxFlowCalculator
    {
        private readonly Graph _graph;
        private int[,] _flow;

        public MaxFlowCalculator(Graph graph)
        {
            _graph = graph;
            _flow = new int[_graph.VertexCount, _graph.VertexCount];
        }

        public int CalculateMaxFlow(int source, int sink)
        {
            int maxFlow = 0;

            while (true)
            {
                int[] parent = new int[_graph.VertexCount];
                Array.Fill(parent, -1);

                if (!FindAugmentingPath(source, sink, parent))
                    break;

                int pathFlow = int.MaxValue;
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, _graph.Capacity[u, v] - _flow[u, v]);
                }

                UpdateFlow(source, sink, parent, pathFlow);

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        private void UpdateFlow(int source, int sink, int[] parent, int pathFlow)
        {
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                _flow[u, v] += pathFlow;
                _flow[v, u] -= pathFlow;
            }
        }

        private bool FindAugmentingPath(int source, int sink, int[] parent)
        {
            bool[] visited = new bool[_graph.VertexCount];
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < _graph.VertexCount; v++)
                {
                    if (!visited[v] && _graph.Capacity[u, v] > _flow[u, v])
                    {
                        parent[v] = u;
                        visited[v] = true;

                        if (v == sink)
                            return true;

                        queue.Enqueue(v);
                    }
                }
            }

            return false;
        }
    }

    class Program
    {
        static void Main()
        {
            int[,] capacity = {
                { 0, 16, 13, 0, 0, 0 },
                { 0, 0, 10, 12, 0, 0 },
                { 0, 4, 0, 0, 14, 0 },
                { 0, 0, 9, 0, 0, 20 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
            };
            
            var stopwatch = Stopwatch.StartNew();

            Graph graph = new Graph(capacity);
            MaxFlowCalculator calculator = new MaxFlowCalculator(graph);

            int source = 0;
            int sink = 5;

            int maxFlow = calculator.CalculateMaxFlow(source, sink);
            
            stopwatch.Stop();

            Console.WriteLine($"Максимальный поток: {maxFlow}");
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} миллисекунд");
        }
    }
}