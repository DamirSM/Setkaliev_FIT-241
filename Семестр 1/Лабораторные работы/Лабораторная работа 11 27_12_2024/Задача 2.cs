using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    //Задача 2

    //Студент

    //1 Класс - "оценки" (Поля: наименование учебного предмета, оценка)
    //2 Класс - "студет" (Поля: ФИО, массив(или список) из объектов класса "оценки")

    //1 Выборка(если база из студентов заполнена) - выдать всех студентов, у которых средний балл выше, чем 4.5.
    //Выход

    class Grades
    {
        public string name { get; set; }
        public double grade { get; set; }

        public Grades(string name, double grade)
        {
            this.name = name;
            this.grade = grade;
        }
    }

    class Student
    {
        public string name { get; set; }
        Grades[] s_grades = null;

        public Student(string name, Grades[] s_grades)
        {
            this.name = name;
            this.s_grades = s_grades;
        }

        public double Average()
        {
            double sum = 0;
            int count = 0;
            foreach (Grades obj in s_grades)
            {
                sum += obj.grade;
                count++;
            }
            double result = sum/count;
            return (result);
        }

        public void Print()
        {
            Console.WriteLine(name, Average());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int amount = 0;
            Student[] database = null;
            while (option < 3)
            {
                Console.WriteLine("\n1. Заполение\n" +
                "2. Выборка (средний балл > 4,5)\n" +
                "3. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("\nВведите количество предметов: ");
                        int subj_amount = Convert.ToInt32(Console.ReadLine());
                        string[] subjects = new string[subj_amount];
                        for (int i = 0; i < subj_amount; i++)
                            subjects[i] = Console.ReadLine();
                        Console.Write("\nВведите количество студентов: ");
                        amount = Convert.ToInt32(Console.ReadLine());
                        database = new Student[amount];
                        for (int i = 0; i < amount; i++)
                        {
                            Console.Write("\nВведите ФИО студента: ");
                            string name = Console.ReadLine();
                            Grades[] grades = new Grades[subj_amount];
                            Console.WriteLine("\nВведите оценки: ");
                            for (int j = 0; j < subj_amount; j++)
                            {
                                Console.Write("{0}: ", subjects[j]);
                                double grade = Convert.ToDouble(Console.ReadLine());
                                grades[j] = new Grades(subjects[j], grade);
                            }
                            database[i] = new Student(name, grades);
                        }
                        break;

                    case 2:
                        if (amount > 0)
                        {
                            for (int i = 0; i < amount; i++)
                                if (database[i].Average() > 4.5)
                                    database[i].Print();
                        }
                        else Console.WriteLine("\nБаза данных не заполнена");
                        break;
                }
            }
        }
    }
}