using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoublyLinkedList
{
    class Program
    {
        enum Option { MENU, NEXT, LAST, COM, QUIT };
        
        static void Main(string[] args)
        {
            bool run = true;
            int option = (int)Option.COM;
            DoublyLinkedList lists = new DoublyLinkedList();
            while (run)
            {
                switch(option)
                {
                    case (int)Option.MENU:
                        Console.Write("1. Next Step, 2. Last Step, 3. Compile, -1. Quit: ");
                        string str = Console.ReadLine();
                        int number;
                        bool result = Int32.TryParse(str, out number);
                        if (result)
                        {
                            if (number == 1 || number == 2 || number == 3 || number == -1)
                            {
                                option = number;
                            }
                            else
                            {
                                Console.WriteLine("請輸入數字");
                            }
                        }
                        break;
                    case (int)Option.NEXT:
                        if (lists.Next() == true)
                        {
                            lists.MoveCurNext();
                            Console.WriteLine("After: " + lists.GetCurrent().GetCommand() + " " + lists.GetCurrent().GetArgs() + ";");
                            Console.WriteLine("Value: " + lists.GetCurrent().GetTemp());
                        }
                        else
                        {
                            Console.WriteLine("End of Code");
                            Console.WriteLine("Value: " + lists.GetCurrent().GetTemp());
                        }
                        option = (int)Option.MENU;
                        break;
                    case (int)Option.LAST:
                        if (lists.Last() == true)
                        {
                            Console.WriteLine("Before: " + lists.GetCurrent().GetCommand() + " " + lists.GetCurrent().GetArgs() + ";" );
                            lists.MoveCurLast();
                            Console.WriteLine("Value: " + lists.GetCurrent().GetTemp());
                        }
                        else
                        {
                            Console.WriteLine("Begining of Code");
                            //Console.WriteLine("First code: " + lists.GetCurrent().GetCommand() + "\t" + lists.GetCurrent().GetArgs()) ;
                        }
                        option = (int)Option.MENU;
                        break;
                    case (int)Option.COM:
                        lists = new DoublyLinkedList();
                        Compile(lists);
                        option = (int)Option.MENU;
                        break;
                    case (int)Option.QUIT:
                        run = false;
                        break;
                }
            }

        }

        private static void Compile(DoublyLinkedList lists)
        {
            bool show = true;
            Console.WriteLine("Command: ");
            string cmd = Console.ReadLine();
            string[] cmdSplit = cmd.Split(' ');
            int results = 0;
            if (cmdSplit[0].Equals("T") && cmdSplit.Length == 2)
            {
                char[] cmdToCharArray = cmdSplit[1].ToCharArray();
                int n = cmdToCharArray.Length;
                if (cmdToCharArray[n - 1] == 't' || cmdToCharArray[n - 2] == '.')
                {
                    string sourceFile = cmdSplit[1];    //指令來源檔案
                    try
                    {
                        FileStream fileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                        StreamReader streamReader = new StreamReader(fileStream);
                        string str, param;
                        int temp = 0;
                        while ((str = streamReader.ReadLine()) != null)
                        {
                            CommandParser p = new CommandParser(str);   //建立CommandParser的物件參考p
                            String commanLine = p.getCommandLine(); //傳入字串的頭尾空白去掉，並將英文轉大寫字母

                            //判斷字串是否合法
                            if (p.parser(commanLine) == Command.LOAD)
                            {
                                param = p.getArgs();
                                int number;
                                bool result = Int32.TryParse(param, out number);
                                if (result)
                                {
                                    temp = number;
                                    lists.Add("LOAD", param, temp);
                                }
                                else
                                {
                                    Console.WriteLine("Syntax error!");
                                }
                            }
                            else if (p.parser(commanLine) == Command.ADD)
                            {
                                param = p.getArgs();
                                int number;
                                bool result = Int32.TryParse(param, out number);
                                if (result)
                                {
                                    temp += number;
                                    lists.Add("ADD", param, temp);
                                }
                                else
                                {
                                    Console.WriteLine("Syntax error!");

                                }
                            }
                            else if (p.parser(commanLine) == Command.PRT)
                            {
                                lists.Add("PRT","",temp);
                                Console.WriteLine(temp);
                            }
                            else
                            {
                                Console.WriteLine("Syntax error!");
                                show = false;
                                break;
                            }
                        }
                        results = temp;
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine("Files are not found!");
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Files error!");
                    }
                }
                else
                {
                    Console.WriteLine("Command error!");
                }

            }
            else
            {
                Console.WriteLine("Command error!");
            }

            if (show == true)
                Console.WriteLine("Results: " + results);
        }
    }


}
