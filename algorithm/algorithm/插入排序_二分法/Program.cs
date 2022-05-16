using System;
using Utility;

namespace 插入排序_二分法
{
    class Program
    {
        static void Main(string[] args)
        {
            F();
        }
        static int[] arr = RandomArrar.OneArrar(5);
        public static void F() {
            for (int i = 1; i < arr.Length; i++)
            {
                int target = WeiZhi(i);
                ChaRu(i,target);
            }
        }

        //使用二分法 找到要插入的位置
        public static int WeiZhi(int index) {
            int begin = 0;
            int end = index;
            while (begin<end)
            {
                int median = end / 2;
                if (arr[median]<arr[index])
                {
                    begin = median+1;
                }
                else
                {
                    end = median;
                }
            }

            return begin;
            
        }

        /// <summary>
        /// 将数据插入到指定位置 将start索引的值 移动到 index索引
        /// </summary>
        /// <param name="start">要移动的数据索引</param>
        /// <param name="index">目标索引</param>
        public static void ChaRu(int start,int index) { // start>index
            int num = arr[start];

            for (int i = start; i <= index; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[index] = num;
        }
    }
}
