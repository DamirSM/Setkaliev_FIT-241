using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    delegate void CarWash(Car car);
    
    class Car
    {
        public string brand { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public bool clean { get; set; }

        public Car(string brand, string name, int year, bool clean)
        {
            this.brand = brand;
            this.name = name;
            this.year = year;
            this.clean = clean;
        }
    }

    class Garage
    {
        public List<Car> cars { get; set; }

        public Garage(List<Car> cars)
        {
            this.cars = cars;
        }
    }

    class Washer
    {
        public static void Wash(Car car)
        {
            if (car.clean)
                Console.WriteLine("Машина марки " + car.brand + " " + car.year + " года выпуска уже чистая");
            else
            {
                car.clean = true;
                Console.WriteLine("Машина марки " + car.brand + " " + car.year + " года выпуска помыта");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Car> car_lst = new List<Car>();
            while (true)
            {
                Console.Write("Марка автомобиля: ");
                string brand = Console.ReadLine();
                if (brand == "")
                    break;
                Console.Write("ФИО владельца: ");
                string name = Console.ReadLine();
                if (name == "")
                    break;
                Console.Write("Год выпуска: ");
                string temp = Console.ReadLine();
                if (temp == "")
                    break;
                int year = Convert.ToInt32(temp);
                Console.Write("Чистота машины (да или нет): ");
                temp = Console.ReadLine();
                if (temp == "")
                    break;
                bool clean = false;
                if (temp == "да")
                    clean = true;
                Car car = new Car(brand, name, year, clean);
                car_lst.Add(car);
                Console.WriteLine();
            }

            Garage garage = new Garage(car_lst);
            CarWash wash = Washer.Wash;

            foreach (Car car in garage.cars)
            {
                wash(car);
            }
        }
    }
}
