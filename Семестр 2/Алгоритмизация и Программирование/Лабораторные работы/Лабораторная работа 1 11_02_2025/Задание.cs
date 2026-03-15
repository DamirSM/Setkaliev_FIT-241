using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Person
    {
        public string name { get; set; }
        public Phone[] phones { get; set; }
        public string city { get; set; }

        public Person(string name, Phone[] phones, string city)
        {
            this.name = name;
            this.phones = phones;
            this.city = city;
        }

        public void Print()
        {
            Console.WriteLine("ФИО: " + name);
            for (int i = 0; i < phones.Length; i++)
                Console.WriteLine("Номер телефона : " + phones[i].number);
            Console.WriteLine("Город проживания: " + city);
        } 
    }

    class Phone
    {
        public int number { get; set; }
        public string mobile_oper { get; set; }
        public int year { get; set; }

        public Phone(int number, string mobile_oper, int year)
        {
            this.number = number;
            this.mobile_oper = mobile_oper;
            this.year = year;
        }

        public void Print()
        {
            Console.WriteLine("Номер телефона: " + number);
            Console.WriteLine("Оператор мобильной связи: " + mobile_oper);
            Console.WriteLine("Дата заключения договора (год): " + year);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int amount = 0;
            Person[] database = new Person[amount];
            while (option < 6)
            {
                Console.WriteLine("1. Заполение\n" +
                "2. Выборка по дате заключения договора (год)\n" +
                "3. Выборка по оператору связи\n" +
                "4. Выборка по номеру телефона\n" +
                "5. Выборка по городу проживания абонента\n" +
                "6. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("\nВведите количество абонентов: ");
                        amount = Convert.ToInt32(Console.ReadLine());
                        database = new Person[amount];
                        for (int i = 0; i < amount; i++)
                        {
                            Console.WriteLine("\nВведите ФИО, город проживания, количество номеров телефонов: ");
                            Console.Write("ФИО: ");
                            string name = Console.ReadLine();
                            Console.Write("Город проживания: ");
                            string city = Console.ReadLine();
                            Console.Write("Количество номеров телефонов: ");
                            int phone_amount = Convert.ToInt32(Console.ReadLine());
                            Phone[] phones = new Phone[phone_amount];
                            for (int j = 0; j < phone_amount; j++)
                            {
                                Console.Write("\nНомер телефона: ");
                                int number = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Оператор связи: ");
                                string mobile_oper = Console.ReadLine();
                                Console.Write("Дата заключения договора (год): ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                phones[j] = new Phone(number, mobile_oper, year);
                            }
                            database[i] = new Person(name, phones, city);
                        }
                        break;
                    case 2:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите год заключения договора: ");
                            int sel_year = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount; i++)
                            {
                                Phone[] sel_phones = database[i].phones;
                                for (int j = 0; j < sel_phones.Length; j++)
                                    if (sel_phones[j].year == sel_year)
                                    {
                                        Console.WriteLine();
                                        sel_phones[j].Print();
                                    }
                            }

                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                    case 3:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите название оператора связи: ");
                            string sel_mobile_oper = Console.ReadLine();
                            for (int i = 0; i < amount; i++)
                            {
                                Phone[] sel_phones = database[i].phones;
                                for (int j = 0; j < sel_phones.Length; j++)
                                    if (sel_phones[j].mobile_oper == sel_mobile_oper)
                                        Console.WriteLine(sel_phones[j].number);
                            }
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                    case 4:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите номер телефона: ");
                            int sel_number = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < amount; i++)
                            {
                                Phone[] sel_phones = database[i].phones;
                                for (int j = 0; j < sel_phones.Length; j++)
                                    if (sel_phones[j].number == sel_number)
                                    {
                                        Console.WriteLine("\nФИО: " + database[i].name);
                                        sel_phones[j].Print();
                                        break;
                                    }
                            }
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                    case 5:
                        if (amount > 0)
                        {
                            Console.Write("\nВведите город проживания: ");
                            string sel_city = Console.ReadLine();
                            for (int i = 0; i < amount; i++)
                            {
                                if (database[i].city == sel_city)
                                {
                                    Console.WriteLine();
                                    database[i].Print();
                                }
                            }
                        }
                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                }
            }
        }
    }
}
