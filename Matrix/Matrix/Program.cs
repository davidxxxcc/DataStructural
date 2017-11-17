using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ThreadingTasks;
.
namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static Term[] MMult(Term[] a, Term[] b)
        {  //將兩個矩陣內藏的陣列做矩陣相乘運算

            int i = 1, column, totalB = b[0].value, totalD = 0;
            int rowsA = a[0].row, colsA = a[0].col,
                 totalA = a[0].value, colsB = b[0].col;  //totalA是第一個矩陣非零項的個數
            int rowBegin = 1, row = a[1].row, sum = 0;
            Term[] newB;
            if (colsA != b[0].row)
                return null;
            newB = FastTranspose(b);
            Term[] d = new Term[a[0].row * b[0].col + 1];
            //討論：如何降低空間複雜度？
            // 設定邊界狀況
            a[totalA + 1].row = rowsA;
            newB[totalB + 1].row = colsB;
            newB[totalB + 1].col = 0;
            while (i <= totalA)
            {
                column = newB[1].row;
                int j = 1;
                while (j <= totalB + 1)
                {
                    // a的row *= b的column
                    if (a[i].row != row) //當a走到邊界時,換下一列時
                    {
                        storeSum(d, ref totalD, row, column, ref sum);
                        i = rowBegin;
                        while (newB[j].row == column)
                            j++;
                        column = newB[j].row;
                    }
                    else if (newB[j].row != column) //b行走超過，換下一行
                    {
                        storeSum(d, ref totalD, row, column, ref sum);
                        i = rowBegin;
                        column = newB[j].row;
                    }
                    else
                    {
                        if (a[i].col - newB[j].col < 0)
                        {  // go to next term in a
                            i++;
                        }
                        else if (a[i].col - newB[j].col == 0) //add terms, go to next term in a and b
                        {
                            sum += (a[i++].value * newB[j++].value);
                        }
                        else  // advance to next term in b
                            j++;
                    }
                }

                while (a[i].row == row)
                    i++;
                rowBegin = i;
                row = a[i].row;
            } //end of for i <= totalA 

            d[0].row = rowsA;
            d[0].col = colsB;
            d[0].value = totalD;
            return d;
        }

        private static void storeSum(Term[] d, ref int totalD, int row, int column, ref int sum)
        { /* 假如sum != 0, 則sum的值與其所在的列與行會被存在d中totalD+1的位置*/
            if (sum != 0)
            {
                d[++totalD].row = row;
                d[totalD].col = column;
                d[totalD].value = sum;
                sum = 0;
            }
        }


    }
}
