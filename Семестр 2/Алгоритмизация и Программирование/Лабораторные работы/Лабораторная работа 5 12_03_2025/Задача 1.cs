using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Phone
    {
        public int number { get; set; }
        public string mobile_oper { get; set; }
        
        public Phone (int number, string mobile_oper)
        {
            this.number = number;
            this.mobile_oper = mobile_oper;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Phone> phones = new List<Phone>();
            while(true)
            {
                Console.Write("Номер телефона: ");
                string temp = Console.ReadLine();
                if (temp == "")
                    break;
                int number = Convert.ToInt32(temp);
                Console.Write("Оператор связи: ");
                string mobile_oper = Console.ReadLine();
                if (mobile_oper == "")
                    break;
                Phone phone = new Phone(number, mobile_oper);
                phones.Add(phone);
		Console.WriteLine();
            }

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (Phone phone in phones)
            {
                string key = phone.mobile_oper;
                if (dict.ContainsKey(key))
                {
                    dict[key]++;
                }
                else
                    dict.Add(key, 1);
            }

            foreach (string key in dict.Keys)
            {
                Console.WriteLine(key + ": " + dict[key]);
            }
        }
    }
}
