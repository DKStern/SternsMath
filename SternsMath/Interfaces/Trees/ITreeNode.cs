using System.Collections.Generic;

namespace SternsMath.Interfaces.Trees
{
    public interface ITreeNode
    {
        public int Data { get; set; }

        public ITreeNode LeftNode { get; set; }

        public ITreeNode RightNode { get; set; }

        public void Insert(ITreeNode node);

        public int[] Transform(List<int> list = null);
    }
}