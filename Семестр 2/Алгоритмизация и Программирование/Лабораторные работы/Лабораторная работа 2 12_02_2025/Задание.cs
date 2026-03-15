using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Figure
    {
        public string name { get; set; }
        public Figure(string name)
        {
            this.name = name;
        }
    }

    public interface IMeth
    {
        double Area();
        double Perimetr();
    }

    class Circle: Figure, IMeth
    {
        public int radius { get; set; }
        public Circle(int radius, string name)
            : base(name)
        {
            this.radius = radius;
        }
        public double Area()
        {
            double area = 3.14 * Math.Pow(radius, 2.0);
            return area;
        }
        public double Perimetr()
        {
            double perimetr = 2 * 3.14 * radius;
            return perimetr;
        }
    }

    class Square: Figure, IMeth
    {
        public int side { get; set; }
        public Square(int side, string name): base(name)
        {
            this.side = side;
        }
        public double Area()
        {
            double area = Math.Pow(side, 2.0);
            return area;
        }
        public double Perimetr()
        {
            double perimetr = 4 * side;
            return perimetr;
        }
    }

    class Triangle: Figure, IMeth
    {
        public int side { get; set; }
        public Triangle(int side, string name): base(name)
        {
            this.side = side;
        }
        public double Area()
        {
            double area = Math.Pow(side, 2.0) * Math.Pow(3, 0.5) / 4;
            return area;
        }
        public double Perimetr()
        {
            double perimetr = 3 * side;
            return perimetr;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int amount = 3;
            IMeth[] database = new IMeth[amount];
            while (option < 3)
            {
                Console.WriteLine("\n1. Заполение\n" +
                "2. Вывод результатов\n" +
                "3. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        for (int i = 0; i < amount; i++)
                        {
                            Console.Write("\nВведите номер фигуры (1 - Круг, 2 - Квадрат, 3 - Треугольник): ");
                            option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1)
                                Console.Write("Радиус: ");
                            else Console.Write("Длина стороны: ");
                            int side = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Название: ");
                            string name = Console.ReadLine();
                            switch (option)
                            {
                                case 1:
                                    database[i] = new Circle(side, name);
                                    break;
                                case 2:
                                    database[i] = new Square(side, name);
                                    break;
                                case 3:
                                    database[i] = new Triangle(side, name);
                                    break;
                            }
                        }
                        option = 0;
                        break;
                    case 2:
                        for (int i = 0; i < amount; i++)
                        {
                            Console.WriteLine("\nПлощадь фигуры №" + (i+1) + ": " + database[i].Area());
                            Console.WriteLine("Периметр фигуры №" + (i+1) + ": " + database[i].Perimetr());
                        }
                        break;
                }
            }
        }
    }
}
