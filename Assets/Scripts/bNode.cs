using System.Collections.Generic;

namespace Assets.Scripts
{
    internal class bNode
    {
        // Store node value: black (B), white (W), empty (o) or road block (.)
        public char Contain { get; set; }

        // Store connected nodes.
        public List<bNode> Connected { get; set; }

        // Store node X and Y values.
        public char[] coordination { get; set; }

        public bNode(char contain)
        {
            Contain = contain;
            Connected = new List<bNode>();
            coordination = new char[2];
        }
    }
}
