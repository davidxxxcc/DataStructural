using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkList
{
    class Program
    {
        enum Options { NONE, ADD, OUTPUT, INSERT, DELETE, QUIT, MENU};
        static int option = (int)Options.ADD;
        static LinkedList lists = new LinkedList();
        static void Main(string[] args)
        {
            while(true)
            {
                if (option == (int)Options.MENU)
                {
                    Console.Write("Option: 1) Add. 2) Output. 3) Insert. 4) Delete. -1) Quit: ");
                    string inputS = Console.ReadLine();
                    option = Menu(inputS);
                }
                else if (option == (int)Options.ADD)
                {
                    option = AddData();
                    continue;
                }
                else if (option == (int)Options.OUTPUT)
                {
                    option = OutputData();
                    continue;
                }
                else if (option == (int)Options.INSERT)
                {
                    option = InsertionData();
                    continue;
                }
                else if (option == (int)Options.DELETE)
                {
                    option = DeleteData();
                    continue;
                }
                else if (option == (int)Options.QUIT)
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                else
                {
                    Console.WriteLine("輸入錯誤!");
                    continue;
                }
            }
        }

        private static int Menu( string inputS)
        {
            int input;
            bool result = Int32.TryParse(inputS, out input);
            if (result)
            {
                switch (input)
                {
                    case 1:
                        return (int)Options.ADD;
                    case 2:
                        return (int)Options.OUTPUT;
                    case 3:
                        return  (int)Options.INSERT;
                    case 4:
                        return (int)Options.DELETE;
                    case -1:
                        return (int)Options.QUIT;
                    default:
                        Console.WriteLine("Input error!");
                        return (int)Options.MENU;
                }
            }
            else
            {
                Console.WriteLine("Input error!");
                return (int)Options.MENU;
            }
        }

        private static int AddData()
        {
            while (true)
            {
                int[] num = new int[3];
                Console.Write("Insert new data (SN/ENGLISH/MATH): ");
                string input = Console.ReadLine();
                string[] inputSlipt = input.Split(' ');
                if (inputSlipt.Length != 3)
                {
                    Console.WriteLine("Please enter 3 integer number!");
                    continue;
                }
                else
                {
                    int[] number = new int[3];
                    bool result1 = Int32.TryParse(inputSlipt[0], out number[0]);
                    bool result2 = Int32.TryParse(inputSlipt[1], out number[1]);
                    bool result3 = Int32.TryParse(inputSlipt[2], out number[2]);
                    if (result1 == true && result2 == true && result3 == true)
                    {
                        num[0] = number[0];
                        num[1] = number[1];
                        num[2] = number[2];
                    }
                    else
                    {
                        Console.WriteLine("Please enter integer number!");
                        continue;
                    }

                    if (num[0] == -1 && num[1] == -1 && num[2] == -1)
                    {
                        return (int)Options.MENU;
                    }

                    if ( (num[1] < 0 || num[1] > 100) || (num[2] < 0 || num[2] > 100))
                    {
                        Console.WriteLine("Grade need to be within 0-100! ");
                        continue;
                    }

                    if (lists.IsRepeat(num[0]) == true)
                    {
                        Console.WriteLine("SN is already exist!");
                        continue;
                    }
                    else
                    {
                        lists.InsertionOrdered(num[0], num[1], num[2]);
                        return (int)Options.ADD;
                    }
                }
            }
        }


        private static int InsertionData()
        {
            while (true)
            {
                int[] num = new int[3];
                Console.Write("Insert new data (SN/ENGLISH/MATH): ");
                string input = Console.ReadLine();
                string[] inputSlipt = input.Split(' ');
                if (inputSlipt.Length != 3)
                {
                    Console.WriteLine("Please enter 3 integer number!");
                    continue;
                }
                else
                {
                    int[]number = new int[3];
                    bool result1 = Int32.TryParse(inputSlipt[0], out number[0]);
                    bool result2 = Int32.TryParse(inputSlipt[1], out number[1]);
                    bool result3 = Int32.TryParse(inputSlipt[2], out number[2]);
                    if (result1 == true && result2 == true && result3 == true)
                    {
                        num[0] = number[0];
                        num[1] = number[1];
                        num[2] = number[2];
                    }
                    else
                    {
                        Console.WriteLine("Please enter integer number!");
                        continue;
                    }

                    if (num[0] == -1 && num[1] == -1 && num[2] == -1)
                    {
                        return (int)Options.MENU;
                    }

                    if (lists.IsRepeat(num[0]) == true)
                    {
                        Console.WriteLine("SN is already exist!");
                        continue;
                    }
                    else
                    {
                        lists.InsertionOrdered(num[0], num[1], num[2]);
                        return (int)Options.MENU;
                    }
                }
            }
        }

        private static int DeleteData()
        {
            Console.Write("Enter SN to delete data: ");
            string inputS = Console.ReadLine();
            int sn;
            bool result = Int32.TryParse(inputS, out sn);
            if (result)
            {
                if (lists.IsRepeat(sn) == true)
                {
                    lists.Delete(sn);
                    return (int)Options.MENU;
                }
                else
                {
                    Console.WriteLine("SN does not exist!");
                    return (int)Options.DELETE;
                }
            }
            else
            {
                Console.WriteLine("Please enter integer number!");
                return (int)Options.DELETE;
            }
        }

        
        private static int OutputData()
        {
            if (lists.IsEmpty())
            {
                Console.WriteLine("No data to output! ");
                return (int)Options.MENU;
            }
            else
            {
                string title = "SN\tENG\tMATH\tAVG\r\n";
                string line = "----------------------------------------------------------\r\n";
                string data = lists.Output();
                Console.WriteLine(title + line + data);
                return (int)Options.MENU;
            }
        }
        
    }


}
