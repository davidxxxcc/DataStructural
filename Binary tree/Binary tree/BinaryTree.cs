using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_tree
{
    class BinaryTree
    {
        TreeNode root;
        public BinaryTree()
        {
            root = null;
        }
        public BinaryTree(string name, int phone)
        {
            root = new TreeNode(name, phone);
        }
        public TreeNode GetRoot()
        {
            return this.root;
        }

        //public TreeNode[] SearchNode(TreeNode ptr, string name)
        //{
        //    TreeNode[] node = new TreeNode[2];
        //    node[0] = ptr;

        //    if (ptr != null)
        //    {
        //        if (ptr.GetNodeName() == name)
        //            return node;
        //        else
        //        {
        //            node[1] = ptr;
        //            node = SearchNode(ptr.GetLeftChild(), name);
        //            if (node[0] != null)
        //                return node;
        //            node = SearchNode(ptr.GetRightChild(), name);
        //            if (node[0] != null)
        //                return node;
        //        }
        //    }
        //    return null;
        //}

        public TreeNode SearchNode(TreeNode ptr, string name)
        {
            TreeNode temp = null;

            if (ptr != null)
            {
                if (ptr.GetNodeName() == name)
                    return ptr;
                else
                {
                    temp = SearchNode(ptr.GetLeftChild(), name);
                    if (temp != null)
                        return temp;
                    temp = SearchNode(ptr.GetRightChild(), name);
                    if (temp != null)
                        return temp;
                }
            }
            return null;
        }


        public void InsertNode(TreeNode root, string name, int phone)
        {
            TreeNode newNode = new TreeNode(name, phone);
            if (this.root == null)
                this.root = newNode;
            else
            {
                TreeNode current = root;
                TreeNode parent = null;
                while (current != null)
                {
                    parent = current;
                    if (current.GetNodeName().CompareTo(name) > 0)
                    {
                        current = current.GetLeftChild();
                    }
                    else if (current.GetNodeName().CompareTo(name) < 0)
                    {
                        current = current.GetRightChild();
                    }
                    else
                        return;
                }
                if (parent.GetNodeName().CompareTo(name) > 0)
                    parent.SetLeftChlid(newNode);
                else
                    parent.SetRightChlid(newNode);
                newNode.SetParent(parent);
            }
        }

        public bool DeleteNode(string name)
        {
            TreeNode find = SearchNode(this.root, name);
            if (find == null)
                return false;
            else
            {
                if (find.GetLeftChild() == null && find.GetRightChild() != null)       //節點沒有左子樹 有右子樹
                {
                    if (find == root)       //如果要刪的是根節點
                    {
                        root = find.GetRightChild();
                        find.SetParent(null);
                    }

                    else
                    {
                        if (find.GetParentNode().GetRightChild() == find)
                            find.GetParentNode().SetRightChlid(find.GetRightChild());
                        else
                            find.GetParentNode().SetLeftChlid(find.GetRightChild());
                        find.GetRightChild().SetParent(find.GetParentNode());
                    }
                }
                else if (find.GetRightChild() == null && find.GetLeftChild() != null)      //節點沒有右子樹 有左子樹
                {
                    if (find == root)       //如果要刪的是根節點
                    {
                        root = find.GetLeftChild();
                        find.SetParent(null);
                    }
                    else
                    {   
                        if (find.GetParentNode().GetRightChild() == find)
                            find.GetParentNode().SetRightChlid(find.GetLeftChild());
                        else
                            find.GetParentNode().SetLeftChlid(find.GetLeftChild());
                        find.GetLeftChild().SetParent(find.GetParentNode());
                    }
                }
                else if (find.GetLeftChild() == null && find.GetRightChild() == null)   //節點沒有左右子樹
                {
                    if (find.GetParentNode().GetLeftChild() == find)    //find為左子
                        find.GetParentNode().SetLeftChlid(null);
                    else                                                //find為右子
                        find.GetParentNode().SetRightChlid(null);
                }
                else                                                        //節點有左右子樹
                {   //如果要刪的節點之左子節點無右子樹
                    if (find.GetLeftChild().GetRightChild() == null)
                    {
                        find.SetData(find.GetLeftChild().GetNodeName(), find.GetLeftChild().GetPhoneInt());
                        find.SetLeftChlid(find.GetLeftChild().GetLeftChild());
                        if (find.GetLeftChild().GetLeftChild() != null)
                            find.GetLeftChild().GetLeftChild().SetParent(find);
                    }
                    else 
                    {
                        TreeNode rightNode = FindRightNode(find.GetLeftChild());
                        if (rightNode.GetLeftChild() != null)
                        {
                            rightNode.GetParentNode().SetRightChlid(rightNode.GetLeftChild());
                            rightNode.GetLeftChild().SetParent(rightNode.GetParentNode());
                        }
                        find.SetData(rightNode.GetNodeName(), rightNode.GetPhoneInt());
                    }
                }
            }
            return true;
        }
        public void PrintInOrder(TreeNode ptr)
        {
            if (ptr != null)
            {
                PrintInOrder(ptr.GetLeftChild());
                Console.WriteLine(ptr.GetNodeName() + "\t" + ptr.GetPhone());
                PrintInOrder(ptr.GetRightChild());
            }
        }

        public TreeNode FindRightNode(TreeNode ptr)
        {
            TreeNode temp = new TreeNode();
            while (ptr != null)
            {
                temp = ptr;
                ptr = ptr.GetRightChild();
            }
            return temp;
        }




    }
}
