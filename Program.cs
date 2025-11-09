using System;
using System.Globalization;
using System.Threading;
using Geometry;
using System.Linq;

class Program
{
    static void Main()
    {
        // Встановлюємо культуру для коректного виводу рівнянь
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        IShape[] shapes =
        {
            new Line(2, -4, 8),
            new HyperPlane(1, 1, 1, 1, -4) 
        };

        Console.WriteLine("--- Equations ---");
        foreach (var shape in shapes)
        {
            Console.WriteLine($"Shape: {(shape as ILoggable)?.GetLogDescription() ?? "Unknown Shape"}");
            Console.WriteLine($"  Equation: {shape.PrintEquation()}");
            
            // Демонстрація Normalize()
            if (shape is GeometricEquation ge)
            {
                var normalized = ge.Normalize();
                Console.WriteLine($"  Normalized: {normalized.PrintEquation()}");
            }
        }

        Console.WriteLine();

        // ... Логіка ReadCoordinates залишається без змін ...
        foreach (var shape in shapes)
        {
            int dimension = shape.Dimension; 
            Console.WriteLine($"\n--- Check Coordinates for: {shape.PrintEquation()} ---");

            double[] coords = ReadCoordinates(dimension);

            bool belongs = shape.BelongsToShape(coords);
            Console.WriteLine($" -> belongs: {belongs}");
        }
    }

    static double[] ReadCoordinates(int count)
    {
        // ... (метод без змін) ...
        // (Оригінальний метод ReadCoordinates з Program.cs)
        string exampleInput = string.Join(" ", Enumerable.Range(1, count).Select(i => i % 2 == 0 ? $"{i}.5" : $"{i}"));
        
        while (true)
        {
            Console.WriteLine($"Enter {count} numbers separated by spaces (e.g., {exampleInput}):");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string[] parts = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

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
                    Console.WriteLine("Incorrect number format. Ensure numbers are separated by spaces or commas, and use a dot '.' as decimal separator.");
                    ok = false;
                    break;
                }
            }

            if (ok) return result;
        }
    }
}