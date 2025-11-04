using System;
using System.Globalization;
using Geometry;

class Program
{
    static void Main()
    {
        IShape[] shapes =
        {
            new Line(2, -4, 8),
            new HyperPlane(1, 1, 1, 1, -4)
        };

        Console.WriteLine("Equations:");
        foreach (var shape in shapes)
            Console.WriteLine(shape.PrintEquation());

        Console.WriteLine();

        foreach (var shape in shapes)
        {
            int dimension = shape is Line ? 2 : 4;
            Console.WriteLine($"\nEnter {dimension} coordinates for: {shape.PrintEquation()}");

            double[] coords = ReadCoordinates(dimension);

            bool belongs = shape.BelongsToShape(coords);
            Console.WriteLine($" -> belongs: {belongs}");
        }
    }

    static double[] ReadCoordinates(int count)
    {
        while (true)
        {
            Console.WriteLine($"Enter {count} numbers separated by spaces:");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != count)
            {
                Console.WriteLine("Incorrect number of values. Try again.");
                continue;
            }

            double[] result = new double[count];
            bool ok = true;

            for (int i = 0; i < count; i++)
            {
                if (!double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out result[i]))
                {
                    Console.WriteLine("Incorrect number format. Use dot as decimal separator.");
                    ok = false;
                    break;
                }
            }

            if (ok) return result;
        }
    }
}

