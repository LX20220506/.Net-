using System;
using System.Diagnostics;
using Utility;

namespace 选择排序
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = RandomArrar.OneArrar(20);
            foreach (var item in arr)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine();
            F(arr);
       
            
        }

        public static void F(int[] arr) {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < arr.Length-1; i++)
            {
                int min=i;//最小数据值得索引
                for (int j = i+1; j < arr.Length; j++)
                {
                    if (arr[min]>arr[j])
                    {
                        min = j;
                    }
                }
                if (arr[i]>arr[min])
                {
                    int num;
                    num = arr[i];
                    arr[i] = arr[min];
                    arr[min] = num;
                }
            }
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed.ToString());

            foreach (var item in arr)
            {
                Console.Write(item+",");
            }

        }
    }
}
