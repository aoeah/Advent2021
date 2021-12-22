using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day18
    {
        public string Part1()
        {
            TreeNode parent = BuildExpression(GetData().First(), 0);

            foreach (var line in GetData().Skip(1))
            {
                parent = Add(parent, BuildExpression(line, 0));
            }

            return string.Empty;
        }

        public string Part2() 
        {
            return string.Empty;
        }

        private bool Explode(TreeNode treeNode)
        {
            return Explode(treeNode, 1);
        }

        private bool Explode(TreeNode node, int depth)
        {
            if (node == null) return false;
            if (node.Left == null && node.Right == null && depth >= 4) return false;

            if(node.Left != null && depth >= 3)
            {
                //perform explode

                var left = FindLeft(node);
                var right = FindRight(node);

                if(left != null)
                {
                    left.RightValue += node.LeftValue;
                }

                if(right != null)
                {
                    right.LeftValue += node.RightValue;
                }
                //find next left
                //find next right
                //clear child references and parents child ref then set left value to 0 on parent

                return true;
            }

            if(node.Right != null && depth >= 3)
            {
                //perform explode

                return true;
            }

            if (node.Left != null && Explode(node.Left, depth + 1)) return true;
            if (node.Right != null && Explode(node.Right, depth + 1)) return true;

            return false;
        }

        private TreeNode FindLeft(TreeNode treeNode)
        {
            if (treeNode == null) return null;
            if (treeNode.Parent == null) return null;

            var node = treeNode;

            node = node.Parent;

            while(node.Left == treeNode || node.Left == null || node.LeftValue != null)
            {
                if (node.Parent == null) return null;

                node = node.Parent;
            }

            return DiveLeft(node);
        }

        private TreeNode FindRight(TreeNode treeNode)
        {
            if (treeNode == null) return null;
            if (treeNode.Parent == null) return null;

            var node = treeNode;

            node = node.Parent;

            while (node.Left == treeNode || node.Right == null || node.RightValue != null)
            {
                if (node.Parent == null) return null;

                node = node.Parent;
            }

            return DiveRight(node);
        }

        private TreeNode DiveLeft(TreeNode node)
        {
            if (node.RightValue != null) return node;

            if (node.Right != null) return DiveLeft(node.Right);
            else return DiveLeft(node.Left);
        }

        private TreeNode DiveRight(TreeNode node)
        {
            if (node.LeftValue != null) return node;

            if (node.Left != null) return DiveRight(node.Left);
            else return DiveRight(node.Right);
        }


        private TreeNode Add(TreeNode left, TreeNode right)
        {
            var newParent = new TreeNode();
            newParent.Left = left;
            left.Parent = newParent;
            newParent.Right = right;
            right.Parent = newParent;

            return newParent;
        }

        private TreeNode BuildExpression(string expression, int index)
        {
            var newNode = new TreeNode();
            var count = 0;

            //build left
            if(expression[index + 1] == '[')
            {
                var left = BuildExpression(expression, index + 1);
                left.Parent = newNode;
                newNode.Left = left;
            }
            else
            {
                newNode.LeftValue = expression[index + 1] - '0';
            }

            //find right hand part of the expression

            //go past first character
            index++;

            while(true)
            {
                if (count == 0 && expression[index] == ',') break;
                if (expression[index] == '[') count++;
                if (expression[index] == ']') count--;

                index++;
            }

            //build right
            if (expression[index + 1] == '[')
            {
                var right = BuildExpression(expression, index + 1);
                right.Parent = newNode;
                newNode.Right = right;
            }
            else
            {
                newNode.RightValue = expression[index + 1] - '0';
            }

            return newNode;
        }

        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day18\\day18.txt");

            return lines;

            //standard header
            //3 bits
            // version - 6
            
            //3 bits
            // 4 - literal value


            //literal 
            //version - 3 bits
            //
        }


        public class TreeNode
        {
            public int? LeftValue = null;
            public int? RightValue = null;

            private TreeNode _left;
            private TreeNode _right;
            public TreeNode Left { 
                get { return _left; }
                set { if (LeftValue != null) throw new ArgumentException("Cannot have a child if value is set"); _left = value; }
            }
            public TreeNode Right
            {
                get { return _right; }
                set { if (RightValue != null) throw new ArgumentException("Cannot have a child if value is set"); _right = value; }
            }

            //Maybe will use
            public TreeNode Parent;
        }

    }
}
