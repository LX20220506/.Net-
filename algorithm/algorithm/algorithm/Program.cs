using System;

namespace algorithm
{
    class Program
    {
        static void Main()
        {
            foreach (int number in SomeNumbers())
            {
                Console.Write(number.ToString() + " ");
            }
            // Output: 3 5 8
            Console.ReadKey();
        }
        public static System.Collections.IEnumerable SomeNumbers()
        {
            yield return 3;
            yield return 5;
            yield return 8;
        }


    }
}
