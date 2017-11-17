using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkList
{
    class Node
    {
        private int sn, en, ma, avg;
        private Node link;

        public Node()
        {
            this.link = null;
        }
        public Node (int sn, int en, int ma)
        {
            this.sn = sn;
            this.en = en;
            this.ma = ma;
            this.avg = (en + ma) / 2;
            this.link = null;
        }

        public void SetLink(Node link)
        {
            this.link = link;
        }

        public Node GetLink()
        {
            return this.link;
        }

        public int GetSN()
        {
            return this.sn;
        }

        public int GetAvg()
        {
            return avg;
        }

        override
        public string ToString()
        {
            string data = String.Format("{0:d}\t{1:d}\t{2:d}\t{3:d}", sn, en, ma, avg);
            return data;
        }

    }
}
