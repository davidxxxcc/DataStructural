using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_tree
{
    class TreeNode
    {

        private string name;
        private int phone;
        private TreeNode leftChild, rightChild, parent;

        public TreeNode()
        {

        }
        public TreeNode(string name, int phone)
        {
            this.name = name;
            this.phone = phone;
            leftChild = rightChild = null;
        }

        public string GetNodeName()
        {
            return this.name;
        }
        public int GetPhoneInt()
        {
            return this.phone;
        }
        public string GetPhone()
        {
            return this.phone.ToString();
        }
        public TreeNode GetParentNode()
        {
            return parent; 
        }
        public void SetParent(TreeNode node)
        {
            this.parent = node;
        }
        public TreeNode GetLeftChild()
        {
            return leftChild;
        }
        public void SetLeftChlid(TreeNode ptr)
        {
            this.leftChild = ptr;
        }
        public TreeNode GetRightChild()
        {
            return rightChild;
        }
        public void SetRightChlid(TreeNode ptr)
        {
            this.rightChild = ptr;
        }
        public void SetData(string name, int phone)
        {
            this.name = name;
            this.phone = phone;
        }


    }
}
