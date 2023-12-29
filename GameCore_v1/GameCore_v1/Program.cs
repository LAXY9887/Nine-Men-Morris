/*

    This script is core of Morris 9 man Game. 

 */

using GameCore_v1;

// Name and dimension of axies
const int board_width = 7;
const int board_height = 7;
char[] board_x = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
char[] board_y = { '1', '2', '3', '4', '5', '6', '7' };

// Create a 7x7 board in a 2D array.
bNode[,] make_Board(int width, int height)
{
    // This function create a game board
    bNode[,] game_board = new bNode[height, width];

    // Setting blocks on board (spots that can't move or place)
    int[] block1 = { 1, 2, 4, 5 };
    int[] block2 = { 0, 2, 4, 6 };
    int[] block3 = { 0, 1, 5, 6 };
    int[] block4 = { 3 };
    int[][] blockList = { block1, block2, block3, block4 };

    // Settling blocks on board
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            for(int k = 0; k < blockList.Length; k++)
            {
                if(j == k || j == height - (k + 1))
                {
                    if (blockList[k].Contains(i))
                    {
                        game_board[j, i] = new bNode('.');
                    }
                    else
                    {
                        game_board[j, i] = new bNode('o');

                        // Record coordination
                        game_board[j, i].coordination[0] = board_x[i];
                        game_board[j, i].coordination[1] = board_y[j];
                    }
                }
            }
        }
    }

    // Connect the 'o' nodes Vertically
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {

            if (game_board[j, i].Contain == 'o')
            {
                if (game_board[j, i].Connected == null)
                {
                    game_board[j, i].Connected = new List<bNode>();
                }

                if (i < (height - 1) / 2)
                {
                    if (j < (width - 1) / 2)
                    {
                        // Connect to bottom
                        game_board[j, i].Connected.Add(game_board[j + 3 - i, i]);
                    }
                    else if (j == (width - 1) / 2)
                    {
                        // Connect to up and bottom
                        game_board[j, i].Connected.Add(game_board[j + 3 - i, i]);
                        game_board[j, i].Connected.Add(game_board[j - 3 + i, i]);
                    }
                    else
                    {
                        // Connect to up
                        game_board[j, i].Connected.Add(game_board[j - 3 + i, i]);
                    }
                }
                else if (i == (height - 1) / 2)
                {
                    if (j == 0 | j == 4)
                    {
                        // Connect to bottom
                        game_board[j, i].Connected.Add(game_board[j + 1, i]);
                    }
                    else if (j == 1 | j == 5)
                    {
                        // Connect to up and bottom
                        game_board[j, i].Connected.Add(game_board[j + 1, i]);
                        game_board[j, i].Connected.Add(game_board[j - 1, i]);
                    }
                    else if (j == 2 | j == 6)
                    {
                        // Connect to up 
                        game_board[j, i].Connected.Add(game_board[j - 1, i]);
                    }
                    
                }
                else if (i > (height - 1) / 2)
                {
                    if (j < (width - 1) / 2)
                    {
                        // Connect to bottom
                        game_board[j, i].Connected.Add(game_board[j + i - ((height - 1) / 2), i]);
                    }
                    else if (j == (width - 1) / 2)
                    {
                        // Connect to up and bottom
                        game_board[j, i].Connected.Add(game_board[j + i - ((height - 1) / 2), i]);
                        game_board[j, i].Connected.Add(game_board[j - i + ((height - 1) / 2), i]);
                    }
                    else
                    {
                        // Connect to up
                        game_board[j, i].Connected.Add(game_board[j - i + ((height - 1) / 2), i]);
                    }
                }
                
            }

        }
    }

    // Connect the 'o' nodes Horizonally 
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {

            if (game_board[j, i].Contain == 'o')
            {
                if (game_board[j, i].Connected == null)
                {
                    game_board[j, i].Connected = new List<bNode>();
                }

                if (i < (height - 1) / 2)
                {
                    if (j < (width - 1) / 2)
                    {
                        // Connect to right
                        game_board[i, j].Connected.Add(game_board[i, j + 3 - i]);
                    }
                    else if (j == (width - 1) / 2)
                    {
                        // Connect to right and left
                        game_board[i, j].Connected.Add(game_board[i, j + 3 - i]);
                        game_board[i, j].Connected.Add(game_board[i, j - 3 + i]);
                    }
                    else
                    {
                        // Connect to left
                        game_board[i, j].Connected.Add(game_board[i, j - 3 + i]);
                    }
                }
                else if (i == (height - 1) / 2)
                {
                    if (j == 0 | j == 4)
                    {
                        // Connect to right
                        game_board[i, j].Connected.Add(game_board[i, j + 1]);
                    }
                    else if (j == 1 | j == 5)
                    {
                        // Connect to right and left
                        game_board[i, j].Connected.Add(game_board[i, j + 1]);
                        game_board[i, j].Connected.Add(game_board[i, j - 1]);
                    }
                    else if (j == 2 | j == 6)
                    {
                        // Connect to left
                        game_board[i, j].Connected.Add(game_board[i, j - 1]);
                    }

                }
                else if (i > (height - 1) / 2)
                {
                    if (j < (width - 1) / 2)
                    {
                        // Connect to right
                        game_board[i, j].Connected.Add(game_board[i, j + i - ((height - 1) / 2)]);
                    }
                    else if (j == (width - 1) / 2)
                    {
                        // Connect to right and left
                        game_board[i, j].Connected.Add(game_board[i, j + i - ((height - 1) / 2)]);
                        game_board[i, j].Connected.Add(game_board[i, j - i + ((height - 1) / 2)]);
                    }
                    else
                    {
                        // Connect to left
                        game_board[i, j].Connected.Add(game_board[i, j - i + ((height - 1) / 2)]);
                    }
                }

            }

        }
    }

    return game_board;
}
bNode[,] board = make_Board(board_width, board_height);

