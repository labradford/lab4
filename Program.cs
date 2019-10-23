using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Statistics();
            Coordinates();
            SumOfSquares();

            Console.WriteLine();
            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        static void Statistics()
        {
            int[] values = { 1, 6, 4, 7, 9, 2, 5, 7, 2, 6, 5, 7, 8, 1, 2, 8 };
            //int[] values = { 1, 6 , 4, 1, 6, 3, 3, 3, 5, 6 };
            //int[] values = { 26 };
            //int[] values = { 44, 88};
            //int[] values = new int[0];

            double mean = Mean(values);
            double median = Median(values);
            List<int> mode = Mode(values);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Data: " + string.Join(", ", values));
            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Median: {median}");
            Console.WriteLine(string.Join(", ", mode));
            Console.WriteLine();

        }

        static double Mean(params int[] values)
        {
            if (values == null || values.Length == 0) return double.NaN;
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
            if (values == null || values.Length == 0) return double.NaN;
            // if array has only one value, return the value
            if (values.Length == 1)
            {
                median = values[0];
            }
            else
            {
                int[] sorted = new int[values.Length];
                values.CopyTo(sorted, 0);
                Array.Sort(sorted);
                double middleValue = (sorted.Length / 2);

                if (sorted.Length % 2 == 0)
                {
                    int param1 = (int)middleValue;
                    int param2 = (int)middleValue - 1;
                    median = Mean(sorted[param1], sorted[param2]);
                }
                else
                {
                    median = sorted[(int)middleValue];
                }
            }

            return median;

        }

        static List<int> Mode(params int[] values)
        {            
            int[] sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);

            List<int> result = new List<int>();
            var counts = new Dictionary<int, int>();
            int max = 0;
            foreach (int key in sorted)
            {
                int count = 1;
                if (counts.ContainsKey(key))
                {
                    count += counts[key];
                    if (count > max)
                    {
                        max = count;
                    }
                }
                counts[key] = count;
            }

            foreach (var count in counts)
            {
                
                if (count.Value == max)
                {
                    result.Add(count.Key);
                }
            }
            return result;
        }

        static void Coordinates()
        {
            Console.Write("Enter a coordinate in the form (x, y): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string input = Console.ReadLine();
            double x, y;
            if (TryParsePoint(input, out x, out y))
            {
                var polar = RectangularToPolar(x, y);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"r: {polar.Item1}, angle: {polar.Item2} radians");
                Console.WriteLine($"r: {polar.Item1}, angle: {RadianToDegrees(polar.Item2)}°");
                Console.WriteLine();
            }
        }


        static bool TryParsePoint(string input, out double x, out double y)
        {
            x = 0;
            y = 0;
            //input.Trim();
            if (!input.StartsWith("(") && !input.EndsWith(")") && !input.Contains(","))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("doesn't start / end with () or containd a comma");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else
            {
                char[] charsToTrim = { '(', ')' };
                string trimmedInput = input.Trim(charsToTrim);

                string[] pointsArray = new string[2];
                pointsArray = trimmedInput.Split(",");

                if (pointsArray.Length != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Array length is not 2");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                else
                {
                    if (!double.TryParse(pointsArray[0], out x))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("X doesn't parse");
                        Console.ForegroundColor = ConsoleColor.White;

                        return false;
                    }

                    if (!double.TryParse(pointsArray[1], out y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("y doesn't parse");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
                return true;
            }

        }

        static Tuple<double, double> RectangularToPolar(double x, double y)
        {
            double r = 0;
            double angle = 0;
            if (x == 0 && y == 0)
            {
                r = 0;
                angle = 0;
            }
            else if (x > 0 && y == 0)
            {
                r = x;
                angle = 0;
            }
            else if (x == 0 && y > 0)
            {
                r = y;
                angle = Math.PI / 2;

            }
            else if (x < 0 && y == 0)
            {
                r = Math.Abs(x);
                angle = Math.PI;
            }
            else if (x == 0 && y < 0)
            {
                r = Math.Abs(y);
                angle = (Math.PI * 3) / 2;
            }
            else if (x > 0 && y > 0)
            {
                r = Math.Sqrt(x * x + y * y);
                angle = Math.Atan(y / x);
            }
            else if (x > 0 && y < 0)
            {
                r = Math.Sqrt(x * x + y * y);
                angle = Math.PI * 2 + Math.Atan(y / x);
            }
            else
            {
                r = Math.Sqrt(x * x + y * y);
                angle = Math.PI + Math.Atan(y / x);
            }
            return new Tuple<double, double>(r, angle);
        }

        static double RadianToDegrees(double radians)
        {
            double degrees = radians * (180 / Math.PI);
            while (degrees < 0)
            {
                degrees += 360;
            }

            while (degrees > 360)
            {
                degrees -= 360;
            }

            return degrees;
        }

        static void SumOfSquares()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter a positive integer: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int sum))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That is not an an integer");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (sum < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That is not a positive integer.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine($"The sum of squares for {input} is {Squares(sum)}");
            }
        }

        static long Squares(long value)
        {
            if (value > 1)
            {
                return value * value + Squares(value - 1);
            }
            return value;
        }
    }
}
