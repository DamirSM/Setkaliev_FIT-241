using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Item
    {
        public uint id { get; set; }
        public string name { get; set; }
        public TimeSpan exp { get; set; }

        public Item(uint id, string name, TimeSpan exp)
        {
            this.id = id;
            this.name = name;
            this.exp = exp;
        }
    }

    class Supplier
    {
        public uint id { get; set; }
        public string name { get; set; }

        public Supplier(uint id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    class Operation
    {
        public uint item_id { get; set; }
        public uint supplier_id { get; set; }
        public bool send { get; set; }
        public uint amount { get; set; }
        public uint price { get; set; }
        public DateTime date { get; set; }

        public Operation(uint item_id, uint supplier_id, uint amount, uint price, DateTime date)
        {
            this.item_id = item_id;
            this.supplier_id = supplier_id;
            send = false;
            this.amount = amount;
            this.price = price;
            this.date = date;
        }

        public Operation(uint item_id, uint amount, uint price, DateTime date)
        {
            this.item_id = item_id;
            send = true;
            this.amount = amount;
            this.price = price;
            this.date = date;
        }

        public Operation(uint item_id, uint amount, DateTime date)
        {
            this.item_id = item_id;
            send = true;
            this.amount = amount;
            this.date = date;
        }
    }

    class Shop
    {
        public List<Item> items { get; set; }
        public List<Supplier> suppliers { get; set; }
        public List<Operation> operations { get; set; }

        public Shop(List<Item> items, List<Supplier> suppliers, List<Operation> operations)
        {
            this.items = items;
            this.suppliers = suppliers;
            this.operations = operations;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item> {
                new Item(2131, "Сосиски", new TimeSpan(31)),
                new Item(2243, "Курица", new TimeSpan(15)),
                new Item(6765, "Телефоны", new TimeSpan(365*5)),
                new Item(6981, "Телевизоры", new TimeSpan(365*3))
            };
            
            List<Supplier> suppliers = new List<Supplier> {
                new Supplier(21, "Ферма"),
                new Supplier(68, "Завод электроники")
            };

            Operation oper1 = new Operation(items[0].id, suppliers[0].id, 15, 100, DateTime.Parse("30.11.1999"));
            Operation oper2 = new Operation(items[0].id, 8, 160, DateTime.Parse("15.12.1999"));
            Operation oper3 = new Operation(items[0].id, suppliers[0].id, 15, 100, DateTime.Parse("16.12.1999"));
            Operation oper4 = new Operation(items[0].id, oper1.amount - oper2.amount, oper1.date + items[0].exp);
            Operation oper5 = new Operation(items[1].id, suppliers[0].id, 11, 200, DateTime.Parse("02.01.2000"));
            Operation oper6 = new Operation(items[1].id, 11, 350, DateTime.Parse("05.01.2000"));
            Operation oper7 = new Operation(items[2].id, suppliers[1].id, 5, 5000, DateTime.Parse("15.01.2000"));
            Operation oper8 = new Operation(items[2].id, 2, 7500, DateTime.Parse("17.01.2000"));
            Operation oper9 = new Operation(items[3].id, suppliers[1].id, 2, 30000, DateTime.Parse("15.01.2000"));
            Operation oper10 = new Operation(items[3].id, 1, 52000, DateTime.Parse("17.01.2000"));

            List<Operation> operations = new List<Operation> { oper1, oper2, oper3, oper4, oper5, oper6, oper7, oper8, oper9, oper10};

            Shop shop = new Shop(items, suppliers, operations);

            int option = 0;
            int len = shop.operations.Count;

            while (option < 4)
            {
                Console.WriteLine("1. Остатки по каждому товару\n" +
                "2. Списания товаров, отсортированные по дате\n" +
                "3. Выдать выручку от продаж за определённый интервал времени\n" +
                "4. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        var remains = from item in items
                                      join op in operations
                                      on item.id equals op.item_id
                                      into item_ops
                                      select new
                                      {
                                          item_id = item.id,
                                          item_name = item.name,
                                          item_remains = item_ops.Sum(op => op.send ? -(int)op.amount : (int)op.amount)
                                      };

                        foreach (var obj in remains)
                            Console.WriteLine("Остатки товара {0} (id: {1}): {2}", obj.item_name, obj.item_id, obj.item_remains);
                        Console.WriteLine();

                        break;

                    case 2:
                        var oper_write_offs_by_date = from op in operations
                                                      where op.send && op.price == 0
                                                      group op by op.date;
                        foreach (var opers in oper_write_offs_by_date)
                        {
                            Console.WriteLine("Списания, сгрупированные по дате: " + opers.Key);
                            foreach (Operation oper in opers)
                                Console.Write("{0}, {1}\n", oper.item_id, oper.amount);
                            Console.WriteLine();
                        }

                        break;

                    case 3:
                        Console.WriteLine("Введите интервал времени:");
                        Console.Write("От: ");
                        DateTime date_start = DateTime.Parse(Console.ReadLine());
                        Console.Write("До: ");
                        DateTime date_end = DateTime.Parse(Console.ReadLine());
                        var revenue_date_interval = from item in items
                                                join op in operations
                                                on item.id equals op.item_id
                                                into item_ops
                                                select new
                                                {
                                                    item_id = item.id,
                                                    item_name = item.name,
                                                    item_revenue = item_ops.Sum(op => op.send && op.date >= date_start && op.date <= date_end ? (int)op.amount * (int) op.price : 0)
                                                };
                        Console.WriteLine("Выручка от продаж в интервале от {0} до {1}", date_start, date_end);
                        foreach (var obj in revenue_date_interval)
                        {
                            Console.WriteLine("Товар {0} (id: {1}): {2}", obj.item_name, obj.item_id, obj.item_revenue);
                        }

                        break;
                }
            }
        }
    }
}
