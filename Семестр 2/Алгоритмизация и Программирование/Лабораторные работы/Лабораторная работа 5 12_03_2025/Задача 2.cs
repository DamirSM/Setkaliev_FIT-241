using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    delegate void Eval(ref int input);

    class Equation
    {
        public int a { get; set; }
        public int b { get; set; }

        public Equation(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public int Sum(int x1, int x2)
        {
            return (x1+x2);
        }

        public int Dif(int x1, int x2)
        {
            return (x1 - x2);
        }

        public int Mult(int x1, int x2)
        {
            return(x1*x2);
        }

        public int Div(int x1, int x2)
        {
            if (x2 == 0)
            {
                Console.WriteLine("Деление на ноль невозможно");
                throw new DivideByZeroException();
            }
            return(x1/x2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Equation eq = new Equation(10, 5);

            Eval op1 = null;
            op1 += (ref int result) =>
            { 
                result = eq.Sum(eq.a, eq.b);
            };

            op1 += (ref int result) =>
            {
                result = eq.Dif(result, eq.b);
            };

            op1 += (ref int result) =>
            {
                result = eq.Mult(result, eq.a);
            };

            int result1 = 0;
            op1(ref result1);

            Console.WriteLine(result1);

            Eval op2 = null;
            op2 += (ref int result) =>
            {
                result = eq.Mult(eq.a, eq.b);
            };

            op2 += (ref int result) =>
            {
                result = eq.Dif(result, eq.a);
            };

            op2 += (ref int result) =>
            {
                result = eq.Div(result, eq.a);
            };

            int result2 = 0;
            op2(ref result2);

            Console.WriteLine(result2);
        }
    }
}
