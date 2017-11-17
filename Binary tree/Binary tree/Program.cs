using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_tree
{
    class Program
    {
        enum Options { MENU, ADD, DELETE, SHOW, SHOWALL, EXIT };
        static void Main(string[] args)
        {
            Options option = Options.MENU;
            bool run = true;
            BinaryTree bt = new BinaryTree();
            while (run)
            {
                string input, name, phone;
                string[] inputS;
                switch (option)
                {
                    case Options.MENU:
                        Console.Write("i) Add contacts d) Delete contacts f) Show certain contacts l) Show all contacts q) Exit  :");
                        input = Console.ReadLine();
                        if (input.Equals("i"))
                            option = Options.ADD;
                        else if (input.Equals("d"))
                            option = Options.DELETE;
                        else if (input.Equals("f"))
                            option = Options.SHOW;
                        else if (input.Equals("l"))
                            option = Options.SHOWALL;
                        else if (input.Equals("q"))
                            option = Options.EXIT;
                        break;
                    case Options.ADD:
                        Console.Write("Please enter name and phone number (-1 to leave): ");
                        input = Console.ReadLine();
                        if (getPhoneNumber(input) == -1)
                            option = Options.MENU;
                        else
                        {
                            inputS = input.Split(' ');
                            name = inputS[0];
                            phone = inputS[1];
                        if (bt.SearchNode(bt.GetRoot(), name) == null)
                            {
                                bt.InsertNode(bt.GetRoot(), name, getPhoneNumber(phone));
                            }
                            else
                                Console.WriteLine("Name exist already!");
                        }
                        break;
                    case Options.DELETE:
                        Console.Write("Please enter name to delete: ");
                        name = Console.ReadLine();
                        if (bt.DeleteNode(name) == true)
                        {
                            Console.WriteLine("Delete " + name + " sccessfully!");
                            option = Options.MENU;
                        }
                        else
                            Console.WriteLine("Can not find name.");
                        break;
                    case Options.SHOW:
                        Console.Write("Please enter name to search: ");
                        name = Console.ReadLine();
                        TreeNode temp = bt.SearchNode(bt.GetRoot(), name);
                        if (temp == null)
                        {
                            Console.WriteLine("Can not find name.");
                        }
                        else
                        {
                            Console.WriteLine(temp.GetNodeName() + "\t" + temp.GetPhone());
                            option = Options.MENU;
                        }
                        break;
                    case Options.SHOWALL:
                        bt.PrintInOrder(bt.GetRoot());
                        option = Options.MENU;
                        break;
                    case Options.EXIT:
                        run = false;
                        break;
                }
            }

        }

        public static int getPhoneNumber(string phone)
        {
            int number;
            bool result = Int32.TryParse(phone, out number);
            if (result)
                return number;
            else
                return 0;
        }
    }
}
