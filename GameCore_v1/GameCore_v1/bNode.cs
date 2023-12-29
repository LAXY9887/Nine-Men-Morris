/*

 This script is node on chest board.

 */

namespace GameCore_v1
{
    internal class bNode
    {
        public char Contain { get; set; }

        public List<bNode> Connected { get; set; }

        public char[] coordination { get; set; }

        public bNode(char contain)
        {
            Contain = contain;
            Connected = new List<bNode>();
            coordination = new char[2] ;
        }

    }
}
