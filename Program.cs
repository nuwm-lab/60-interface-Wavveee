using System;

// Абстрактний клас
public abstract class GeometricEquation
{
    // Загальні коефіцієнти, які мають усі об'єкти (мінімум 3 для 2D або більше)
    protected readonly float coefficientA;
    protected readonly float coefficientB;
    protected readonly float coefficientC;

    // Похибка для порівняння чисел з плаваючою комою
    protected const float Epsilon = 1e-6f;

    // Конструктор абстрактного класу. Викликається лише конструкторами класів-нащадків.
    public GeometricEquation(float a, float b, float c)
    {
        // Базова валідація для рівняння
        if (a == 0 && b == 0 && c == 0)
            throw new ArgumentException("Усі три базові коефіцієнти A, B і C не можуть бути нульовими одночасно.");

        coefficientA = a;
        coefficientB = b;
        coefficientC = c;

        Console.WriteLine($"[INFO] Створено базовий об'єкт GeometricEquation з A={a}, B={b}, C={c}.");
    }
    ~GeometricEquation()
    {
        // демонстраційний деструктор
    }

    // ----------------------
    // АБСТРАКТНІ МЕТОДИ
    // Клас-нащадок ПОВИНЕН його реалізувати.
    // ----------------------

    /// Абстрактний метод для виведення рівняння у правильному форматі.
    public abstract void PrintEquation();

    /// Абстрактний метод для перевірки належності точки.
    public abstract bool BelongsToShape(params float[] coordinates);
}

public class Line : GeometricEquation
{
    public Line(float a, float b, float c) : base(a, b, c)
    {
        if (a == 0 && b == 0)
            throw new ArgumentException("Для 2D лінії коефіцієнти A і B не можуть бути одночасно нульовими.");

        Console.WriteLine($"[INFO] Створено об'єкт Line (2D).");
    }

    ~Line()
    {
        Console.WriteLine($"[INFO] Деструктор Line (2D) спрацював.");
    }

    public override void PrintEquation()
    {
        Console.WriteLine($"Line (2D): {coefficientA}*x + {coefficientB}*y + {coefficientC} = 0");
    }

    public override bool BelongsToShape(params float[] coordinates)
    {
        if (coordinates.Length != 2)
        {
            Console.WriteLine("Помилка: 2D лінія потребує рівно 2 координати (x, y).");
            return false;
        }

        float x = coordinates[0];
        float y = coordinates[1];
        
        // Перевірка рівняння: Ax + By + C = 0
        return Math.Abs(coefficientA * x + coefficientB * y + coefficientC) < Epsilon;
    }
}

public class HyperPlane : GeometricEquation
{
    private readonly float coefficientD;
    private readonly float coefficientE;

    public HyperPlane(float a, float b, float c, float d, float e) 
        : base(a, b, c) // Викликаємо конструктор GeometricEquation
    {
        // Валідація для 4D
        if (a == 0 && b == 0 && c == 0 && d == 0)
             throw new ArgumentException("Принаймні один з коефіцієнтів A, B, C або D має бути ненульовим.");

        coefficientD = d;
        coefficientE = e;
        Console.WriteLine($"[INFO] Створено об'єкт HyperPlane (4D) з D={d}, E={e}.");
    }
    
    ~HyperPlane()
    {
        Console.WriteLine($"[INFO] Деструктор HyperPlane (4D) спрацював.");
    }

    public override void PrintEquation()
    {
        Console.WriteLine($"HyperPlane (4D): {coefficientA}*x1 + {coefficientB}*x2 + {coefficientC}*x3 + {coefficientD}*x4 + {coefficientE} = 0");
    }

    public override bool BelongsToShape(params float[] coordinates)
    {
        if (coordinates.Length != 4)
        {
            Console.WriteLine("Помилка: 4D гіперплощина потребує рівно 4 координати (x1, x2, x3, x4).");
            return false;
        }

        float x1 = coordinates[0];
        float x2 = coordinates[1];
        float x3 = coordinates[2];
        float x4 = coordinates[3];

        // Перевірка рівняння: Ax1 + Bx2 + Cx3 + Dx4 + E = 0
        float sum = coefficientA * x1 + coefficientB * x2 + coefficientC * x3 + coefficientD * x4 + coefficientE;
        
        return Math.Abs(sum) < Epsilon;
    }
}

class Program
{
    static void Main()
    {
        // 1. Демонстрація класу Line (2D)
        Console.WriteLine("\n[1] Створення 2D Лінії: 2x - 4y + 8 = 0");
        // Динамічно створюємо Line, але посилаємося на нього як на GeometricEquation
        GeometricEquation line = new Line(a: 2, b: -4, c: 8);
        line.PrintEquation(); 
        
        // Поліморфний виклик методу BelongsToShape
        Console.WriteLine($"-> Точка (2, 3) належить: {line.BelongsToShape(2, 3)} ");
        Console.WriteLine($"-> Точка (1, 1) належить: {line.BelongsToShape(1, 1)} ");
        
        // 2. Демонстрація класу HyperPlane (4D)
        Console.WriteLine("\n[2] Створення 4D Гіперплощини: x1 + x2 + x3 + x4 - 4 = 0");
        // Динамічно створюємо HyperPlane, але посилаємося на нього як на GeometricEquation
        GeometricEquation hyperplane = new HyperPlane(a: 1, b: 1, c: 1, d: 1, e: -4);
        hyperplane.PrintEquation(); 
        
        // Поліморфний виклик методу BelongsToShape
        Console.WriteLine($"-> Точка (1, 1, 1, 1) належить: {hyperplane.BelongsToShape(1, 1, 1, 1)} "); 
        Console.WriteLine($"-> Точка (1, 2, 3, 4) належить: {hyperplane.BelongsToShape(1, 2, 3, 4)} ");
    }
}
