using System;
using Geometry;

class Program
{
    static void Main()
    {
        var line = new Line(1, -2, 3);
        Console.WriteLine(line.PrintEquation());

        var plane = new HyperPlane(1, 2, 3, -4); // 3D площина
        Console.WriteLine(plane.PrintEquation());
    }
}
