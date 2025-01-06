using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //Задача 1

    //Меню

    //Дан класс "Печь", в котором выставляются поля "температура", "длительность".
    //Дан класс "Хлеб", в котором выставляются поля от родителя("температура", "длительность (время выпечки)"),
    //"наименование (пшеничный, урожайный и т.д.)"

    //Конструктор с отсылкой на базовый

    //Создать меню с пунктами, где будет заполнение базы данных(по хлебу) - массив(или список)
    //Выборка по длительности(всё, что печётся дольше, чем заданное время, можно в часах и минутах или просто в минутах)
    //Выборка по температурному режиму(всё, что печётся при заданной температуре)

    //Если база не заполнена, то выводить это(выборка невозоможна)

    //Возвращаться в главное меню(while).

    //Меню:
    //1. Заполение
    //2. Выборка по длительности
    //3. Выборка по температуре
    //4. Выход

    class Furnace
    {
        public int temperature { get; set; }
        public int time { get; set; }

        public Furnace ()
        {
            temperature = 180;
            time = 40;
        }

        public Furnace (int temperature, int time)
        {
            this.temperature = temperature;
            this.time = time;
        }
    }

    class Bread: Furnace
    {
        string name;
        
        public Bread (string name, int temperature, int time): base(temperature, time)
        {
            this.name = name;
        }

        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}", name, temperature, time);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int amount = 0;
            Bread[] database = new Bread[amount];
            while (option < 4)
            {
                Console.WriteLine("\n1. Заполение\n" +
                "2. Выборка по длительности\n" +
                "3. Выборка по температуре\n" +
                "4. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());
                
                switch (option)
                {
                    case 1:
                        Console.Write("\nВведите количество сортов хлеба: ");
                        amount = Convert.ToInt32(Console.ReadLine());
                        database = new Bread[amount];
                        for (int i = 0; i < amount; i++)
                        {
                            Console.WriteLine("\nВведите сорт, температуру и время выпечки хлеба: ");
                            Console.Write("Сорт: ");
                            string type = Console.ReadLine();
                            Console.Write("Температура в градусах Цельсия: ");
                            int degrees = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Время в минутах: ");
                            int minutes = Convert.ToInt32(Console.ReadLine());
                            database[i] = new Bread(type, degrees, minutes);
                        }
                        break;
                    case 2:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите минимальное время выпекания: ");
                            int time_check = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount; i++)
                                if (database[i].time >= time_check)
                                    database[i].Print();
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                    case 3:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите температуру выпекания: ");
                            int temperature_check = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount; i++)
                                if (database[i].temperature == temperature_check)
                                    database[i].Print();
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                }
            }
        }
    }
}