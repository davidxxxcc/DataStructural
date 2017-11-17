using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class DoublyLinkedList
    {
        Node first, last, header, current;

        public Node GetCurrent()
        {
            return this.current;
        }
        public DoublyLinkedList()
        {
            first = null;
            last = null;
            header = new Node();
            current = null;
        }

        public bool Delete(Node deleted)
        {  //從雙向鏈結串列刪除節點”deleted”
            if (this.first == deleted)   //node 指向 header node
                return false;   //不可刪除header node
            deleted.Getllink().Setrlink(deleted.Getrlink());
            deleted.Getrlink().Setllink(deleted.Getllink());
            return true;
        }

        public bool Add(string command, string args, int temp)
        {
            Node newNode = new Node(command, args, temp);
            if (header.Getrlink() ==  null)
            {
                first = last = newNode;
                header.Setrlink(newNode);
                newNode.Setllink(header);
            }
            else
            {
                newNode.Setllink(last);
                last.Setrlink(newNode);
                last = newNode;
                current = last;
            }
            return true;
        }


        public void MoveCurNext()
        {
            current = current.Getrlink();
        }

        public void MoveCurLast()
        {
            current = current.Getllink();
        }

        public bool Next()
        {
            if(current.Getrlink() != null)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Last()
        {
            if (current.Getllink() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
