using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable_Chaining
{
    class Node
    {
        public Element data = null;
        public Node link = null;

        public Node(string word)
        {
            data = new Element(word);
        }
        public int GetKey()
        {
            return data.GetKey();
        }
        public Element GetElement()
        {
            return this.data;
        }
        public Node GetLink()
        {
            return this.link;
        }
        public void SetLink(Node link)
        {
            this.link = link;
        }
    }
}
