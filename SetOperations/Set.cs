using System.Collections.Generic;

namespace SetOperations
{
    public class Set
    {
        private int[] _elements;
        private int[] _count;
        private int[] _name;
        private int[] _father;
        private int[] _root;

        public Set(IReadOnlyList<int> dataSet)
        {
            int size = dataSet.Count;
            _elements = new int[size];
            _count = new int[size];
            _name = new int[size];
            _father = new int[size];
            _root = new int[size];

            for (int i = 0; i < size; i++)
            {
                _elements[i] = dataSet[i];
                _count[i] = 1;
                _name[i] = i;
                _father[i] = -1;
                _root[i] = i;
            }
        }

        public void Union(int i, int j, int k)
        {
            if (_count[_root[i]] > _count[_root[j]])
            {
                int temp = i;
                i = j;
                j = temp;
            }

            int small = _root[i];
            int large = _root[j];

            _father[small] = large;
            _count[large] = _count[large] + _count[small];
            _name[large] = k;
            _root[k] = large;
        }

        public int Find(int i)
        {
            var list = new List<int>();
            int v = i;

            while (_father[v] != -1)
            {
                list.Add(v);
                v = _father[v];
            }

            foreach (var w in list)
            {
                _father[w] = v;
            }

            return _name[v];
        }

        public override string ToString()
        {
            string resultString = "Element | Name | Root | Father | Count\n";

            for (int i = 0; i < _elements.Length; i++)
            {
                resultString += $"{_elements[i],7} | {_name[i],4} | {_root[i],4} | {_father[i],6} | {_count[i],5}\n";
            }

            return resultString;
        }
    }
}
