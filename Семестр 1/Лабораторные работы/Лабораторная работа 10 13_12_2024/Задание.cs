using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //Классы

    //Дан класс, состоящий из двух полей целого типа.
    //Необходимо реализовать в классе 3 конструктора:
    //конструктор без параметров (в это случае все поля инициализиуются нулями),
    //конструктор с одним параметром(в этом случае второе поле инициализируется нулём),
    //конструктор с двумя параметрами(в этом случае поля инициализируются заданными значениями).

    //Создать четыре метода, которые реализуют:
    //сложение двух элементов,
    //разность первого со вторым,
    //произведение двух элементов,
    //деление первого на второй(при делении отловить ошибку деления на ноль)

    //В головной программе(Main) создать объекты с помощью трёх разных конструкторов.
    //Для каждого объекта выполнить все четыре метода, выдать результаты.
    //Выдавать результаты можно как в методе, так и с помощью передачи результатов в головную программу (Main).
    
    class Algebra
    {
        // Поля класса
        int var_1;
        int var_2;

        // | Конструкторы |
        
        // || Конструктор без параметров ||
        public Algebra()
        {
            var_1 = 0;
            var_2 = 0;
        }

        // || Конструктор с одним параметром ||
        public Algebra(int var_1)
        {
            this.var_1 = var_1;
            var_2 = 0;
        }

        // || Конструктор с двумя параметрами ||
        public Algebra(int var_1, int var_2)
        {
            this.var_1 = var_1;
            this.var_2 = var_2;
        }

        // | Методы |

        // || Сложение двух элементов ||
        public int Sum ()
        {
            int result = var_1 + var_2;
            return result;
        }

        // || Разность первого со вторым ||
        public int Dif()
        {
            int result = var_1 - var_2;
            return result;
        }

        // || Произведение двух элементов ||
        public int Mult()
        {
            int result = var_1 * var_2;
            return result;
        }

        // || Деление первого на второй ||
        // || (при делении отловить ошибку деления на ноль) ||
        public void Div()
        {
            if (var_2 == 0)
                Console.WriteLine("Ошибка: деление на нуль невозможно");
            else
            {
                int result = var_1 / var_2;
                Console.WriteLine(result);
            }
        }

        // || Метод для выведения результатов ||
        public void Print()
        {
            Console.WriteLine("Сумма: " + Sum());
            Console.WriteLine("Разность: " + Dif());
            Console.WriteLine("Умножение: " + Mult());
            Console.Write("Деление: ");
            Div();
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Algebra obj1 = new Algebra();
            obj1.Print();

            int var = Convert.ToInt32(Console.ReadLine());
            Algebra obj2 = new Algebra(var);
            obj2.Print();

            string[] input = Console.ReadLine().Split();
            Algebra obj3 = new Algebra(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]));
            obj3.Print();
        }
    }
}
