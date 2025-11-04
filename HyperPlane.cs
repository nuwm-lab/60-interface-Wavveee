using System;
using System.Linq;

namespace Geometry
{
    /// <summary>
    /// Представляє гіперплощину у 4D (лише лінійна форма із 5 коефіцієнтами: a*x1 + b*x2 + c*x3 + d*x4 + e = 0).
    /// Клас не виконує виведення у консоль — метод PrintEquation повертає рядок (тестованість).
    /// </summary>
    public class HyperPlane : GeometricEquation
    {
        private readonly double[] _coefficients; // очікується довжина 5: {a, b, c, d, e}

        /// <summary>
        /// Ітераційна константа розміру коефіцієнтів для цієї реалізації.
        /// </summary>
        public const int ExpectedLength = 5;

        /// <summary>
        /// Доступ до коефіцієнтів (копія для безпеки).
        /// Порядок: [0]=A, [1]=B, [2]=C, [3]=D, [4]=E (вільний член).
        /// </summary>
        public double[] Coefficients => (double[])_coefficients.Clone();

        /// <summary>
        /// Конструктор: приймає 5 коефіцієнтів явно.
        /// </summary>
        public HyperPlane(double a, double b, double c, double d, double e)
            : base(a, b, c)
        {
            _coefficients = new[] { a, b, c, d, e };
            ValidateCoefficientsOrThrow(_coefficients);
        }

        /// <summary>
        /// Конструктор: приймає масив коефіцієнтів.
        /// </summary>
        /// <param name="coefficients">Має бути довжини 5: a,b,c,d,e</param>
        public HyperPlane(double[] coefficients)
            : base(
                  coefficients != null && coefficients.Length >= 3 ? coefficients[0] : 0.0,
                  coefficients != null && coefficients.Length >= 3 ? coefficients[1] : 0.0,
                  coefficients != null && coefficients.Length >= 3 ? coefficients[2] : 0.0)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));

            if (coefficients.Length != ExpectedLength)
                throw new ArgumentException($"Масив коефіцієнтів має містити рівно {ExpectedLength} значень (a,b,c,d,e).", nameof(coefficients));

            _coefficients = (double[])coefficients.Clone();
            ValidateCoefficientsOrThrow(_coefficients);
        }

        /// <summary>
        /// Перевіряє, що не всі коефіцієнти одночасно нульові.
        /// Викидає ArgumentException з інформативним повідомленням при помилці.
        /// </summary>
        private static void ValidateCoefficientsOrThrow(double[] coeffs)
        {
            if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
            if (coeffs.Length != ExpectedLength)
                throw new ArgumentException($"Очікується масив довжини {ExpectedLength}.", nameof(coeffs));

            bool allZero = coeffs.All(v => Math.Abs(v) < GeometricEquation.Epsilon);
            if (allZero)
                throw new ArgumentException("Усі коефіцієнти не можуть бути нульовими одночасно.");
        }

        /// <summary>
        /// Формує добре відформатований член (наприклад " + 2*x1", " - 3*x2" або для константи " + 5").
        /// Виключає випадок нульового коефіцієнта (повертає пустий рядок).
        /// </summary>
        private static string FormatTerm(double coefficient, string variable, bool isFirstNonZero)
        {
            if (Math.Abs(coefficient) < GeometricEquation.Epsilon)
                return string.Empty;

            string sign;
            if (isFirstNonZero)
            {
                // Перший видимий член — знак "-" можливий, але не ставимо "+" перед позитивним
                sign = coefficient < 0 ? "-" : string.Empty;
            }
            else
            {
                sign = coefficient < 0 ? " - " : " + ";
            }

            double absValue = Math.Abs(coefficient);

            if (string.IsNullOrWhiteSpace(variable))
            {
                // константа (вільний член)
                return $"{sign}{absValue}";
            }
            else
            {
                return $"{sign}{absValue}*{variable}";
            }
        }

        /// <summary>
        /// Повертає відформатоване рядкове представлення рівняння.
        /// Наприклад: "HyperPlane (4D): x1 + 2*x2 - 3*x3 + 4*x4 - 5 = 0"
        /// </summary>
        public override string PrintEquation()
        {
            // Порядок імена змінних: x1..x4, останній коефіцієнт — вільний член
            string[] variables = { "x1", "x2", "x3", "x4", "" };

            // Знайдемо перший ненульовий член, щоб не ставити провідний "+"
            int firstIndex = -1;
            for (int i = 0; i < _coefficients.Length; i++)
            {
                if (Math.Abs(_coefficients[i]) >= GeometricEquation.Epsilon)
                {
                    firstIndex = i;
                    break;
                }
            }

            // Якщо усі коефіцієнти нульові (повинно було заблоковано в конструкторі) — повертаємо "0 = 0"
            if (firstIndex == -1)
                return "HyperPlane (4D): 0 = 0";

            var parts = new System.Text.StringBuilder();
            parts.Append("HyperPlane (4D): ");

            bool firstAdded = false;
            for (int i = 0; i < _coefficients.Length; i++)
            {
                double coeff = _coefficients[i];
                bool isFirstNonZero = !firstAdded && Math.Abs(coeff) >= GeometricEquation.Epsilon;
                string term = FormatTerm(coeff, variables[i], isFirstNonZero);
                if (!string.IsNullOrEmpty(term))
                {
                    parts.Append(term);
                    firstAdded = true;
                }
            }

            parts.Append(" = 0");
            return parts.ToString();
        }

        /// <summary>
        /// Перевіряє належність точки до гіперплощини.
        /// Очікує 4 координати (x1..x4). Викликає исключення при некоректній кількості координат.
        /// </summary>
        /// <param name="coordinates">x1, x2, x3, x4</param>
        /// <returns>true, якщо |a*x1 + b*x2 + c*x3 + d*x4 + e| &lt; Epsilon</returns>
        public override bool BelongsToShape(params double[] coordinates)
        {
            if (coordinates == null)
                throw new ArgumentNullException(nameof(coordinates));

            if (coordinates.Length != ExpectedLength - 1) // 4 координати для 5 коефіцієнтів
                throw new ArgumentException($"Для HyperPlane потрібно рівно {ExpectedLength - 1} координат (x1..x4).", nameof(coordinates));

            // Обчислюємо лінійну комбінацію
            double sum = 0.0;
            for (int i = 0; i < coordinates.Length; i++)
            {
                sum += _coefficients[i] * coordinates[i];
            }

            // додаємо вільний член (останній коефіцієнт)
            sum += _coefficients[ExpectedLength - 1];

            return Math.Abs(sum) < GeometricEquation.Epsilon;
        }

        // За потреби можна додати додаткові API: Translate, Scale, ToNormalizedForm тощо.
    }
}
