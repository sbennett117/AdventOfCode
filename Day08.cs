using System;
namespace AdventOfCode
{
    public class Day08
    {
        public int SumMetadata()
        {
            string[] licence = System.IO.File.ReadAllLines(@"input/Day08.txt")[0].Split(' ');
            TreeNode root = BuildNode(licence, 0).Item2;

            return root.SumMeta();
        }

        public int RootValue()
        {
            string[] licence = System.IO.File.ReadAllLines(@"input/Day08.txt")[0].Split(' ');
            TreeNode root = BuildNode(licence, 0).Item2;

            return root.NodeValue();
        }

        public Tuple<int, TreeNode> BuildNode(string[] licence, int currIndex)
        {
            int children = int.Parse(licence[currIndex]);
            int metaQuant = int.Parse(licence[currIndex + 1]);
            TreeNode node = new TreeNode(children, metaQuant);
            int newIndex = currIndex + 2;
            for (int i = 0; i < children; i++)
            {
                Tuple<int, TreeNode> pair = BuildNode(licence, newIndex);
                newIndex = pair.Item1;
                node.AddChild(pair.Item2);
            }
            for (int i = 0; i < metaQuant; i++)
            {
                node.AddMeta(int.Parse(licence[newIndex + i]));
            }
            return new Tuple<int, TreeNode>(newIndex + metaQuant, node);
        }
    }

    public class TreeNode
    {
        TreeNode[] children;
        int[] metadata { get; }
        int currChildren;
        int currMeta;

        public TreeNode(int noOfChildren, int noOfMetadata)
        {
            children = new TreeNode[noOfChildren];
            metadata = new int[noOfMetadata];
        }

        public void AddChild(TreeNode child)
        {
            if (currChildren < children.Length)
            {
                children[currChildren] = child;
                currChildren++;
            }
        }

        public void AddMeta(int meta)
        {
            if (currMeta < metadata.Length)
            {
                metadata[currMeta] = meta;
                currMeta++;
            }
        }

        public int SumMeta()
        {
            int sum = 0;
            foreach (int meta in metadata) sum += meta;
            foreach (TreeNode child in children){
                sum += child.SumMeta();
            } 
            return sum;
        }

        public int NodeValue()
        {
            if (children.Length == 0) return SumMeta();
            int value = 0;
            foreach (int meta in metadata)
            {
                if (meta != 0 && meta <= children.Length) value += children[meta - 1].NodeValue();
            }
            return value;
        }
    }
}
