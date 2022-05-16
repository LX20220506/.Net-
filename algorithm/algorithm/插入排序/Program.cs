using System;
using System.Diagnostics;
using Utility;

namespace 插入排序
{
    class Program
    {
        static void Main(string[] args)
        {
            F(RandomArrar.OneArrar(20));
        }

        public static void F(int[] arr) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = i; j > 0 ; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        int num;
                        num = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = num;
                    }
                    else
                        break;
                }
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());

            Console.WriteLine();
            foreach (var item in arr)
            {
                Console.Write(item+",");
            }

        }
    }
}
