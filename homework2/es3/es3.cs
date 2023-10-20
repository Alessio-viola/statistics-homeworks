using System;
using System.Collections.Generic;
 
class Program
{
    static void Main()
    {
        int N = 1000; // Number of random variates
        int k = 10; // Number of intervals
 
        Random random = new Random();
        List<int> frequency = new List<int>(new int[k]);
 
        for (int i = 0; i < N; i++)
        {
            double value = random.NextDouble(); // Generate a random number in [0, 1)
            int interval = (int)(value * k);
            frequency[interval]++;
        }
 
        for (int i = 0; i < k; i++)
        {
            double lowerBound = i / (double)k;
            double upperBound = (i + 1) / (double)k;
            Console.WriteLine($"Interval [{lowerBound:F2}, {upperBound:F2}): {frequency[i]}");
        }
    }
}