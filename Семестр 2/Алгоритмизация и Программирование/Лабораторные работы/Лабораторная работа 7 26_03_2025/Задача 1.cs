using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Dot
    {
        public int x;
        public int y;
        static Random rnd = new Random();

        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Move()
        {
            x += rnd.Next(-2, 2);
            y += rnd.Next(-2, 2);
        }
    }

    class Event
    {
        public event EventHandler SomeEvent;

        public void OnSomeEvent()
        {
            if (SomeEvent != null)
                SomeEvent(this, EventArgs.Empty);
        }
    }

    class Program
    {
        static void Handler(object source, EventArgs arg)
        {
            Console.WriteLine("Точка вышла за пределы области");
        }

        static void Main(string[] args)
        {
            Event evt = new Event();

            Dot a = new Dot(0, 0);
            Dot b = new Dot(6, 6);

            Dot dot = new Dot(3, 3);

            while (true)
            {
                dot.Move();
                Console.WriteLine(dot.x + " " + dot.y);
                if (dot.x < a.x || dot.x > b.x || dot.y < a.y || dot.y > b.y)
                {
                    evt.SomeEvent += Handler;
                    evt.OnSomeEvent();
                    break;
                }
            }
        }
    }
}
