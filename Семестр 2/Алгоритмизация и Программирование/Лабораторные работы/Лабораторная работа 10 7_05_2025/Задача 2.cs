using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class ItemAmount
    {
        public string name { get; set; }
        public uint amount { get; set; }

        public ItemAmount(string name, uint amount)
        {
            this.name = name;
            this.amount = amount;
        }
    }

    class Item
    {
        public uint id { get; set; }
        public string name { get; set; }

        public Item(uint id, string name)
        {
            this.id = id;
            this.name = name;
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
        public bool sell { get; set; }
        public uint amount { get; set; }
        public uint price { get; set; }
        public string date { get; set; }

        public Operation(uint item_id, uint supplier_id, uint amount, uint price, string date)
        {
            this.item_id = item_id;
            this.supplier_id = supplier_id;
            sell = false;
            this.amount = amount;
            this.price = price;
            this.date = date;
        }

        public Operation(uint item_id, uint amount, uint price, string date)
        {
            this.item_id = item_id;
            supplier_id = 0;
            sell = true;
            this.amount = amount;
            this.price = price;
            this.date = date;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Item[] items = {
                new Item(2131, "Сосиски"),
                new Item(2243, "Курица"),
                new Item(6765, "Телефоны"),
                new Item(6981, "Телевизоры")
            };
            Supplier[] suppliers = {
                new Supplier(21, "Ферма"),
                new Supplier(68, "Завод электроники")
            };

            Operation[] operations = {
                new Operation(items[0].id, suppliers[0].id, 15, 100, "01.01.2000"),
                new Operation(items[0].id, 8, 160, "02.01.2000"),
                new Operation(items[1].id, suppliers[0].id, 11, 200, "02.01.2000"),
                new Operation(items[1].id, 11, 350, "05.01.2000"),
                new Operation(items[2].id, suppliers[1].id, 5, 5000, "15.01.2000"),
                new Operation(items[2].id, 2, 7500, "17.01.2000"),
                new Operation(items[3].id, suppliers[1].id, 2, 30000, "15.01.2000"),
                new Operation(items[3].id, 1, 52000, "17.01.2000")
            };

            int option = 0;
            int len = operations.Length;

            while (option < 6)
            {
                Console.WriteLine("1. Остатки по каждому товару\n" +
                "2. Поставки товаров, сгрупированные по поставщику\n" +
                "3. Продажи товаров, сгрупированные по дате\n" +
                "4. Выдать выручку от продаж\n" +
                "5. Выдать поставщика, который поставил максимальное количество товара (в штуках)\n" +
                "6. Выход");
                Console.Write("Выберите опцию: ");
                option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

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
                                          item_remains = item_ops.Sum(op => op.sell ? -(int)op.amount : (int)op.amount)
                                      };

                        foreach (var obj in remains)
                            Console.WriteLine("Остатки товара {0} (id: {1}): {2}", obj.item_name, obj.item_id, obj.item_remains);
                        Console.WriteLine();

                        break;

                    case 2:
                        var oper_buy_by_sup = from sup in suppliers
                                              join op in operations
                                              on sup.id equals op.supplier_id
                                              into sup_ops
                                              select new
                                              {
                                                  sup_id = sup.id,
                                                  sup_name = sup.name,
                                                  op_buy = sup_ops.GroupBy(op => op.supplier_id)
                                              };

                        foreach (var oper_list in oper_buy_by_sup)
                        {
                            Console.WriteLine("Поставки, сгрупированные по поставщику: {0} (ID: {1})", oper_list.sup_name, oper_list.sup_id);
                            foreach (var opers in oper_list.op_buy)
                                foreach(Operation oper in opers)
                                    Console.WriteLine("{0}, {1}, {2}, {3}", oper.item_id, oper.amount, oper.price, oper.date);
                            Console.WriteLine();
                        }
                    
                        break;
                    
                    case 3:
                        var oper_sell_by_date = from op in operations
                                                where op.sell
                                                group op by op.date;

                        foreach (var opers in oper_sell_by_date)
                            {
                                Console.WriteLine("Продажи, сгрупированные по дате: " + opers.Key);
                                foreach (Operation oper in opers)
                                    Console.Write("{0}, {1}, {2}, {3}\n", oper.item_id, oper.supplier_id, oper.amount, oper.price);
                                Console.WriteLine();
                            }

                        break;

                    case 4:
                        var revenue = from item in items
                                      join op in operations
                                      on item.id equals op.item_id
                                      into item_ops
                                      select new
                                      {
                                          item_id = item.id,
                                          item_name = item.name,
                                          item_revenue = item_ops.Sum(op => op.sell ? (int)op.amount * (int)op.price : 0)
                                      };

                        foreach (var obj in revenue) 
                            Console.WriteLine("Выручка от продаж от товара {0} (id: {1}): {2}", obj.item_name, obj.item_id, obj.item_revenue);
                        Console.WriteLine();

                        break;

                    case 5:
                        var max_sup = (from sup in suppliers
                                      join op in operations
                                      on sup.id equals op.supplier_id
                                      into sup_ops
                                      select new
                                      {
                                          sup_id = sup.id,
                                          sup_name = sup.name,
                                          sup_amount = sup_ops.Sum(op => op.amount)
                                      }).OrderByDescending(s => s.sup_amount).FirstOrDefault();

                        Console.WriteLine("Поставщик, который поставил максимальное количество товаров: {0} (id: {1}): {2}", max_sup.sup_name, max_sup.sup_id, max_sup.sup_amount);
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
