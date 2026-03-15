using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input_name = "Input.txt";
            string file_input = @"C:\Users\User\Desktop\Семестр 2\АиП\Лабораторные работы\Лабораторная работа 9 23_04_2025\" + input_name;
            string output_name = "Output.txt";
            string file_output = @"C:\Users\User\Desktop\Семестр 2\АиП\Лабораторные работы\Лабораторная работа 9 23_04_2025\" + output_name;
            File.Create(file_input).Close();
            File.Create(file_output).Close();
            FileInfo f = new FileInfo(file_input);
            StreamWriter sw = f.AppendText();
            sw.WriteLine("1dasdaw2");
            sw.WriteLine("adwd10dwadf");
            sw.WriteLine("dwad12dwad1dawd");
            sw.WriteLine("dasdf30");
            sw.WriteLine("dawdfsdf25");
            sw.Close();

            
            string text_in = File.ReadAllText(file_input);
            Console.WriteLine("Входной текст:");
            Console.WriteLine(text_in);
            string[] strs = text_in.Split('\n');
            foreach (string s in strs)
            {
                bool check = false;
                string temp_s = "";
                int num;
                for (int i = 0; i < s.Length; i++)
                {
                    if ("1234567890".Contains(s[i]))
                        temp_s += s[i];
                    else
                    {
                        bool result = int.TryParse(temp_s, out num);
                        temp_s = "";
                        if (result == true) 
                        {
                            if (num % 2 == 1)
                            {
                                check = true;
                                break;
                            }
                        }
                    }
                }
                if (check)
                {
                    f = new FileInfo(file_output);
                    sw = f.AppendText();
                    sw.WriteLine(s.Remove(s.Count()-1));
                    sw.Close();
                }
            }

            string text_out = File.ReadAllText(file_output);
            Console.WriteLine("Результирующий файл:");
            Console.WriteLine(text_out);
        }
    }
}
