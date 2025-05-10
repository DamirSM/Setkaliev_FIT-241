using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    struct Book
    {
        public string author;
        public string name;
        public int year;
        public string publisher;

        public Book(string author, string name, int year, string publisher)
        {
            this.author = author;
            this.name = name;
            this.year = year;
            this.publisher = publisher;
        }
    }

    struct Logbook
    {
        public string date_getting;
        public string date_returning;

        public Logbook(string date_getting, string date_returning)
        {
            this.date_getting = date_getting;
            this.date_returning = date_returning;
        }
    }

    class Library
    {
        public Book book { get; set; }
        public List<Logbook> logbook { get; set; }

        public Library(Book book, List<Logbook> logbook)
        {
            this.book = book;
            this.logbook = logbook;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            List<Library> database = new List<Library>();
            while (option < 4)
            {
                Console.WriteLine("1. Заполение\n" +
                "2. Вывод сведений о книгах, которые ни разу не выдавались\n" +
                "3. Вывод сведений о книгах, которые не сданы.\n" +
                "4. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        while (true)
                        {
                            Console.WriteLine("\nПрекратить ввод - введите 0 в ФИО автора\nВведите ФИО автора, наименование книги, год выпуска, издательство: ");
                            Console.Write("ФИО автора: ");
                            string author = Console.ReadLine();
                            if (author == "0")
                            {
                                Console.WriteLine();
                                break;
                            }
                            Console.Write("Наименование книги: ");
                            string name = Console.ReadLine();
                            Console.Write("Год выпуска: ");
                            int year = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Издательство: ");
                            string publisher = Console.ReadLine();
                            Book book = new Book(author, name, year, publisher);
                            
                            Console.WriteLine("Заполнение формуляра.\nЕсли книга не была выдана или не была сдана - нажмите Enter.");
                            List<Logbook> logbook = new List<Logbook>();
                            while (true)
                            {
                                Console.Write("Введите дату выдачи книги: ");
                                string date_getting = Console.ReadLine();
                                string date_returning = "";
                                if (date_getting != "")
                                {
                                    Console.Write("Введите дату сдачи книги: ");
                                    date_returning = Console.ReadLine();
                                }
                                logbook.Add(new Logbook(date_getting, date_returning));
                                if (date_returning == "" )
                                    break;
                            }
                            database.Add(new Library(book, logbook));
                        }
                        break;
                    case 2:
                        if (database.Count > 0)
                        {
                            Console.WriteLine("\nВывод сведений о книгах, которые ни разу не выдавались: ");
                            bool check = true;
                            for (int i = 0; i < database.Count; i++)
                            {
                                List<Logbook> logbook = database[i].logbook;
                                if (logbook[0].date_getting == "")
                                {
                                    check = false;
                                    Book book = database[i].book;
                                    Console.WriteLine("ФИО автора: " + book.author);
                                    Console.WriteLine("Наименование книги: " + book.name);
                                    Console.WriteLine("Год выпуска: " + book.year);
                                    Console.WriteLine("Издательство: " + book.publisher + "\n");
                                }
                            }
                            if (check)
                                Console.WriteLine("Не выдававшихся ни разу книг нет");
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                    case 3:
                        if (database.Count > 0)
                        {
                            Console.WriteLine("\nВывод сведений о книгах, которые не сданы: ");
                            bool check = true;
                            for (int i = 0; i < database.Count; i++)
                            {
                                List<Logbook> logbook = database[i].logbook;
                                if (logbook[logbook.Count-1].date_getting != "" && logbook[logbook.Count-1].date_returning == "")
                                {
                                    check = false;
                                    Book book = database[i].book;
                                    Console.WriteLine("ФИО автора: " + book.author);
                                    Console.WriteLine("Наименование книги: " + book.name);
                                    Console.WriteLine("Год выпуска: " + book.year);
                                    Console.WriteLine("Издательство: " + book.publisher);
                                    Console.WriteLine("Дата выдачи: " + logbook[0].date_getting + "\n");
                                }
                            }
                            if (check)
                                Console.WriteLine("Несданных книг нет");
                        }

                        else Console.WriteLine("\nБаза данных не заполнена\n");
                        break;
                }
            }
        }
    }
}
