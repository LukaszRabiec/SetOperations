using System;

namespace SetOperations.Client
{
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            //TestSet();
            TestKruskal();
            Console.ReadKey();
        }

        private static void TestSet()
        {
            var set = new Set(new[] { 1, 3, 5, 7, 2, 4, 8, 6 });

            Console.WriteLine(set.ToString());

            TryUnion(set, 1, 2, 1);
            TryUnion(set, 0, 1, 1);
            TryFind(set, 0);
            TryFind(set, 1);
            TryFind(set, 2);
            Console.WriteLine();
            TryUnion(set, 3, 4, 3);
            TryUnion(set, 1, 3, 1);
            TryFind(set, 3);
            TryFind(set, 6);
        }

        private static void TryUnion(Set set, int i, int j, int k)
        {
            Console.WriteLine($"Union({i},{j},{k}):");
            try
            {
                set.Union(i, j, k);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception}!");
                return;
            }

            Console.WriteLine(set.ToString());
        }

        private static void TryFind(Set set, int i)
        {
            int name;

            Console.WriteLine($"Find({i}):");
            try
            {
                name = set.Find(i);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception}!");
                return;
            }

            Console.WriteLine($"Found in: {name}");
        }

        private static void TestKruskal()
        {
            var graphFile = "Data/Graph1.json";
            var mst = new MinimumSpanningTree(graphFile);
            //var mst = new MinimumSpanningTree(RandomizeCostMatrix(5));

            Console.WriteLine(mst.ToString());

            var mstEdges = mst.KruskalAlgorithm();
            ShowMstPath(mstEdges);
        }

        private static void ShowMstPath(Stack<Edge> mstEdges)
        {
            string resultString = "Vertice1 | Vertice2 | Cost\n";

            while (mstEdges.Any())
            {
                var edge = mstEdges.Pop();
                resultString += $"{edge.Vertice1,8} | {edge.Vertice2,8} | {edge.Cost,4}\n";
            }

            Console.WriteLine(resultString);
        }

        private static int[,] RandomizeCostMatrix(int size)
        {
            var costMatrix = new int[size, size];

            Random rand = new Random(36578);

            for (int vertice = 0; vertice < size; vertice++)
            {
                for (int neighbour = 0; neighbour < size; neighbour++)
                {
                    if (vertice != neighbour)
                    {
                        costMatrix[vertice, neighbour] = rand.Next(1, 100);
                        costMatrix[neighbour, vertice] = costMatrix[vertice, neighbour];
                    }
                }
            }

            return costMatrix;
        }
    }
}
