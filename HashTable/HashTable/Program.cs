using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {   
        enum Status { MENU, INSERT, SEARCH, QUIT };
        static HashTable ht = new HashTable(41);
        static void Main(string[] args)
        {

            Status status = Status.MENU;
            string input;
            while (true)
            {
                if (status == Status.MENU)
                {
                    
                    status = ChooseOption();
                }
                else if (status == Status.INSERT)
                {
                    Console.Write("Sentence: ");
                    input = Console.ReadLine();
                    input.TrimEnd();
                    char[] s = { ' ', ',', '.', '/','?' };
                    string[] inputS = input.Split(s);
                    if (inputS[inputS.Length - 1] == "")
                        Array.Resize(ref inputS, inputS.Length - 1);
                    if (inputS.Length > 0)
                        LinearHash(inputS);
                    Console.Write("\nDone!");
                    status = Status.MENU;
                }
                else if (status == Status.SEARCH)
                {
                    Console.Write("Query: ");
                    input = Console.ReadLine();
                    if (ht.ElementSearch(input) != null)
                        Console.WriteLine("'" + input + "' is found!");
                    else
                        Console.WriteLine("'" + input + "' is not found!");
                    status = Status.MENU;
                }
                else if (status == Status.QUIT)
                {
                    break;
                }
            }
        }

        private static void LinearHash(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                ht.ElementInsertion(str[i]);
            }
        }

        private static Status ChooseOption()
        {
            while (true)
            {
                Console.Write("1) Insert. 2) Search. -1) Quit:");
                string input = Console.ReadLine();
                if (input.Equals("1"))
                    return Status.INSERT;
                else if (input.Equals("2"))
                    return Status.SEARCH;
                else if (input.Equals("-1"))
                    return Status.QUIT;
                else
                    Console.WriteLine("Input error!");
            }
        }
    }
}
