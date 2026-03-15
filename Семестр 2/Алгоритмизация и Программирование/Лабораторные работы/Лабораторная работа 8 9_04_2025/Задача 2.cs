using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Calculator<T>
    {
        public T a { get; set; }
        public T b { get; set; }

        public Calculator(T a, T b)
        {
            this.a = a;
            this.b = b;
        }

        public T Sum()
        {
            return (dynamic)a + (dynamic)b;
        }

        public T Substr()
        {
            return (dynamic)a - (dynamic)b;
        }

        public T Mult()
        {
            return (dynamic)a * (dynamic)b;
        }

        public T Div()
        {
            if ((dynamic)b == 0)
                Console.WriteLine("Деление на нуль невозможно");
            else
                return (dynamic)a / (dynamic)b;
            throw new DivideByZeroException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator<int> calc1 = new Calculator<int>(10, 2);
            Console.WriteLine(calc1.Sum());
            Console.WriteLine(calc1.Substr());
            Console.WriteLine(calc1.Mult());
            Console.WriteLine(calc1.Div() + "\n");

            Calculator<double> calc2 = new Calculator<double>(7.5, 2.5);
            Console.WriteLine(calc2.Sum());
            Console.WriteLine(calc2.Substr());
            Console.WriteLine(calc2.Mult());
            Console.WriteLine(calc2.Div() + "\n");

            calc2.b = 0;
            Console.WriteLine(calc2.Div());
        }
    }
}
