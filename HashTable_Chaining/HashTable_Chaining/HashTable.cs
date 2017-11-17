using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable_Chaining
{
    class HashTable
    {

        private Node[] ht = null;
        private int b = 1;
        public HashTable(int arraySize)
        {
            ht = new Node[arraySize];
            b = arraySize;
        }

        private int H(int k)
        {
            if (ht != null)
                return k % ht.Length;
            return 0;
        }

        private int GetKey(Node node)
        {
            return node.GetElement().GetKey();
        }

        public void NodeInsertion(string word)
        {
            Node newNode = new Node(word);
            int homeBucket = H(newNode.GetKey());
            int currentBucket = homeBucket;
            while (ht[currentBucket] != null && ht[currentBucket].GetKey() != newNode.GetKey())
            {
                if (ht[currentBucket].GetLink() == null)
                {
                    ht[currentBucket].SetLink(newNode);
                    return;
                }
                ht[currentBucket] = ht[currentBucket].GetLink();

            }
            if (ht[currentBucket] != null && ht[currentBucket].GetKey() == newNode.GetKey())
                return;
            else
            {
                ht[currentBucket] = newNode;
            }
        }

        /* 用線性探索法在雜湊表ht中搜尋key所對應的數值（每一個bucket中將僅存放一個slot，
         * 當中儲存位址/參考指向一個（key, element）的數對(pair)），
         * 如果有找到key，就回傳這個數對（型態為Element），否則便回傳null */
        public Node NodeSearch(string word)
        {
            Node newNode = new Node(word);
            int homeBucket = H(newNode.GetKey());
            int currentBucket = homeBucket;
            while (ht[currentBucket] != null && ht[currentBucket].GetKey() != newNode.GetKey())
            {
                ht[currentBucket] = ht[currentBucket].GetLink();
            }
            if (ht[currentBucket] != null && ht[currentBucket].GetKey() == newNode.GetKey())
                return ht[currentBucket];
            return null;
        }
    }
}
