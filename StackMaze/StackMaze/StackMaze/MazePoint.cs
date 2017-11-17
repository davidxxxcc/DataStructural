using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackMaze
{
    struct MazePoint
    {
        public  int row;
        public  int col;
        public  int dir;

        public MazePoint(int row, int col, int dir)
        {
            this.row = row;
            this.col = col;
            this.dir = dir;
        }

        public int GetRow()
        {
            return this.row;
        }

        public int GetCol()
        {
            return this.col;
        }
        public void SetRow(int row)
        {
            this.row = row;
        }
        public void SetCol(int col)
        {
            this.col = col;
        }
    }
}
