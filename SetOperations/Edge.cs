namespace SetOperations
{
    public class Edge
    {
        public int Vertice1 { get; }
        public int Vertice2 { get; }
        public int Cost { get; }

        public Edge(int vertice1, int vertice2, int cost)
        {
            Vertice1 = vertice1;
            Vertice2 = vertice2;
            Cost = cost;
        }
    }
}
