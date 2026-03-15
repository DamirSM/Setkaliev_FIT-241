using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //ООП Наследование
    
    //Задача
    
    //1 Класс - Печь
    //Поля:
    //температура
    //время в минутах (приготовления при данной температуре)

    //Два класса-наследника класса печь:
    //1 Класс-наследник от печи Хлеб
    //Поля:
    //наименование хлеба
    //вес хлеба (в граммах)

    //2 Класс-наследник от печи Мясо
    //Поля:
    //наименование мяса
    //вес мяса (в граммах)

    //Меню по заполнению (база по хлебу, база по мясу):
    //Заполнение баз данных
    //Поиск по времени приготовления (и хлеб, и мясо по обеим базам)
    //Поиск по температурнуму режиму (и хлеб, и мясо по обеим базам)
    //Выдавать полностью все данные (писать отдельно, если нет базы данных по хлебу и базе данных по мясу)
    //Выход

    class Furnace
    {
        public int temperature;
        public int time;
        public Furnace(int temperature, int time)
        {
            this.temperature = temperature;
            this.time = time;
        }
    }

    class Bread : Furnace
    {
        string name;
        int weight;
        public Bread(string name, int weight, int temperature, int time): base (temperature, time)
        {
            this.name = name;
            this.weight = weight;
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, weight, temperature, time);
        }
    }

    class Meat : Furnace
    {
        string name;
        int weight;
        public Meat(string name, int weight, int temperature, int time): base (temperature, time)
        {
            this.name = name;
            this.weight = weight;
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, weight, temperature, time);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int amount_b = 0;
            int amount_m = 0;
            Bread[] database_b = new Bread[amount_b];
            Meat[] database_m = new Meat[amount_m];
            while (option < 5)
            {
                Console.WriteLine("\n1. Заполение базы данных хлеба\n" +
                "2. Заполнение базы данных мяса\n" +
                "3. Выборка по длительности\n" +
                "4. Выборка по температуре\n" +
                "5. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("\nВведите количество сортов хлеба: ");
                        amount_b = Convert.ToInt32(Console.ReadLine());
                        database_b = new Bread[amount_b];
                        for (int i = 0; i < amount_b; i++)
                        {
                            Console.WriteLine("\nВведите сорт, вес, температуру и время выпечки хлеба: ");
                            Console.Write("Сорт: ");
                            string type = Console.ReadLine();
                            Console.Write("Вес: ");
                            int mass = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Температура в градусах Цельсия: ");
                            int degrees = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Время в минутах: ");
                            int minutes = Convert.ToInt32(Console.ReadLine());
                            database_b[i] = new Bread(type, mass, degrees, minutes);
                        }
                        break;
                    case 2:
                        Console.Write("\nВведите количество видов мяса: ");
                        amount_m = Convert.ToInt32(Console.ReadLine());
                        database_m = new Meat[amount_m];
                        for (int i = 0; i < amount_m; i++)
                        {
                            Console.WriteLine("\nВведите вид, температуру и время приготовления мяса: ");
                            Console.Write("Вид: ");
                            string type = Console.ReadLine();
                            Console.Write("Вес: ");
                            int mass = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Температура в градусах Цельсия: ");
                            int degrees = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Время в минутах: ");
                            int minutes = Convert.ToInt32(Console.ReadLine());
                            database_m[i] = new Meat(type, mass, degrees, minutes);
                        }
                        break;
                    case 3:
                        if (amount_b == 0) Console.WriteLine("\nБаза данных хлеба не заполнена");
                        if (amount_b > 0 || amount_m > 0)
                        {
                            Console.Write("\nВведите минимальное время приготовления: ");
                            int time_check = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount_b; i++)
                                if (database_b[i].time >= time_check)
                                    database_b[i].Print();
                            for (int i = 0; i < amount_m; i++)
                                if (database_m[i].time >= time_check)
                                    database_m[i].Print();
                        }
                        if (amount_m == 0) Console.WriteLine("\nБаза данных мяса не заполнена");
                        else if (amount_b == 0 && amount_m == 0) Console.WriteLine("\nБазы данных не заполнены\n");
                        break;
                    case 4:
                        if (amount_b == 0) Console.WriteLine("\nБаза данных хлеба не заполнена");
                        if (amount_b > 0 || amount_m > 0)
                        {
                            Console.Write("\nВведите температуру приготовления: ");
                            int temperature_check = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount_b; i++)
                                if (database_b[i].temperature == temperature_check)
                                    database_b[i].Print();
                            for (int i = 0; i < amount_m; i++)
                                if (database_m[i].temperature == temperature_check)
                                    database_m[i].Print();
                        }
                        if (amount_m == 0) Console.WriteLine("\nБаза данных мяса не заполнена");
                        else if (amount_b == 0 && amount_m == 0) Console.WriteLine("\nБазы данных не заполнены\n");
                        break;
                }
            }
        }
    }
}