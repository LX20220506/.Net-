using System;

namespace Utility
{
    public static class RandomArrar
    {
        public static int[] OneArrar(int length) {
            int[] arr = new int[length];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Random().Next(100);
            }
            return arr;
        }

        public static int[,] TowArrar(int length1,int length2) {
            int[,] arr = new int[length1, length2];

            for (int i = 0; i < length1; i++)
            {
                for (int j = 0; j < length2; j++)
                {
                    arr[i, j] = new Random().Next(0, 100);
                }
            }

            return arr;
        }
    }
}
