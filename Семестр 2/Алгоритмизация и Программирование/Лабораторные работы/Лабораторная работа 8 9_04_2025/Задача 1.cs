using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class GenArr<T>
    {
        public T[] arr;
        int end;

        public GenArr()
        {
            arr = new T[1];
            end = 0;
        }

        public void Add(T newEntry)
        {
            arr[end] = newEntry;
            end++;
            Array.Resize(ref arr, end + 1);
        }

        public void Del(int ind)
        {
            if (ind < 0 || ind >= end)
            {
                Console.WriteLine("Индекс выходит за пределы");
                throw new IndexOutOfRangeException();
            }
            else
            {
                for (int i = ind; i < end - 1; i++)
                    arr[i] = arr[i + 1];
                if (end > 1)
                    Array.Resize(ref arr, end - 1);
                end--;
            }
        }

        public T Find(int ind)
        {
            if (ind < 0 || ind >= end)
            {
                Console.WriteLine("Индекс выходит за пределы");
                throw new IndexOutOfRangeException();
            }
            return arr[ind];
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GenArr<int> arr = new GenArr<int>();
            arr.Add(1);
            arr.Del(0);
            arr.Add(2);
            Console.WriteLine(arr.Find(0) + "\n");
            arr.Del(0);

            for (int i = 0; i < 5; i++)
            {
                arr.Add(i*2);
                Console.WriteLine(arr.Find(i));
            }
            Console.WriteLine("\n" + arr.Find(3) + "\n");
            arr.Del(3);
            Console.WriteLine(arr.Find(3));
        }
    }
}
