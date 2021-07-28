using System.Collections.Generic;
using Trees.Interfaces;

namespace Trees.Models
{
    public class TreeNode : ITreeNode
    {
        public TreeNode(int data)
        {
            Data = data;
        }

        public int Data { get; set; }

        public ITreeNode LeftNode { get; set; }

        public ITreeNode RightNode { get; set; }

        public void Insert(ITreeNode node)
        {
            if (node.Data < Data)
            {
                if (LeftNode == null)
                    LeftNode = node;
                else
                    LeftNode.Insert(node);
            }
            else
            {
                if (RightNode == null)
                    RightNode = node;
                else
                    RightNode.Insert(node);
            }
        }

        public int[] Transform(List<int> elements = null)
        {
            if (elements == null)
            {
                elements = new List<int>();
            }

            LeftNode?.Transform(elements);

            elements.Add(Data);

            RightNode?.Transform(elements);

            return elements.ToArray();
        }
    }
}