using System;

namespace 二維數組的遍歷
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[2,3];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = j;
                }
            }

            foreach (var item in array)
            {
                Console.WriteLine(item+"\t");
            }
        }
    }
}