// Make a dict for each axis to an integer
Dictionary<char, int> axis_toDict(char[] axis)
{
    Dictionary<char, int> temp = new Dictionary<char, int>();
    for (int i = 0; i < axis.Length; i++)
    {
        temp.Add(axis[i], i);
    }
    return temp;
}
Dictionary<char, int> axis_x_Dict = axis_toDict(board_x);
Dictionary<char, int> axis_y_Dict = axis_toDict(board_y);

// Create 2 players PlayerA, PlayerB, Each player has 9 pieces
string playerA_name = "PlayerA";
string playerB_name = "PlayerB";
const int n_piece = 9;
PlayerObj playerA = new PlayerObj(playerA_name, n_piece);
PlayerObj playerB = new PlayerObj(playerB_name, n_piece);
PlayerObj[] playerList = { playerB, playerA };

// Sudden death round
bool isSuddenDeath = false;
int suddenDeath_round = 10;

// Randomly decide who play first hand.
void RandomizePlayers()
{
    Random rand = new Random();
    int rand_n = rand.Next(2);
    if (rand_n > 0)
    {
        PlayerObj tmp = playerList[0];
        playerList[0] = playerList[1];
        playerList[1] = tmp;
    }

    // Assign color
    playerList[0].color = 'B';
    playerList[1].color = 'W';
}
RandomizePlayers();

// To show The player status
void show_Players(PlayerObj[] player_list)
{
    Console.Write(player_list[0].Name + " 先手黑棋(B); ");
    Console.Write(player_list[1].Name + " 後手白棋(W); ");
    Console.WriteLine("\n");
}
show_Players(playerList);

// Record Mill combination
List<string> Mill_list = new List<string>();

