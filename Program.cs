using System;

class Program
{
    static void Main()
    {
        IShape line = new Line(2, -4, 8);
        Console.WriteLine(line.PrintEquation());
        Console.WriteLine($"Belongs (2,3): {line.BelongsToShape(2, 3)}");
        Console.WriteLine($"Belongs (1,1): {line.BelongsToShape(1, 1)}");

        Console.WriteLine();

        IShape hyperplane = new HyperPlane(1, 1, 1, 1, -4);
        Console.WriteLine(hyperplane.PrintEquation());
        Console.WriteLine($"Belongs (1,1,1,1): {hyperplane.BelongsToShape(1, 1, 1, 1)}");
        Console.WriteLine($"Belongs (1,2,3,4): {hyperplane.BelongsToShape(1, 2, 3, 4)}");
    }
}
