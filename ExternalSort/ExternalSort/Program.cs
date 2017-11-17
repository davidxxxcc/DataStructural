using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace ExternalSort
{
    class Program
    {
        static void Main(string[] args)
        {   
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            int count = 0, pass = 0;
            int dataSize = 30, totalData = 0, dataGroup = 0;
            string sourceFile = "timetable_20.csv";
            string dividedFile = "timetable_divid.csv";
            string[] passFile = new string[10];
            //string[] tempFileArr = new string[10];
            //tempFileArr[fileCount] = "temp_" + fileCount.ToString() + ".csv";
            FileStream fromFile = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(fromFile);
            FileStream toFile = new FileStream(dividedFile, FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(toFile, Encoding.UTF8);
            
            string title = fileReader.ReadLine();
            string str = fileReader.ReadLine();
            string[] strBuffer = new string[dataSize];

            //Using HeapSort to sort the divid data
            while (true)
            {
                try
                {
                    while ((str != null) && (count < strBuffer.Length))
                    {
                        strBuffer[count++] = str;
                        str = fileReader.ReadLine();
                    }
                    HeapSort(strBuffer, count);
                    //QuickSort(strBuffer, 0, strBuffer.Length -1);
                    for (int i = 0; i < count; i++)
                    {
                        fileWriter.WriteLine(strBuffer[i]);
                        totalData++;
                    }
                    dataGroup++;
                    fileWriter.Flush();

                    if (str == null)
                    {
                        strBuffer = null;
                        fileWriter.Close();
                        toFile.Close();
                        break;
                    }
                    else
                    {
                        count = 0; 
                    }

                }
                catch
                {
                    Console.WriteLine("IO exception");
                }
            }
            
            fromFile = null; fileReader = null;
            if (pass == 0)
                fromFile = new FileStream(dividedFile, FileMode.Open, FileAccess.Read);
            else
            {
                passFile[pass] = "timetablePass" + pass;
                fromFile = new FileStream(passFile[pass], FileMode.Open, FileAccess.Read);
            }
            fileReader = new StreamReader(fromFile);

            int newDataSize = dataSize / 2;       
            if (newDataSize < 1)
                newDataSize = 1;
            string[] strBuffer1 = new string[newDataSize];
            string[] strBuffer2 = new string[newDataSize];

            //Using MergeSort to combine data
            int newDataSize = dataSize / (dataGroup + 1);       // +1 for one buffer memory
            if (newDataSize < 1)
                newDataSize = 1;
            int run = 0;
            int totalRun = dataSize/newDataSize;
            while (run < totalRun)
            {
                strBuffer = new string[dataSize];
                toFile = null; fileWriter = null;
                toFile = new FileStream(targetFile, FileMode.Append, FileAccess.Write);
                fileWriter = new StreamWriter(toFile, Encoding.UTF8);
                fromFile = null; fileReader = null;
                fromFile = new FileStream(dividedFile, FileMode.Open, FileAccess.Read);
                fileReader = new StreamReader(fromFile);
                count = 0;
                int groupCount = 0, index = 0;
                while (groupCount <= dataGroup && index <= totalData)
                {
                    while (index < dataSize * groupCount + newDataSize * run && index < totalData)            //Read over the previous processed data
                    {
                        fileReader.ReadLine();
                        index++;
                    }
                    while (index >= dataSize * groupCount + newDataSize * run && index < dataSize * groupCount + newDataSize * run + newDataSize && index < totalData)   //Write data into strBuffer[]
                    {
                        strBuffer[count++] = fileReader.ReadLine();
                        index++;
                    }
                    if(true)
                    {
                        while (index >= dataSize * groupCount + newDataSize * run + newDataSize && index < dataSize * (groupCount + 1) && index < totalData)     //Read over the unprocessed data
                        {
                            fileReader.ReadLine();
                            index++;
                        }
                        groupCount++;
                    }
                }
                fileReader.Close(); fromFile.Close();

                HeapSort(strBuffer, count);
                //MergeSort(strBuffer, dataSize, newDataSize);
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        fileWriter.WriteLine(strBuffer[i]);
                    }
                }
                catch
                {
                    Console.WriteLine("FileWriter error!");
                }
                fileWriter.Close(); toFile.Close();
                run++;
            }

            sw.Stop();
            Console.WriteLine("Stopwatch Method: {0} ms", sw.Elapsed.TotalMilliseconds);
        }



        public static void MergeSort(String[] a, int n, int dataLength)
        {
            int s = dataLength;
            String[] extra = new String[n];

            while (s < n)
            {
                MergePass(a, extra, n, s);
                s *= 2;
                MergePass(extra, a, n, s);
                s *= 2;
            }
        }

        public static void MergePass(string[] initList, string[] mergedList, int n, int s)
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


        public static void Merge(string[] initList, string[] mergedList, int i, int m, int n)
        {
            int j = m + 1;
            int k = i;
            while (i <= m && j <= n)
            {
                if (FindTime(initList[i]) <= FindTime(initList[j]))
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

        public static void HeapSort(string[] a, int n)
        {
            for (int i = (n - 2) / 2; i >= 0; i--)
            {
                Adjust(a, i, n);
            }
            for (int j = n - 2; j >= 0; j--)
            {
                Swap(a, 0, j + 1);
                Adjust(a, 0, j + 1);
            }
        }

        public static void Adjust(string[] a, int root, int n)
        {
            int child;
            string temp = a[root];
            child = 2 * root + 1;
            while (child < n)
            {
                if ((child < n - 1) && (FindTime(a[child]) < FindTime(a[child + 1])))
                    child++;
                if (FindTime(temp) > FindTime(a[child]))
                    break;
                else
                {
                    a[(child - 1) / 2] = a[child];
                    child = 2 * child + 1;
                }
            }
            a[(child - 1) / 2] = temp;
        }

        public static void Swap(string[] a, int i, int j)
        {
            string temp = a[j];
            a[j] = a[i];
            a[i] = temp;
        }

        private static int FindTime(string str)
        {
            string[] s = str.Split(',');
            int number;
            bool result = Int32.TryParse(s[7], out number);
            if (result)
                return number;
            return -1;
        }

        public static void QuickSort(string[] a, int left, int right)
        {
            double pivot;
            int i, j;
            if (left < right)
            {
                i = left; j = right + 1;
                pivot = FindTime(a[left]);
                do
                {
                    do i++; while (i < j && FindTime(a[i]) < pivot);
                    do j--; while (FindTime(a[j]) > pivot);
                    if (i < j) Swap(a, i, j);
                } while (i < j);
                Swap(a, left, j);
                QuickSort(a, left, j - 1);
                QuickSort(a, j + 1, right);
            }
        }
    }
}
