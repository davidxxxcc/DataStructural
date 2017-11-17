using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace StackMaze
{
    //包含一個二維陣列紀錄迷宮的內容，並將相關成員與方法寫於此類別中
    //例如，迷宮的尺寸以及一個用來標示路徑的陣列
    //相關的方法有初始化迷宮內容、標印出迷宮內容等等
    class Maze
    {
        int count;
        int[][,] maze;
        int[][,] mark;
        string[] path;
        public Maze()
        {
            count = 0;
            maze = new int[10][,];
            mark = new int[10][,];
            path = new string[10];
        }

        public int GetCount()
        {
            return this.count;
        }
        public int[,] GetMark(int count)
        {
            return mark[count];
        }

        public void SetMark(int count, int row, int col, int m)
        {
            mark[count][row, col] = m;
        }
        public int[,] GetMaze(int count)
        {
            return maze[count];
        }

        public void ReadFile(string sourceFile)
        {
            
            string str = "";
            FileStream fromStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(fromStream);
            while ((str = fileReader.ReadLine()) != null)
            {   
                int[,] matrixMaze = null;
                int[,] matrixMark = null;
                int lineCount = 1;
                string[] strSplit = str.Split(' ');
                int n = strSplit.Length + 2;
                if (matrixMaze == null)
                {
                    matrixMaze = new int[n, n];
                    matrixMark = new int[n, n];
                    
                }

                for (int i = 0; i < n; i++)
                {
                    matrixMaze[0, i] = 1;
                    matrixMaze[n - 1, i] = 1;
                    matrixMaze[i, 0] = 1;
                    matrixMaze[i, n - 1] = 1;
                }
                while (str != "" && str != null)
                {
                    for (int i = 0; i < n - 2; i++)
                    {
                        int number;
                        bool result = Int32.TryParse(strSplit[i], out number);
                        if (result)
                        {
                            matrixMaze[lineCount, i + 1] = number;
                           // Console.Write(number + " ");
                        }
                    }
                    //Console.WriteLine();
                    lineCount++;
                    str = fileReader.ReadLine();
                    if (str != null && str != "")
                        strSplit = str.Split(' ');
                }


                maze[count] = matrixMaze;
                mark[count++] = matrixMark;
            }


        }

        public void showMatrix(int count)
        {
            int[,] newMaze = maze[count];
            int n = newMaze.GetLength(0);
            for (int i = 1; i < n-1; i++)
            {
                for (int j = 1; j < n-1; j++)
                {
                    Console.Write(newMaze[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }



    }
}