// To show the board status
void show_Board(bNode[,] B)
{
    // Plot the boundary
    void draw_up_down_boundary(char d)
    {
        Console.Write("  ");
        for (int i = 0; i < B.GetLength(1) * 2; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(d);
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();
    }

    // This function show board status.
    Console.WriteLine("--> Showing Board <--");

    // Show sudden death round
    if (isSuddenDeath)
    {
        Console.WriteLine("剩下子數目為 4 : 3 進入倒數回合 ");
        Console.WriteLine("殘餘移動數: " + suddenDeath_round);
    }

    // Show players' pieces on board
    foreach (PlayerObj pl in playerList)
    {
        Console.WriteLine(pl.Name + "(" + pl.color + ")" + " 在場上有 " + pl.piece_on_board + " 個子 ");
    }

    // Draw upper boarder 
    Console.WriteLine() ;
    Console.Write("  ");

    int boarder_Xaxis_idx = 0;
    for (int i = 0; i < B.GetLength(1) * 2; i++)
    {
        if (i % 2 == 0)
        {
            Console.Write(board_x[boarder_Xaxis_idx]);
            boarder_Xaxis_idx++;
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
    draw_up_down_boundary('=');

    // Loop through array and print values
    int boarder_Yaxis_idx = 0;
    for (int i = 0; i < B.GetLength(0); i++)
    {
        Console.Write(board_y[boarder_Yaxis_idx]);
        boarder_Yaxis_idx ++;
        Console.Write("|"); // Draw boarder
        for (int j = 0; j < B.GetLength(1); j++)
        {
            if (j < B.GetLength(1) -1 )
            {
                Console.Write(B[i, j].Contain + "-");
            }
            else
            {
                // Draw boarder
                Console.Write(B[i, j].Contain + "|");
            }
        }
        Console.WriteLine();
    }

    // Draw lower boarder 
    draw_up_down_boundary('=');
    Console.WriteLine();
}

// To valid player input string
bool valid_input(string str)
{
    if (str.Length != 2)
    {
        Console.WriteLine("Invalid! 請重新輸入! \nPress Enter to Continue ......");
        Console.ReadKey();
        return false;
    }

    bool isValid = true;
    // Check whether input fit in board_x and board_y
    for (int i = 0; i < str.Length; i++)
    {
        if (i == 0 & !board_x.Contains(str[0]))
        {
            isValid = false;
        }
        else if (i == 1 & !board_y.Contains(str[1]))
        {
            isValid = false;
        }
    }

    // Validation
    if (!isValid)
    {
        Console.WriteLine("Invalid! 請重新輸入! \nPress Enter to Continue ......");
        Console.ReadKey();
        return false;
    }

    return true;
}

// Player place pieces at Phase I
bool place_Piece(PlayerObj player, string inputPOS)
{
    // Check Input
    bool inputCheck = valid_input(inputPOS);
    if (!inputCheck) { return false; }

    // Place piece
    int placeX = axis_x_Dict[inputPOS[0]];
    int placeY = axis_y_Dict[inputPOS[1]];
    if (board[placeY, placeX].Contain == 'o')
    {
        // Place at as the inputPOS
        board[placeY, placeX].Contain = player.color;

        // Decrease player's piece number
        player.N_piece -= 1;

        // Increase player's piece on board
        player.piece_on_board += 1;

        return true;
    }
    else
    {
        Console.WriteLine("不能放在該位置,請重新輸入! \nPress Enter to Continue ......");
        Console.ReadKey();
        return false;
    }
}

// Player move pieces at Phase II
bool move_Piece(PlayerObj player)
{

    // To store Player inputs
    string playerMoveInput = "";
    string playerDestInput = "";

    /* Hint & Handle input */

    // Target
    bool TargetOK = false;
    while (!TargetOK)
    {
        // Initialize playerMoveInput
        playerMoveInput = "";

        // Move target
        Console.Write(player.Name + " (" + player.color + ") " + " 行動, 選擇要移動的子: ");
        playerMoveInput += Console.ReadLine();

        // Check input
        if (valid_input(playerMoveInput))
        {
            int Input_target_X = axis_x_Dict[playerMoveInput[0]];
            int Input_target_Y = axis_y_Dict[playerMoveInput[1]];

            bNode selected = board[Input_target_Y, Input_target_X];
            if (selected.Contain == player.color)
            {
                int free_Node = 0;
                foreach (bNode node in selected.Connected)
                {
                    if (node.Contain == 'o')
                    {
                        free_Node++;
                    }
                }
                if (free_Node >= 1)
                {
                    TargetOK = true;
                }
                else
                {
                    Console.WriteLine("Invalid! 無路可走, 請重新選擇");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Invalid! 不是自己的棋子, 請重新選擇");
                Console.ReadKey();
            }
        }
    }

    // Destination
    bool DestOK = false;
    while (!DestOK)
    {
        // Initialize
        playerDestInput = "";

        // Move target
        Console.Write(player.Name + " (" + player.color + ") " + " 行動, 選擇目的地: ");
        playerDestInput += Console.ReadLine();

        // Check input
        if (valid_input(playerDestInput))
        {
            int Input_target_X = axis_x_Dict[playerMoveInput[0]];
            int Input_target_Y = axis_y_Dict[playerMoveInput[1]];
            bNode selected_target = board[Input_target_Y, Input_target_X];

            int Input_dist_X = axis_x_Dict[playerDestInput[0]];
            int Input_dist_Y = axis_y_Dict[playerDestInput[1]];
            bNode selected_dist = board[Input_dist_Y, Input_dist_X];

            if (selected_target.Connected.Contains(selected_dist) & selected_dist.Contain == 'o')
            {
                DestOK = true;
            }
            else if (selected_target.coordination == selected_dist.coordination)
            {
                DestOK = true;
                Console.WriteLine("> 輸入同一個位置, 重新選擇要移動的棋子 <");
                Console.ReadKey();
                return false;
            }
            else
            {
                Console.WriteLine("Invalid! 無法移動到該位置, 請重新選擇");
                Console.ReadKey();
            }
        }
    }

    // Change node contanins
    bNode fromNode = board[axis_y_Dict[playerMoveInput[1]], axis_x_Dict[playerMoveInput[0]]];
    bNode toNode = board[axis_y_Dict[playerDestInput[1]], axis_x_Dict[playerDestInput[0]]];
    fromNode.Contain = 'o';
    toNode.Contain = player.color;

    // If selection is in a mill, remove mill from list
    List<string> temp = new List<string>();
    foreach (string mill in Mill_list)
    {
        if (!mill.Contains(playerMoveInput))
        {
            temp.Add(mill);
        }
    }
    Mill_list = temp;

    return true;
}

// To Check whether a mill is form.
bool check_Mill()
{
    // To check one of directions.
    int check_coord(int coord, bNode node)
    {
        int count = 1;
        foreach (bNode connect in node.Connected)
        {
            if (connect.coordination[coord] == node.coordination[coord] & connect.Contain == node.Contain & connect.Contain != 'o')
            {
                count++;
            }
        }
        return count;
    }

    // To record nodes that form the Mill.
    string collectMill(int coord, bNode node)
    {
        string report = node.coordination[0].ToString() + node.coordination[1].ToString() + ", ";

        foreach (bNode connect in node.Connected)
        {
            if (connect.coordination[coord] == node.coordination[coord] & connect.Contain == node.Contain & connect.Contain != 'o')
            {
                report += connect.coordination[0].ToString() + connect.coordination[1].ToString() + ", ";
            }
        }

        return report;
    }

    // Place to check
    string[] check_list = { "d2", "d6", "b4", "f4", "d1", "d3", "d5", "d7", "a4", "c4", "e4", "g4" };
    string[] check_typeI = { "d2", "d6", "b4", "f4" };
    string[] check_typeLR = { "d1", "d3", "d5", "d7" };
    string[] check_typeTB = { "a4", "c4", "e4", "g4" };

    // Loop through the check list
    foreach (string check in check_list)
    {
        // Place piece
        int placeX = axis_x_Dict[check[0]];
        int placeY = axis_y_Dict[check[1]];
        bNode check_node = board[placeY, placeX];

        // To check type I: All four direction
        if (check_typeI.Contains(check))
        {
            // Check all four directions
            for (int i = 0; i < 2; i++)
            {
                if (check_coord(i, check_node) == 3)
                {
                    // If a mill was formed, add Mill combination to Mill list.
                    string newMill = collectMill(i, check_node);
                    if (!Mill_list.Contains(newMill))
                    {
                        Mill_list.Add(newMill);
                        return true;
                    }
                }
            }
        }
        // To check type LR: Left and Right direction
        else if (check_typeLR.Contains(check))
        {
            // Check left and right 
            if (check_coord(1, check_node) == 3)
            {
                // If a mill was formed, add Mill combination to Mill list.
                string newMill = collectMill(1, check_node);
                if (!Mill_list.Contains(newMill))
                {
                    Mill_list.Add(newMill);
                    return true;
                }
            }
        }
        // To check type TB: Top and Bottom direction
        else if (check_typeTB.Contains(check))
        {
            // Check top and bottom 
            if (check_coord(0, check_node) == 3)
            {
                // If a mill was formed, add Mill combination to Mill list.
                string newMill = collectMill(0, check_node);
                if (!Mill_list.Contains(newMill))
                {
                    Mill_list.Add(newMill);
                    return true;
                }
            }
        }
    }

    return false;
}

// To Remove a opponent's piece
bool Remove_opponent_piece(PlayerObj player)
{
    // A collection for possible chooices
    List<char[]> remove_choices = new List<char[]>();
    List<string> remove_choices_str = new List<string>();

    // List to check
    char[] check_list = new char[] { 'o', '.', player.color };

    // Collect opponent piece location
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (!check_list.Contains(board[j, i].Contain))
            {
                remove_choices.Add(board[j, i].coordination);
            }
        }
    }

    // Convert char[] to string
    foreach (char[] choice in remove_choices)
    {
        remove_choices_str.Add(choice[0].ToString() + choice[1].ToString());
    }

    // Hints
    string strRemove_possible = "";
    foreach (string choice in remove_choices_str)
    {
        strRemove_possible += choice + ", ";
    }
    Console.WriteLine(player.Name + " 做成Mill, 移除一個對手的子");
    Console.WriteLine("可以移除的位置: " + strRemove_possible);

    // Player's choice
    string toRemoveStr = "";
    Console.Write(player.Name + "選擇: ");
    toRemoveStr += Console.ReadLine();

    // Check input char length
    if (toRemoveStr.Length != 2 | !remove_choices_str.Contains(toRemoveStr))
    {
        Console.WriteLine("Invalid! 請重新輸入! \nPress Enter to Continue ......");
        Console.ReadKey();
        return false;
    }
    else
    {
        // Remove opponent piece
        int toRemoveX = axis_x_Dict[toRemoveStr[0]];
        int toRemoveY = axis_y_Dict[toRemoveStr[1]];

        // Change the choosen one to empty node.
        board[toRemoveY, toRemoveX].Contain = 'o';

        // If selection is in a mill, remove mill from list
        List<string> temp = new List<string>();
        foreach (string mill in Mill_list)
        {
            if (!mill.Contains(toRemoveStr))
            {
                temp.Add(mill);
            }
        }
        Mill_list = temp;

        // Decrease opponent's piece on board
        if (player == playerList[0])
        {
            playerList[1].piece_on_board -= 1;
        }
        else if (player == playerList[1])
        {
            playerList[0].piece_on_board -= 1;
        }

        return true;
    }
}

/* Main Program */
void main()
{
    // Controll game status
    bool game_over = false;

    // Mill checker
    bool isMill = false;

    while (!game_over)
    {
        foreach (var pl in playerList)
        {
            // Clear Console
            Console.Clear();

            // Print board
            show_Board(board);

            // First Phase : Player place pieces
            if (pl.N_piece > 0)
            {
                // Initialize input string
                string playerInput = "";

                // Check if player properly moved
                bool isPiecePlace = false;
                
                // Keep asking for right input
                while (!isPiecePlace)
                {
                    // Initialize input string
                    playerInput = "";

                    // Hint 
                    Console.Write(pl.Name + " (" + pl.color + ") " + " 行動, 放子(" + pl.N_piece + "): ");

                    // Handle input
                    playerInput += Console.ReadLine();

                    // Place piece
                    isPiecePlace = place_Piece(pl, playerInput);

                }

                // Update board
                Console.Clear();
                show_Board(board);

                // Report move
                Console.WriteLine(pl.Name + " 將棋子放置在 " + playerInput);
                Console.WriteLine("\n");

                // Check if a Mill is made
                isMill = check_Mill();

                // If a mill was made, remove an opponent's piece
                if (isMill)
                {
                    /* Player decide to remove */
                    bool isPieceRemoved = false;
                    while (!isPieceRemoved)
                    {
                        isPieceRemoved = Remove_opponent_piece(pl);
                    }
                }

            }
            
            // Second Phase: Player move pieces
            else
            {
                bool isPieceMoved = false;
                while (!isPieceMoved)
                {
                    // Complete a Move
                    isPieceMoved = move_Piece(pl);
                }

                // Check if a Mill is made
                isMill = check_Mill();

                // If a mill was made, remove an opponent's piece
                if (isMill)
                {
                    // Clear Console
                    Console.Clear();

                    // Print board
                    show_Board(board);

                    /* Player decide to remove */
                    bool isPieceRemoved = false;
                    while (!isPieceRemoved)
                    {
                        isPieceRemoved = Remove_opponent_piece(pl);
                    }
                }

            }

            // Decide Game status
            if (playerList[0].N_piece == 0 & playerList[1].N_piece == 0)
            {
                // Decide whether start sudden death
                if (playerList[0].piece_on_board == 3 & playerList[1].piece_on_board == 4 | playerList[1].piece_on_board == 3 & playerList[0].piece_on_board == 4)
                {
                    isSuddenDeath = true;
                }

                // sudden death count down
                if (isSuddenDeath)
                {
                    suddenDeath_round -= 1;
                }

                // sudden death count down to 0, game over
                if (suddenDeath_round == 0)
                {
                    // Draw & Game over
                    Console.WriteLine("已經用盡移動數, 遊戲結束, 平手");
                    Console.ReadKey();
                    game_over = true;
                    break;
                }

                // Decide whether game is over
                /* Player's piece on board < 3 will lose */
                if (pl == playerList[0] & playerList[1].N_piece == 0 & playerList[1].piece_on_board < 3)
                {
                    // Update board
                    Console.Clear();
                    show_Board(board);

                    // Win & Game over
                    Console.WriteLine(pl.Name + "(" + pl.color + ")" + " 獲勝 !");
                    Console.ReadKey();
                    game_over = true;
                    break;
                }
                else if (pl == playerList[1] & playerList[0].N_piece == 0 & playerList[0].piece_on_board < 3)
                {
                    // Update board
                    Console.Clear();
                    show_Board(board);

                    // Win & Game over
                    Console.WriteLine(pl.Name + "(" + pl.color + ")" + " 獲勝 !");
                    Console.ReadKey();
                    game_over = true;
                    break;
                }
            }
            
        }
    }
}

// Run this Game
main();

Console.Write("... Game Over ...");
Console.ReadKey();