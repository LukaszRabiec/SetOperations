using System;

namespace SetOperations.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new Set(new[] { 1, 3, 5, 7, 2, 4, 8, 6 });

            Console.WriteLine(set.ToString());

            TryUnion(set, 1, 2, 1);
            TryUnion(set, 3, 4, 3);
            TryUnion(set, 1, 3, 1);


            Console.ReadKey();
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
    }
}
