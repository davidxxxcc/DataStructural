using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackMaze
{
    class Stack
    {
        int capacity;
        private MazePoint[]items;
        int top;   //宣告變數 top 代表堆疊中頂端的元素，初始化時設定top為 -1 因為堆疊為空

        public Stack()
        {
            capacity = 10;
            top = -1;
            items = new MazePoint[capacity];
        }

        public bool IsEmpty()
        {
            if (top < 0)
                return true;
            return false;
        }

        public bool IsFull()
        {
            if (top >= items.Length - 1)
                return true;
            return false;
        }

        public MazePoint GetItem(int index)
        {
            return items[index];
        }

        public MazePoint Pop()
        {
            if (IsEmpty())
                return new MazePoint(-1,-1,-1);
            return items[top--];
        }

        public void Push(int row, int col, int dir)
        {
            if (!IsFull())
                items[++top] = new MazePoint(row, col, dir); // 從堆疊頂部加入並改變top值
            else
            {
                Resize();
                items[++top] = new MazePoint(row, col, dir);
            }
        }

        private void Resize()
        {
                Array.Resize(ref items, items.Length * 2);
        }

        public string OutPut()
        {
            string str = "";
            for(int i = 0; i <= top; i ++)
            {   if (i <= top -1)
                    str += string.Format("({0:D},{1:D}) -> ", items[i].row -1, items[i].col -1);
                if (i == top)
                    str += string.Format("({0:D},{1:D})", items[i].row -1 , items[i].col -1);
            }
            return str;
        }
    }
}
