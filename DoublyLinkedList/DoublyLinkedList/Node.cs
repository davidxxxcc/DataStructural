using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Node
    {
        private Node llink;
        private Node rlink;
        private string args;
        private int temp;
        private string command;

        public Node()
        {
            this.llink = null;
            this.rlink = null;
        }

        public Node(string command, string args, int temp)
        {
            this.llink = null;
            this.rlink = null;
            this.args = args;
            this.temp = temp;
            this.command = command;
        }

        public Node Getllink()
        {
            return this.llink;
        }

        public void Setllink(Node llink)
        {
            this.llink = llink;
        }

        public Node Getrlink()
        {
            return this.rlink;
        }

        public void Setrlink(Node rlink)
        {
            this.rlink = rlink;
        }

        public string GetArgs()
        {
            return this.args;
        }

        public int GetTemp()
        {
            return this.temp;
        }
        public string GetCommand()
        {
            return this.command;
        }



    }

}
