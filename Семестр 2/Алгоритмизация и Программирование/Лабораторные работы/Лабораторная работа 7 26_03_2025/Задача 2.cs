using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    delegate void EventHandler();

    class Participant
    {
        public string name;
        public int speed { get; set; }

        public Participant(string name, int speed)
        {
            this.name = name;
            this.speed = speed;
        }

        public Participant()
        {
            this.name = string.Empty;
            this.speed = 0;
        }

        public void PHandler()
        {
            Console.WriteLine(name + " победил!");
        }
    }

    class Event
    {
        public event EventHandler SomeEvent;

        public void OnSomeEvent()
        {
            if (SomeEvent != null)
                SomeEvent();
        }
    }

    class Program
    {
        public static bool Check(Participant[] p_arr, int[] dist_arr, int finish, out Participant[] winner)
        {
            List<Participant> listWinners = new List<Participant>();

            for (int i = 0; i < p_arr.Length; i++)
                if (dist_arr[i] >= finish)
                    listWinners.Add(p_arr[i]);

            if (listWinners.Count > 0)
            {
                winner = listWinners.ToArray();
                return true;
            }
            else
            {
                winner = new Participant[0];
                return false;
            }
        }

        static void MultipleHandler()
        {
            Console.WriteLine("Победила дружба!");
        }

        static void Main(string[] args)
        {
            Event evt = new Event();

            Participant[] p_arr = new Participant[3] {
                new Participant("Иван", 10),
                new Participant("Джон", 5),
                new Participant("Хуан", 20)
            };

            int n = p_arr.Count();
            int time = 0;
            int finish = 1000;


            int[] dist_arr = new int[n];
            int[] speed_arr = new int[n];
            

            while(true)
            {
                time += 5;

                for (int i = 0; i < n; i++)
                {
                    speed_arr[i] = p_arr[i].speed;
                    dist_arr[i] += speed_arr[i]*time;
                    Console.WriteLine(p_arr[i].name + ": " + speed_arr[i] + " * " + time + " = " + dist_arr[i]);
                }

                Console.WriteLine();

                if (Check(p_arr, dist_arr, finish, out Participant[] p_winner))
                {
                    if (p_winner.Length > 1)
                        evt.SomeEvent += MultipleHandler;
                    else
                        evt.SomeEvent += p_winner[0].PHandler;
                    evt.OnSomeEvent();
                    break;
                }

                Random rnd = new Random();
                for (int i = 0; i < n; i++)
                {
                    p_arr[i].speed = speed_arr[rnd.Next(0, n)];
                }
            }
        }
    }
}
