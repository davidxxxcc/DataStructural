using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Element
    {
        private long key;
        private string word;

        public Element(string word)
        {
            this.key = StringToKey(word);
            this.word = word;
        }

        public long GetKey()
        {
            return this.key;
        }


        public static long StringToKey(string word)
        {
            byte[] charArr = Encoding.ASCII.GetBytes(word.ToUpper());
            int n = charArr.Length;
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += charArr[i]*(long)Math.Pow(9,i);
            }
            return sum;
        }

        
    }
}
