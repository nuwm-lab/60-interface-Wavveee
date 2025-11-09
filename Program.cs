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
            // Тепер використовуємо універсальну HyperPlane
            new HyperPlane(1, 1, 1, 1, -4) // 4D: x1 + x2 + x3 + x4 - 4 = 0
        };

        Console.WriteLine("Equations:");
        foreach (var shape in shapes)
            Console.WriteLine(shape.PrintEquation());

        Console.WriteLine();

        foreach (var shape in shapes)
        {
            // Використовуємо властивість Dimension з інтерфейсу IShape
            int dimension = shape.Dimension; 
            Console.WriteLine($"\nEnter {dimension} coordinates for: {shape.PrintEquation()}");

            double[] coords = ReadCoordinates(dimension);

            bool belongs = shape.BelongsToShape(coords);
            Console.WriteLine($" -> belongs: {belongs}");
        }
    }

    /// <summary>
    /// Зчитує необхідну кількість координат від користувача.
    /// </summary>
    /// <param name="count">Кількість координат для зчитування.</param>
    /// <returns>Масив координат типу double.</returns>
    static double[] ReadCoordinates(int count)
    {
        // Створення прикладу введення для підказки
        string exampleInput = string.Join(" ", Enumerable.Range(1, count).Select(i => i % 2 == 0 ? $"{i}.5" : $"{i}"));
        
        while (true)
        {
            // Покращена підказка з прикладом вводу
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
                // Використовуємо InvariantCulture для забезпечення зчитування дробової частини через крапку (1.23)
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