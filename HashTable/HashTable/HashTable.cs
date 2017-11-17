using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class HashTable
    {
        private Element[] ht = null;
        private int b = 1;
        public HashTable(int arraySize)
        {
            ht = new Element[arraySize];
            b = arraySize;
        }

        private int H(long k)
        {
            if (ht != null)
                return (int)(k % ht.Length);
            return 0;
        }

        public void ElementInsertion(string word)
        {
            Element newElement = new Element(word);
            int homeBucket = H(newElement.GetKey());
            int currentBucket = homeBucket;
            while(ht[currentBucket] != null && ht[currentBucket].GetKey() != newElement.GetKey())
            {
                currentBucket = (currentBucket + 1) % b;
                if(currentBucket == homeBucket)     //整個HashTable都搜尋過一遍已經回到homeBucket點
                    return;
            }
            if (ht[currentBucket] != null && ht[currentBucket].GetKey() == newElement.GetKey()) //HashTable中已經有相同的key值
                return ;
            else
                ht[currentBucket] = newElement;
        }

        /* 用線性探索法在雜湊表ht中搜尋key所對應的數值（每一個bucket中將僅存放一個slot，
         * 當中儲存位址/參考指向一個（key, element）的數對(pair)），
         * 如果有找到key，就回傳這個數對（型態為Element），否則便回傳null */
        public Element ElementSearch(string word)
        {
            long k = Element.StringToKey(word);
            int homeBucket = H(k);
            int currentBucket = homeBucket;
            while (ht[currentBucket] != null && ht[currentBucket].GetKey() != k)
            {
                currentBucket = (currentBucket + 1) % b;
                if (currentBucket == homeBucket)
                    return null;
            }
            if (ht[currentBucket] != null && ht[currentBucket].GetKey() == k)
                return ht[currentBucket];
            return null;
        }
    }
}
