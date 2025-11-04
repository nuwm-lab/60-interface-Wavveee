using System;
using Geometry;

class Program
{
    static void Main()
    {
        IShape[] shapes =
        {
            new Line(2, -4, 8),
            new HyperPlane(1, 1, 1, 1, -4) // x1 + x2 + x3 + x4 - 4 = 0
        };

        Console.WriteLine("Equations:");
        foreach (var shape in shapes)
            Console.WriteLine(shape.PrintEquation());

        Console.WriteLine("\nEnter coordinates separated by spaces:");
        string input = Console.ReadLine();
        double[] coords = Array.ConvertAll(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), double.Parse);

        Console.WriteLine();
        foreach (var shape in shapes)
        {
            bool belongs = shape.BelongsToShape(coords);
            Console.WriteLine($"{shape.PrintEquation()} -> belongs: {belongs}");
        }
    }
}
