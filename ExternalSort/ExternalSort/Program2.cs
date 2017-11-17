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
            //Divide data into memory accessable data size
            int count = 0, divide = 0, fileLevel = 0, totalData = 0, dataSize;
            Console.Write("Please enter fileName: ");
            string sourceFile = Console.ReadLine();
            Console.Write("Please enter dataSize: ");
            string input = Console.ReadLine();
            bool result = Int32.TryParse(input, out dataSize);

            sw.Reset();
            sw.Start();
            string[,] PassFile = new string[1000, 1000];
            FileStream fromFile1 = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            StreamReader fileReader1 = new StreamReader(fromFile1);
            FileStream fromFile2; StreamReader fileReader2; FileStream toFile; StreamWriter fileWriter;
            string title = fileReader1.ReadLine();
            string str = fileReader1.ReadLine();
            string[] strBuffer = new string[dataSize];
            //Using HeapSort to sort the divid data
            while (true)
            {
                try
                {
                    PassFile[fileLevel, divide] = sourceFile + divide + ".csv";
                    toFile = new FileStream(PassFile[fileLevel, divide++], FileMode.Create, FileAccess.Write);
                    fileWriter = new StreamWriter(toFile, Encoding.UTF8);
                    //if (divide >= PassFile[0].Length)
                    while ((str != null) && (count < strBuffer.Length))
                    {
                        strBuffer[count++] = str;
                        str = fileReader1.ReadLine();
                    }
                    //HeapSort(strBuffer, count);
                    QuickSort(strBuffer, 0, count-1);
                    for (int i = 0; i < count; i++)
                    {
                        fileWriter.WriteLine(strBuffer[i]);
                        totalData++;
                    }
                    fileWriter.Flush();
                    fileWriter.Close();
                    toFile.Close();

                }
                catch
                {
                    Console.WriteLine("IO exception");
                }
                if (str == null)
                {
                    strBuffer = null;
                    break;
                }
                else
                    count = 0;
            }
            fileReader1.Close();
            fromFile1.Close();
            sw.Stop();
            Console.WriteLine("Stopwatch Method(Split the file): {0} ms", sw.Elapsed.TotalMilliseconds);




            sw.Reset();
            sw.Start();

            //Merge sublength data 
            int newDataSize = dataSize / 2;
            double d = divide;
            int mergeTime = (int)Math.Log(d, 2);
            if (newDataSize < 1)
                newDataSize = 1;
            string[] strBuffer1 = new string[newDataSize]; string[] strBuffer2 = new string[newDataSize];
            string targetFile = "";
            strBuffer = new string[dataSize];
            int fileCount = divide, pass = 1;
            //while (pass * dataSize < totalData)
            while (true)
            {
                int file = 0, s;
                fileLevel++;
                for (s = 0; s <= fileCount - 2; s += 2)
                {
                    int writeTimes = 0;
                    fromFile1 = new FileStream(PassFile[fileLevel - 1, s], FileMode.Open, FileAccess.Read); fileReader1 = new StreamReader(fromFile1);
                    fromFile2 = new FileStream(PassFile[fileLevel - 1, s + 1], FileMode.Open, FileAccess.Read); fileReader2 = new StreamReader(fromFile2);
                    if (fileCount == 2)     //output file
                    {
                        targetFile = sourceFile + "_Output.csv";
                        toFile = new FileStream(targetFile, FileMode.Create, FileAccess.Write); fileWriter = new StreamWriter(toFile, Encoding.UTF8);
                        fileWriter.WriteLine(title);
                    }
                    else
                    {
                        PassFile[fileLevel, file] = PassFile[fileLevel - 1, s] + "Pass" + pass + ".csv";
                        toFile = new FileStream(PassFile[fileLevel, file++], FileMode.Create, FileAccess.Write); fileWriter = new StreamWriter(toFile, Encoding.UTF8);
                    }
                    int m = ReadFile(fileReader1, strBuffer1);          //Read newDataSize amount of data m into string array strBuffer1
                    int n = ReadFile(fileReader2, strBuffer2);          //Read newDataSize amount of data n into string array strBuffer2
                    int i = 0, j = 0, k = 0;
                    while (true)
                    {
                        // when current read index i and j are within data size strBuffer1 m and strBuffer2 n
                        while (i < m && j < n && k < dataSize)
                        {
                            if (FindTime(strBuffer1[i]) < FindTime(strBuffer2[j]))  //Compare i and j
                            {
                                strBuffer[k++] = strBuffer1[i];
                                i++;
                            }
                            else
                            {
                                strBuffer[k++] = strBuffer2[j];
                                j++;
                            }

                            if (i >= m)   //When all of the data in strBuffer1 are read
                            {
                                //strBuffer1 = null;
                                //strBuffer1 = new string[newDataSize];
                                m = ReadFile(fileReader1, strBuffer1);  //Read the file from fileReader1 and update into strBuffer1, return data size to m
                                i = 0;
                            }
                            else if (j >= n)   //When all of the data in strBuffer2 are read
                            {
                                //strBuffer2 = null;
                                //strBuffer2 = new string[newDataSize];
                                n = ReadFile(fileReader2, strBuffer2);  //Read the file from fileReader1 and update into strBuffer1, return data size to m
                                j = 0;
                            }
                        }
                        //Write strBuffer data into file and count writeTimes
                        if (k >= dataSize || (m == 0 && n == 0))
                        {
                            for (int t = 0; t < k; t++)
                                fileWriter.WriteLine(strBuffer[t]);
                            writeTimes++;
                            if (writeTimes >= Math.Pow(2, pass) || (m == 0 && n == 0))
                            {
                                //strBuffer = null;
                                //strBuffer = new string[dataSize];
                                k = 0;
                                break;
                            }
                            else
                            {
                                //strBuffer = null;
                                //strBuffer = new string[dataSize];
                                k = 0;
                            }
                        }
                        else if (m == 0)        //When all of the data from fileReader1 are read already
                        {
                            while (k < dataSize && n != 0)
                            {
                                if (j >= n)
                                {
                                    //strBuffer2 = null;
                                    //strBuffer2 = new string[newDataSize];
                                    n = ReadFile(fileReader2, strBuffer2);
                                    j = 0;
                                }
                                else
                                {
                                    strBuffer[k++] = strBuffer2[j];
                                    j++;
                                }
                            }
                        }
                        else                //When all of the data from fileReader2 are read already
                        {
                            while (k < dataSize && m != 0)
                            {
                                if (i >= m)
                                {
                                    //strBuffer1 = null;
                                    //strBuffer1 = new string[newDataSize];
                                    m = ReadFile(fileReader1, strBuffer1);
                                    i = 0;
                                }
                                else
                                {
                                    strBuffer[k++] = strBuffer1[i];
                                    i++;
                                }
                            }
                        }
                    }
                    fileWriter.Close();
                    toFile.Close();
                }
                //fill in the rest of data
                //fromFile1 = null; fileReader1 = null; fromFile2 = null; fileReader2 = null; toFile = null; fileWriter = null; strBuffer2 = null; 
                strBuffer2 = new string[newDataSize];
                if (file * 2 != fileCount && file != 0)
                {
                    fromFile1 = new FileStream(PassFile[fileLevel - 1, s], FileMode.Open, FileAccess.Read);
                    fileReader1 = new StreamReader(fromFile1);
                    PassFile[fileLevel, file] = PassFile[fileLevel - 1, s] + "Pass" + pass + ".csv";
                    toFile = new FileStream(PassFile[fileLevel, file++], FileMode.Create, FileAccess.Write);
                    fileWriter = new StreamWriter(toFile, Encoding.UTF8);
                    //strBuffer1 = null;
                    strBuffer1 = new string[newDataSize];
                    int m = ReadFile(fileReader1, strBuffer1);          //Read newDataSize amount of data into string array strBuffer1
                    int i = 0, k = 0;
                    while (true)
                    {
                        while (k < dataSize && (m != 0))
                        {
                            if (i >= m)
                            {
                                m = ReadFile(fileReader1, strBuffer1);
                                i = 0;
                            }
                            else
                            {
                                strBuffer[k++] = strBuffer1[i];
                                i++;
                            }
                        }
                        if (k >= dataSize || m == 0)
                        {

                            for (int t = 0; t < k; t++)
                                fileWriter.WriteLine(strBuffer[t]);
                            k = 0;
                            if (m == 0)
                                break;

                        }
                    }
                    fileWriter.Close();
                    toFile.Close();
                }
                pass *= 2;
                fileCount = file;
                if (fileCount == 0)
                    break;
            }
            sw.Stop();
            Console.WriteLine("Stopwatch Method(sorting): {0} ms", sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Please see the sorting file named as " + targetFile);
        }

        private static Array ResizeArray(Array arr, int[] newSizes)
        {
            if (newSizes.Length != arr.Rank)
                throw new ArgumentException("arr must have the same number of dimensions " +
                                            "as there are elements in newSizes", "newSizes");

            var temp = Array.CreateInstance(arr.GetType().GetElementType(), newSizes);
            int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
            Array.ConstrainedCopy(arr, 0, temp, 0, length);
            return temp;
        }
        private static int ReadFile(StreamReader fileReader, string[] strBuffer)
        {
            string str;

            int count = 0;
            while ((count < strBuffer.Length))
            {
                if ((str = fileReader.ReadLine()) != null)
                    strBuffer[count++] = str;
                else
                    return count;

            }
            return count;
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
