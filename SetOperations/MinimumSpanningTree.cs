namespace SetOperations
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    public class MinimumSpanningTree
    {
        private List<Edge> _sortedEdges;
        private Set _sets;
        private List<int> _actualSetsIndexes;
        private int[,] _costMatrix;

        public MinimumSpanningTree(string jsonFilePath)
        {
            _costMatrix = GetCostMatrixFromJsonFile(jsonFilePath);
            InitializeMst();
        }

        public MinimumSpanningTree(int[,] costMatrix)
        {
            _costMatrix = costMatrix;
            InitializeMst();
        }

        public Stack<Edge> KruskalAlgorithm()
        {
            var result = new Stack<Edge>();

            foreach (Edge edge in _sortedEdges)
            {
                int vertice1 = edge.Vertice1;
                int vertice2 = edge.Vertice2;

                if (_actualSetsIndexes.Contains(vertice1) && _actualSetsIndexes.Contains(vertice2))
                {
                    if (!_sets.IsSameRoot(vertice1, vertice2))
                    {
                        int set1 = _sets.Find(vertice1);
                        int set2 = _sets.Find(vertice2);

                        _sets.Union(set1, set2, set1);
                        result.Push(new Edge(vertice1, vertice2, edge.Cost));

                        if (_sets.MaxCount() == _costMatrix.GetLength(0))
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            string resultString = "";
            StringBuilder stringBuilder = new StringBuilder();

            for (int vertice = 0; vertice < _costMatrix.GetLength(0); vertice++)
            {
                stringBuilder.Clear();
                for (int neighbour = 0; neighbour < _costMatrix.GetLength(1); neighbour++)
                {
                    stringBuilder.Append(_costMatrix[vertice, neighbour] + "\t");
                }
                resultString += stringBuilder + "\n";
            }

            return resultString;
        }

        private void InitializeMst()
        {
            var size = _costMatrix.GetLength(0);
            var edges = InitializeEdgesFromCostMatrix();
            _sortedEdges = SortEdgesByCost(edges);
            _sets = new Set(size);

            InitializeActualSets(size);
        }

        private List<Edge> InitializeEdgesFromCostMatrix()
        {
            var edges = new List<Edge>();

            for (int vertice = 0; vertice < _costMatrix.GetLength(0); vertice++)
            {
                for (int neighbour = vertice + 1; neighbour < _costMatrix.GetLength(1); neighbour++)
                {
                    if (_costMatrix[vertice, neighbour] != 0)
                    {
                        edges.Add(new Edge(vertice, neighbour, _costMatrix[vertice, neighbour]));
                        edges.Add(new Edge(neighbour, vertice, _costMatrix[neighbour, vertice]));
                    }
                }
            }

            return edges;
        }

        private int[,] GetCostMatrixFromJsonFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<int[,]>(streamReader.ReadToEnd());
            }
        }

        private List<Edge> SortEdgesByCost(List<Edge> edges)
        {
            return edges.OrderBy(e => e.Cost).ToList();
        }

        private void InitializeActualSets(int size)
        {
            _actualSetsIndexes = new List<int>();

            for (int i = 0; i < size; i++)
            {
                _actualSetsIndexes.Add(i);
            }
        }
    }
}
