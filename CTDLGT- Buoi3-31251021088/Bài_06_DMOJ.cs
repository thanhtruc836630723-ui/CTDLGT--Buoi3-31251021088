using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT__Buoi3_31251021088
{
    internal class Bài_06_DMOJ
    {
        public static void Main(string[] args)
        {
            string line1 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line1)) return;
            int n = int.Parse(line1.Trim());

 
            string[] line2 = Console.ReadLine().Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            long[] h = new long[n];
            for (int i = 0; i < n; i++)
            {
                h[i] = long.Parse(line2[i]);
            }
            Stack<int> stack = new Stack<int>();
            long maxArea = 0;

            for (int i = 0; i <= n; i++)
            {
                long currentHeight = (i == n) ? 0 : h[i];
                while (stack.Count > 0 && currentHeight < h[stack.Peek()])
                {
                    int topIndex = stack.Pop();
                    long height = h[topIndex];
                    long width = (stack.Count == 0) ? i : (i - stack.Peek() - 1);

                    long area = height * width;
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }

                stack.Push(i);
            }
            Console.WriteLine(maxArea);
        }
    }

}




   

