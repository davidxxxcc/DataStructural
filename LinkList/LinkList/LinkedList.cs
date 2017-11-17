using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkList
{
    class LinkedList
    {
        Node first;

        //初始化
        public LinkedList()
        {
            first = null;
        }

        //判斷串列是否為空
        public bool IsEmpty()
        {
            if (first != null)
                return false;
            return true;
        }

        //比對資料取得節點
        public bool IsRepeat(int sn)
        {
            Node tmp = first;
            while(tmp != null)
            {
                if (sn == tmp.GetSN())
                    return true;
                tmp = tmp.GetLink();
            }
            return false;
        }


        public void InsertionOrdered(int sn, int en, int ma)
        {
            int avg = (en + ma) / 2;
            Node tmp = first;
            Node prev = null;   //代表前一個節點
            while(tmp != null)
            {
                if (avg > tmp.GetAvg())
                    break;
                prev = tmp;
                tmp = tmp.GetLink();
            }

            Node newNode = new Node(sn,en,ma);
            if(tmp == this.first)   //如果this.first是null，也在這個情況之中
            {   //新增到開頭
                newNode.SetLink(this.first);
                this.first = newNode;
            }
            else if (tmp == null)   //順序不能跟第一個判斷式對調
            {   //新增在結尾
                prev.SetLink(newNode);
            }
            else
            {   //新增在中間
                newNode.SetLink(tmp);
                prev.SetLink(newNode);
            }
        }

        public bool Delete (int  sn)
        {
            Node tmp = first;
            Node prev = null; //代表前一個節點
            while(tmp != null)
            {
                if (sn.Equals(tmp.GetSN()))
                    break;
                prev = tmp;
                tmp = tmp.GetLink();
            }
            if (tmp == null)
                return false;   //代表串列為空的或者沒有找到資料
            else if (tmp == this.first)
                this.first = this.first.GetLink();  //更新了鏈結串列的first參考
            else
                prev.SetLink(tmp.GetLink());
            return true;
        }

        public string Output()
        {
            if (this.IsEmpty())
            {
                return null;
            }
            else
            {
                string str = "";
                Node tmp = first;
                Node prev = null;
                while (tmp != null)
                {
                    str += tmp.ToString() + "\r\n";
                    prev = tmp;
                    tmp = tmp.GetLink();
                }
                return str;
            }
        }
    }
}
