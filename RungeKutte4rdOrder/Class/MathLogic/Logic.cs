using System;
using System.Collections.Generic;

namespace RungeKutte4rdOrder.Class.MathLogic
{
    class Logic
    {
        private static string lastElement;
        private static string [] element;
        private static double n = 0;
        private static double h = 0;
        private static double division = -1;
        private static double x=0, y=0, z=0;
        private static double C0 = 0, C1 = 0, C2 = 0, C3 = 0;
        private static double J0=0,J1=0, J2 = 0,J3 = 0;
        internal void Solute(ref List<string> equation, out List<double> point)
        {
            string[] points = equation[0].Trim().Split(';');
            x = Convert.ToDouble(points[0]);
            y = Convert.ToDouble(points[1]);
            n= Convert.ToDouble(equation[1]);
            h = n/Convert.ToDouble(equation[2]);
            z = Convert.ToDouble(equation[3]);
            element = equation[4].Trim().Split(' ');
            equation.Clear();
            point = new List<double>();
            int j = 0;
            for (double i = h; i <n; i += h)
            {
                RungeKutte();
                x += h / 2;
                j++;
                equation.Add($"x{j} = {Math.Round(x,4)} y{j} = {Math.Round(y,4)} y'{j} = {Math.Round(z,4)} h{j} = {i}");
                point.Add(Math.Round(y, 4));
            }

        }
        private void RungeKutte()
        {
            C0 = h * z;
            J0 = h * f(x, y, z);
            C1 = h * (z + J0 / 2);
            J1 = h * f(x + h / 2, y + C0 / 2, z + J0 / 2);
            C2 = h * (z + J1 / 2);
            J2 = h * f(x + h / 2, y + C1 / 2, z + J1 / 2);
            C3 = h * (z + J2 / 2);
            J3 = h * f(x + h / 2, y + C2 / 2, z + J2 / 2);
            y = y + (C0 + 2 * C1 + 2 * C2 + C3) / 6;
            z = z + (J0 + 2 * J1 + 2 * J2 + J3) / 6;
        }
        private double f(in double x, in double y, in double z)
        {
            double integral = 0;
            for (int i = 0; i < element.Length; i++)
            {
                if (i != element.Length - 1)
                    integral += SoluteDivision(element[i], i, element[i + 1], ContainsDivision(element[i + 1]));
                else
                    integral += SoluteDivision(element[i], i, element[i], ContainsDivision(element[i]));
                lastElement = element[i];
            }
            return integral;
        }
        private double SoluteDivision(in string numbers, in int i, in string nextvalue, bool flag)
        {
            if (flag == true)
                return 0;
            if (i == division)
                return 0;
            if (numbers == "/")
            {
                division = i + 1;
                double firstNumber = CheckEquation(lastElement);
                double secondNumber = double.Parse(nextvalue);
                return firstNumber / secondNumber;
            }
            else
                return CheckEquation(numbers);
        }
        private double CheckEquation(in string numbers)
        {
            double answer = 0;
            answer += Trigonometry(numbers);
            answer += Pow(numbers);
            answer += Sqrt(numbers);
            answer += OnlyNumbers(numbers);
            answer += NumbersWithXYZ(numbers);

            return answer;
        }
        private bool ContainsDivision(string numbers)
        {
            return numbers == "/" ? true : false;
        }
        private double Trigonometry(in string numbers)
        {
            if (numbers.StartsWith("sin"))
            {
                string[] number = numbers.Split(new string[] { "sin(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                return Math.Sin(NumbersWithXYZ(number[0]));
            }
            if (numbers.StartsWith("cos"))
            {
                string[] number = numbers.Split(new string[] { "cos(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                return Math.Sin(NumbersWithXYZ(number[0]));
            }
            return 0;
        }
        private double MultipleNegativeOrNot(double value)
        {
            return lastElement == "-" ? value * -1: value;
        }
        private double Pow(string numbers)
        {
            string[] number = numbers.Trim().Split('^');
            if(number.Length>1)
                return Math.Pow(NumbersWithXYZ(number[0]), double.Parse(number[1]));
            return 0;
        }
        private double OnlyNumbers(string numbers)
        {
            if (double.TryParse(numbers, out double number))
                return number;
            else
                return 0;
        }
        private double NumbersWithXYZ(string numbers)
        {
            if (numbers.EndsWith("x") || numbers.EndsWith("y") || numbers.EndsWith("z"))
            {
                if (numbers.Length > 1)
                {
                    string[] number = numbers.Split('x','y','z');
                    double.TryParse(number[0], out double numb);
                    return number[0] == "x" ? MultipleNegativeOrNot(numb * x) : number[0] == "y" ? MultipleNegativeOrNot(numb * y) : MultipleNegativeOrNot(numb * z);
                }
                else
                    return numbers == "x" ? MultipleNegativeOrNot(x) : numbers== "y" ? MultipleNegativeOrNot(y) : MultipleNegativeOrNot(z);
            }
            return 0;
        }
        private double Sqrt(string numbers)
        {
            if (numbers.StartsWith("sqrt"))
            {
                string[] number = numbers.Split(new string[] { "sqrt(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries);
                if (number[0]=="x" || number[0] == "y" || number[0] == "z")
                    return Math.Pow(NumbersWithXYZ(number[0]), 1 / double.Parse(number[1]));
            }
            return 0;
        }
    }
}
