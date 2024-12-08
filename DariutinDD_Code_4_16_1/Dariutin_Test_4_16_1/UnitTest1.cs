namespace FordFulkerson.Tests
{
    public class MaxFlowTests
    {
        [Fact]
        public void CalculateMaxFlow_SimpleGraph_ReturnsCorrectMaxFlow()
        {
            int[,] capacity = {
                { 0, 16, 13, 0, 0, 0 },
                { 0, 0, 10, 12, 0, 0 },
                { 0, 4, 0, 0, 14, 0 },
                { 0, 0, 9, 0, 0, 20 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
            };

            Graph graph = new Graph(capacity);
            MaxFlowCalculator calculator = new MaxFlowCalculator(graph);

            int source = 0;
            int sink = 5;

            int maxFlow = calculator.CalculateMaxFlow(source, sink);
            Assert.Equal(23, maxFlow);
        }

        [Fact]
        public void CalculateMaxFlow_ComplexGraph_ReturnsCorrectMaxFlow()
        {
            int[,] capacity = {
                { 0, 10, 10, 0, 0, 0 },
                { 0, 0, 2, 4, 8, 0 },
                { 0, 0, 0, 0, 9, 0 },
                { 0, 0, 0, 0, 0, 10 },
                { 0, 0, 0, 6, 0, 10 },
                { 0, 0, 0, 0, 0, 0 }
            };

            Graph graph = new Graph(capacity);
            MaxFlowCalculator calculator = new MaxFlowCalculator(graph);

            int source = 0;
            int sink = 5;

            int maxFlow = calculator.CalculateMaxFlow(source, sink);
            Assert.Equal(19, maxFlow);
        }

        [Fact]
        public void CalculateMaxFlow_NoPath_ReturnsZero()
        {
            int[,] capacity = {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            Graph graph = new Graph(capacity);
            MaxFlowCalculator calculator = new MaxFlowCalculator(graph);

            int source = 0;
            int sink = 2;

            int maxFlow = calculator.CalculateMaxFlow(source, sink);
            Assert.Equal(0, maxFlow);
        }
    }
}