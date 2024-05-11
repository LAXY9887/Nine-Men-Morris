
/*

 This script is players class

 */

namespace GameCore_v1
{
    internal class PlayerObj
    {
        public string Name { get; set; }

        public int N_piece { get; set; }

        public int piece_on_board { get; set; } 

        public char color { get; set; }

        public PlayerObj(string name, int number_piece)
        {
            Name = name;
            N_piece = number_piece;
        }
    }
}
