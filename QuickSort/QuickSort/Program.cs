using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuickSort
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
                
                Console.Write("Sort method: a)QuickSort   b)MergeSort: ");
                string input = Console.ReadLine();
                if (input.Equals("a"))
                {
                    QuickSort(stockFile.GetStocks(), 0, stockFile.GetItemCount() - 1);
                }else if (input.Equals("b"))
                {
                    MergeSort(stockFile.GetStocks(), stockFile.GetItemCount() - 1);
                }
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

            //int[] a = { 5, 19, 10, 2, 1, 3, 2, 7, 2 };
            //int[] b = { 0, 0, 0, 0, 0, 0, 0, 0 };

            //for (int i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine(a[i]);
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Console.WriteLine(stockFile.GetStock(i).GetPrice());
            //}
            Console.Write("Please enter a file name to output: ");
            fileName = Console.ReadLine();
            FileStream toStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(toStream,Encoding.UTF8);

            fileWriter.Write(title);
            fileWriter.WriteLine();
            for (int i = 0; i < stockFile.GetItemCount(); i++)
            {
                fileWriter.Write(stockFile.GetStock(i).ToString());
                fileWriter.WriteLine();
            }
            Console.WriteLine("File output is done.");
        }

        public static void QuickSort(Stock[]a, int left, int right)
        {
            double pivot;
            int i, j;
            if (left < right)
            {
                i = left; j = right + 1;
                pivot = a[left].GetDoublePrice();
                do
                {
                    do i++; while (i < j && a[i].GetDoublePrice() < pivot);
                    do j--; while (a[j].GetDoublePrice() > pivot);
                    if (i < j) Swap(a, i, j);
                } while (i < j);
                Swap(a, left, j);
                QuickSort(a, left, j - 1);
                QuickSort(a, j + 1, right);
            }
        }

        public static void Swap(Stock[] a, int i, int j)
        {
            if (a != null)
            {
                Stock temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }
        }

        public static void rMergeSort(Stock[]a, int start, int end)
        {
            int middle;
            if (end <= start) return;
            middle = (start + end) / 2;
            rMergeSort(a, start, middle);
            rMergeSort(a, middle + 1, end);
            MergeList(a, start, middle, end);
        }

        public static void MergeList(Stock[] mergedList, int i, int m, int n)
        {
            int j = m + 1;
            int k = i;
            Stock[] initList = new Stock[mergedList.Length];
            Array.Copy(mergedList, initList, mergedList.Length);
            while (i <= m && j <= n)
            {
                if (initList[i].GetDoublePrice() <= initList[j].GetDoublePrice())
                    mergedList[k++] = initList[i++];
                else
                    mergedList[k++] = initList[j++];
            }
            if (i > m)
            {
                for (int t = j; t <= n; t++)
                    mergedList[t] = initList[t];
            }
            else
            {
                for (int t = i; t <= m; t++)
                {
                    mergedList[t] = initList[t];
                }
            }
            Console.Write("");
        }

        public static void MergeSort(Stock[]a, int n)
        {
            int s = 1;
            Stock[] extra = new Stock[n];
            for(int i = 0; i < n; i++)
            {
                extra[i] = new Stock();
            }
            while (s < n)
            {
                MergePass(a, extra, n, s);
                s *= 2;
                MergePass(extra, a, n, s);
                s *= 2;
            }
        }
        
        public static void MergePass(Stock[]initList, Stock[]mergedList, int n, int s)
        {
            int i;
            for (i = 0; i <= n - 2*s; i += 2 * s)
                Merge(initList, mergedList, i, i + s - 1, i + 2 * s - 1);
            if (i + s < n)
                Merge(initList, mergedList, i, i + s - 1, n - 1);
            else
                for (int j = i; j < n; j++)
                    mergedList[j] = initList[j];
        }

        public static void MergePass(int[] initList, int[] mergedList, int n, int s)
        {
            int i;
            for (i = 0; i <= n - 2 * s; i += 2 * s)
                Merge(initList, mergedList, i, i + s - 1, i + 2 * s - 1);
            if (i + s < n)
                Merge(initList, mergedList, i, i + s - 1, n - 1);
            else
                for (int j = i; j < n; j++)
                    mergedList[j] = initList[j];
        }

        public static void Merge(Stock[]initList, Stock[] mergedList, int i, int m, int n)
        {
            int j = m + 1;
            int k = i;
            while(i <= m && j <= n)
            {
                if (initList[i].GetDoublePrice() <= initList[j].GetDoublePrice())
                    mergedList[k++] = initList[i++];
                else
                    mergedList[k++] = initList[j++];
            }
            if (i > m)
            {
                for (int t = j; t <= n; t++)
                    mergedList[k++] = initList[t];
            }
            else
            {
                for (int t = i; t <= m; t++)
                {
                    mergedList[k++] = initList[t];
                }
            }
        }


        public static void Merge(int[] a, int[] b, int i, int m, int n)
        {
            int j = m + 1;
            int k = i;
            while (i <= m && j <= n)
            {
                if (a[i]<= a[j])
                    b[k++] = a[i++];
                else
                    b[k++] = a[j++];
            }
            if (i > m)
            {
                for (int t = j; t <= n; t++)
                    b[k++] = a[t];
            }
            else
            {
                for (int t = i; t <= m; t++)
                {
                    b[k++] = a[t];
                }
            }
        }


    }
}
