using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            StockFile stockFile = new StockFile();
            Console.Write("Please enter file name: ");
            string fileName = Console.ReadLine();
            FileStream fromStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(fromStream);
            String str = "";
            String title = fileReader.ReadLine();
            Console.WriteLine("File is loading...");
            while ((str = fileReader.ReadLine()) != null)
            {
                string[] s = str.Split(',');
                if (stockFile.StockExists(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7]) != true)
                    stockFile.AddStock(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7]);
            }
            fileReader.Close();

            if (stockFile.GetItemCount() > 0)
            {
                HeapSort(stockFile.GetStocks(), stockFile.GetItemCount());
                Console.Write("Please enter a file name to output: ");
                fileName = Console.ReadLine();
                FileStream toStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(toStream, Encoding.UTF8);

                fileWriter.Write(title);
                fileWriter.WriteLine();
                for (int i = 0; i < stockFile.GetItemCount(); i++)
                {
                    fileWriter.Write(stockFile.GetStock(i).ToString());
                    fileWriter.WriteLine();
                }
                Console.WriteLine("File output is done.");

            }
            else
            {
                Console.WriteLine("No data in file.");
            }

            //int count = 0;
            //while (count < 50)
            //{
            //    str = fileReader.ReadLine();
            //    string[] s = str.Split(',');
            //    if (stockFile.StockExists(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7]) != true)
            //        stockFile.AddStock(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7]);
            //    count++;
            //}
            //rMergeSort(stockFile.GetStocks(), 0, stockFile.GetItemCount()-1);

            //int[] a = { 5, 19, 10, 2, 1, 3, 2, 7, 2,4 };
            //HeapSort(a, 10);

            //for (int i = 0; i < a.Length; i++)
            //{
            //    Console.Write(a[i] + " ");
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Console.WriteLine(stockFile.GetStock(i).GetPrice());
            //}

        }

        public static void HeapSort(Stock[]a, int n)
        {
            for (int i = (n-2)/2; i >= 0; i--)
            {
                Adjust(a, i, n);
            }
            for(int j = n - 2; j >= 0; j--)
            {
                Swap(a, 0, j + 1);
                Adjust(a, 0, j + 1);
            }
        }

        public static void Adjust(Stock[]a, int root, int n)
        {
            int child;
            Stock temp = a[root];
            child = 2 * root + 1;
            while(child < n)
            {
                if (    (child < n-1)   &&  (a[child].GetDoublePrice() < a[child+1].GetDoublePrice()))
                    child++;
                if (temp.GetDoublePrice() > a[child].GetDoublePrice())
                    break;
                else
                {
                    a[(child - 1) / 2] = a[child];
                    child = 2 * child + 1;
                }
            }
            a[(child - 1) / 2] = temp;
        }

        public static void Swap(Stock[] a, int i, int j)
        {
            Stock temp = a[j];
            a[j] = a[i];
            a[i] = temp;
        }
    }
}
