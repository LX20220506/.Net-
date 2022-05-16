using System;
using System.Diagnostics;

namespace 冒泡排序
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] {1,8,62,56,7,54,12,35,9896,156,395,951,345,972,982,411,35 };


            Stopwatch swatch = new Stopwatch();    //创建Stopwatch 实例
            swatch.Start();    //开始计时

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length-i-1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp;
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }

                }
            }

            swatch.Stop();     //结束计时
            Console.WriteLine(swatch.Elapsed.ToString());
            foreach (var item in arr)
            {
                Console.Write(item+",");
            }
        }
    }
}
