using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Phone
    {
        public uint phone_number { get; set; }
        public string owner { get; set; }
        public uint year { get; set; }
        public string provider { get; set; }

        public Phone(uint phone_number, string owner, uint year, string provider)
        {
            this.phone_number = phone_number;
            this.owner = owner;
            this.year = year;
            this.provider = provider;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Phone phone_1 = new Phone(123, "Ivan", 2000, "Tele1");
            Phone phone_2 = new Phone(9999, "Petr", 2010, "Tele1");
            Phone phone_3 = new Phone(789432985, "Alex", 2010, "NTS");
            Phone[] database = new Phone[]{phone_1, phone_2, phone_3};

            int option = 0;
            int len = database.Length;

            while (option < 5)
            {
                Console.WriteLine("1. Выборка по оператору связи\n" +
                "2. Выборка по году выпуска телефона\n" +
                "3. Выдать все данные, сгрупированные по оператору связи\n" +
                "4. Выдать все данные, сгрупированные по году выпуска\n" +
                "5. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("\nВведите название оператора связи: ");
                        string sel_prov = Console.ReadLine();

                        var phones_by_prov = from phone in database
                                             where phone.provider == sel_prov
                                             select phone.phone_number;

                        foreach (uint number in phones_by_prov) Console.WriteLine(number);
                        Console.WriteLine();

                        break;

                    case 2:
                        Console.Write("\nВведите год заключения договора: ");
                        int sel_year = Convert.ToInt32(Console.ReadLine());

                        var phones_by_year = from phone in database
                                             where phone.year == sel_year
                                             select phone.phone_number;

                        foreach (uint number in phones_by_year) Console.WriteLine(number);
                        Console.WriteLine();

                        break;

                    case 3:
                        var data_by_prov = from phone in database
                                           group phone by phone.provider;

                        foreach (var phones in data_by_prov)
                        {
                            Console.WriteLine("Телефоны, сгрупированные по оператору: " + phones.Key);
                            foreach (Phone phone in phones)
                                Console.Write(phone.phone_number + ", " + phone.owner + ", " + phone.year + "\n");
                            Console.WriteLine();
                        }

                        break;
                    case 4:
                        var data_by_year = from phone in database
                                           group phone by phone.year;

                        foreach (var phones in data_by_year)
                        {
                            Console.WriteLine("Телефоны, сгрупированные по году выпуска: " + phones.Key);
                            foreach (Phone phone in phones)
                                Console.Write(phone.phone_number + ", " + phone.owner + ", " + phone.provider + "\n");
                            Console.WriteLine();
                        }

                        break;
                }
            }
        }
    }
}
