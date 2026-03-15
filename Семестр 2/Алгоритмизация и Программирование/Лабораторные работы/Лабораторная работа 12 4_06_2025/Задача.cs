using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    unsafe struct Animal
    {
        public int id { get; set; }
        public fixed char name[50];
        public Animal* left;
        public Animal* right;
    }

    unsafe class Program
    {
        const int max_amount = 50;

        static Animal* Create (Animal* buffer, ref int count, int id, string name)
        {
            if (count >= max_amount)
            {
                Console.WriteLine("Превышено количество элементов в буфере");
                return null;
            }
            Animal* elem = &buffer[count++];
            elem->id = id;
            fixed (char* p = name)
            {
                for (int i = 0; i < 50 && p[i] != '\0'; i++)
                {
                    elem->name[i] = p[i];
                }
                elem->name[Math.Min(49, name.Length)] = '\0';
            }
            elem->left = null;
            elem->right = null;
            return elem;
        }

        static void Insert(Animal** root_ptr, Animal* new_elem)
        {
            if (*root_ptr == null)
            {
                *root_ptr = new_elem;
                return;
            }
            Animal* current = *root_ptr;

            while(true)
            {
                if (new_elem->id > current->id)
                {
                    if (current->right == null)
                    {
                        current->right = new_elem;
                        return;
                    }
                    current = current->right;
                }
                else
                {
                    if (current->left == null)
                    {
                        current->left = new_elem;
                        return;
                    }
                    current = current->left;
                }
            }
        }

        static void Print(Animal* root)
        {
            if (root == null)
                return;

            Print(root->left);

            string name = new string(root->name);
            Console.WriteLine(root->id + ": " + name);

            Print(root->right);
        }

        static void Main(string[] args)
        {
            

            Animal* buffer = stackalloc Animal[max_amount];

            int count = 0;

            Animal* root = null;

            Animal* elem1 = Create(buffer, ref count, 10, "Слон");
            Animal* elem2 = Create(buffer, ref count, 5, "Носорог");
            Animal* elem3 = Create(buffer, ref count, 12, "Пантера");
            Animal* elem4 = Create(buffer, ref count, 6, "Бегемот");

            for(int i = 0; i < count; i++)
            {
                Insert(&root, &buffer[i]);
            }

            Print(root);
        }
    }
}
