using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> lst = new List<int>();
            Console.WriteLine("Введите количество элементов целочисленного списка:");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите элементы целочисленного списка:");
            for (int i = 0; i < amount; i++)
            {
                int var = Convert.ToInt32(Console.ReadLine());
                lst.Add(var);
            }


            // Задача 1
            SortedSet<int> hashset = new SortedSet<int>(lst);

            Console.WriteLine("Список состоит из следующих элементов: ");
            foreach (int elem in hashset)
            {
                Console.Write(elem + " ");
            }


            // Задача 2
            SortedDictionary<int, int> dict = new SortedDictionary<int, int>();

            int count = 0;
            foreach (int elem in lst)
            {
                if (dict.ContainsKey(elem))
                {
                    dict[elem]++;
                    if (dict[elem] > count)
                        count = dict[elem];
                }
                else
                    dict.Add(elem, 1);
            }

            // Если бы не было HashSet (здесь SortedSet), то понадобилась бы коллекция ключей:
            // ICollection<int> keys = dict.Keys;

            Console.WriteLine("\nМаксимальное количество одинаковых элементов: " + count);
            Console.WriteLine("Наиболее часто встречающиеся элементы: ");
            foreach(int key in hashset)
            {
                if (dict[key] == count)
                    Console.Write(key + " ");
            }
        }
    }
}
