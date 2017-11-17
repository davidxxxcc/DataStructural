using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable_Chaining
{
    class Element
    {
        private int key;
        private string word;

        public Element(string word)
        {
            this.key = StringToKey(word);
            this.word = word;
        }

        public int GetKey()
        {
            return this.key;
        }


        public static int StringToKey(string word)
        {
            byte[] charArr = Encoding.ASCII.GetBytes(word.ToUpper());
            int n = charArr.Length, sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += charArr[i];
            }
            return sum;
        }


    }
}
