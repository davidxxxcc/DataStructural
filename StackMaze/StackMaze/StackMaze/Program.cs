using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StackMaze
{
    class Program
    {
        public static Maze mz = new Maze();
        public static void Main(string[] args)
        {
            offsets[] move = new offsets[8];
            initializeMove(ref move);
            MazePoint startPoint, endPoint;

            Console.Write("Please enter file name: ");
            string sourceFile = Console.ReadLine();
            mz.ReadFile(sourceFile);

            for (int i = 0; i < mz.GetCount(); i++)
            {   
                mz.showMatrix(i);
                Console.Write("Please enter starting point x and y: ");
                string startString = Console.ReadLine();
                ReadStartingPoint(startString, out startPoint.row, out startPoint.col);

                Console.Write("Please enter ending point x and y: ");
                string endString = Console.ReadLine();
                ReadEndingPoint(endString, out endPoint.row, out endPoint.col);

                int row = startPoint.row;
                int col = startPoint.col;
                int dir = 0;
                Stack stk = new Stack();
                do
                {
                    mz.SetMark(i,row,col,1);
                    //try different orientation with d value
                    while (dir < 8)
                    {
                        int newRow = row + move[dir].getVert();
                        int newCol = col + move[dir].getHoriz();
                        if (mz.GetMaze(i)[newRow, newCol] == 0 && mz.GetMark(i)[newRow, newCol] == 0)
                        {
                            stk.Push(row, col, dir);
                            row = newRow;
                            col = newCol;
                            dir = 0;
                            break;
                        }
                        dir++;
                    }

                    if (row == endPoint.row && col == endPoint.col)
                    {
                        stk.Push(row, col, dir);
                        FileStream toStream = new FileStream("Path.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter FileWriter = new StreamWriter(toStream);
                        string str = stk.OutPut();
                        FileWriter.Write(str);
                        FileWriter.WriteLine("");
                        FileWriter.WriteLine("");
                        FileWriter.Close();
                        toStream.Close();
                        break;      //path found
                    }
                    if (dir == 8)     //所有的情況都有找過一次
                    {
                        if (stk.IsEmpty() == true)
                        {
                            FileStream toStream = new FileStream("Path.txt", FileMode.Append, FileAccess.Write);
                            StreamWriter FileWriter = new StreamWriter(toStream);
                            FileWriter.WriteLine("No path.");
                            FileWriter.Close();
                            toStream.Close();
                        }
                        //wrong position, return to last point
                        else
                        {
                            MazePoint q = stk.Pop();
                            row = q.row;
                            col = q.col;
                            dir = q.dir + 1;   //next orientation
                        }

                    }
                    if (stk.IsEmpty() == true)
                    {
                        FileStream toStream = new FileStream("Path.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter FileWriter = new StreamWriter(toStream);
                        FileWriter.WriteLine("No path.");
                        FileWriter.Close();
                        toStream.Close();
                    }
                } while (!stk.IsEmpty());
            }


        }

        private static void initializeMove(ref offsets[] move)
        {
            move[0].setVert(-1);
            move[0].setHoriz(0);
            move[1].setVert(-1);
            move[1].setHoriz(1);
            move[2].setVert(0);
            move[2].setHoriz(1);
            move[3].setVert(1);
            move[3].setHoriz(1);
            move[4].setVert(1);
            move[4].setHoriz(0);
            move[5].setVert(1);
            move[5].setHoriz(-1);
            move[6].setVert(0);
            move[6].setHoriz(-1);
            move[7].setVert(-1);
            move[7].setHoriz(-1);
        }

        private static void ReadStartingPoint(string startString, out int startPointRow, out int startPointCol)
        {
            string[] startSplit = startString.Split(' ');
            bool result = Int32.TryParse(startSplit[0], out startPointRow);
            if (!result)
                Console.WriteLine("Input error");
            result = Int32.TryParse(startSplit[1], out startPointCol);
            if (!result)
                Console.WriteLine("Input error");
            startPointRow += 1;
            startPointCol += 1;
        }

        private static void ReadEndingPoint(string endString, out int endPointRow, out int endPointCol)
        {
            string[] endSplit = endString.Split(' ');
            bool result = Int32.TryParse(endSplit[0], out endPointRow);
            if (!result)
                Console.WriteLine("Input error");
            result = Int32.TryParse(endSplit[1], out endPointCol);
            if (!result)
                Console.WriteLine("Input error");

            endPointRow += 1;
            endPointCol += 1;
        }

        private static void ReadFile(String sourceFile, ref int[,] maze)
        {
            string str = "";
            int count = 1;
            FileStream fromStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(fromStream);
            while ((str = fileReader.ReadLine()) != null)
            {
                string[] strSplit = str.Split(' ');
                int n = strSplit.Length + 2;
                if (maze == null)
                {
                    maze = new int[n , n ];
                }
                for (int i = 0; i < n; i++)
                {
                    maze[0, i] = 1;
                    maze[n - 1, i] = 1;
                    maze[i, 0] = 1;
                    maze[i, n-1] = 1;
                }
                for (int i = 0; i < n -2 ; i++)
                {   
                    int number;
                    bool result = Int32.TryParse(strSplit[i], out number);
                    if (result)
                    {
                        maze[count, i+1] = number;
                        Console.Write(number + " ");
                    }
                }
                Console.WriteLine();
                count++;
            }
        }

        struct offsets
        {
            short vert;
            short horiz;

            public void setVert(short vert)
            {
                this.vert = vert;
            }

            public void setHoriz(short horiz)
            {
                this.horiz = horiz;
            }

            public int getVert()
            {
                return vert;
            }

            public int getHoriz()
            {
                return horiz;
            }
        }
    }
}
