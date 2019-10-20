using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Statistics();

            Console.WriteLine();
            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        static void Statistics()
        {
            int[] values = { 1, 6, 4, 7, 9, 2, 5, 7, 2, 6, 5, 7, 8, 1, 2, 8 };
            //int[] values = { 3, 3, 5 };
            //int[] values = { 26 };
            //int[] values = new int[1];

           double mean = Mean(values);
           double median = Median(values);
            // Mode(values);

            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Median: {median}");

        }

        static double Mean(params int[] values)
        {
            if (values == null || values.Length == 0) throw new ArgumentNullException();
            double mean;
            double sum = 0;

            for (int i = 0; i < values.Length; ++i)
            {
                sum += values[i];
            }

            mean = sum / values.Length;
            return mean;
        }

        static double Median(params int[] values)
        {
            double median; 
            if (values == null || values.Length == 0) throw new ArgumentNullException();
            //Console.WriteLine($"VALUES.LENGTH:  {values.Length}");
            // if array has only one value, return the value
            if (values.Length == 1)
            {
                median = values[0];
                //Console.WriteLine($"VALUES OF ONE LENGTH: {median}");
            }
            else
            {
                int[] sorted = new int[values.Length];
                values.CopyTo(sorted, 0);
                Array.Sort(sorted);
                double middleValue = (sorted.Length / 2);
                //Console.WriteLine($"MIDDLEVALUE: {middleValue}");

                if (sorted.Length % 2 == 0)
                {
                    int param1 = (int)middleValue;
                    int param2 = (int)middleValue - 1;
                    //Console.WriteLine($"param1: {param1}  param2: {param2}");
                    median = Mean(sorted[param1], sorted[param2]);
                    //Console.WriteLine($"MEDIAN EVEN: {median}");
                }
                else
                {
                    median = sorted[(int)middleValue];
                    //Console.WriteLine($"MEDIAN ODD: {median}");
                }
            }

            return median;
            
            


        }

        //static double Mode(params int[] values)
        //{

        //}
    }
}
