using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour
{
    // 顯示遊戲進度, 使用 UI TextMeshPro
    public TMP_Text progress_logger;
    public TMP_Text player_logger;
    public TMP_Text p1_number;
    public TMP_Text p2_number;

    // Store players into player array
    private GameObject[] playerArray = new GameObject[2];
    private GameObject currentPlayer;

    // 顏色有黑白之分
    public const string BLACK = "Black";
    public const string WHITE = "White";

    // The function that shuffle players in array
    private void RandomizePlayers(GameObject[] playerList)
    {
        System.Random rand = new System.Random();
        int rand_n = rand.Next(2);
        if (rand_n > 0)
        {
            GameObject tmp = playerList[0];
            playerList[0] = playerList[1];
            playerList[1] = tmp;
        }

        // Assign color
        playerList[0].GetComponent<PlayerScript>().playerColor = BLACK;
        playerList[1].GetComponent<PlayerScript>().playerColor = WHITE;
        foreach (GameObject player in playerList)
        {
            char piece = player.GetComponent<PlayerScript>().playerColor[0];
            player.GetComponent<PlayerScript>().playerPiece = piece;
        }
    }

    // A flag that control game status
    private bool game_over = false;
    private bool sudden_death = false;
    private const int sudden_death_round = 10;
    private int remain_round;

    // A flag that control player's turn
    private bool player_turn = false;

    // A list to store mills
    public List<string> Mill_list = new List<string>();

    /* Selection mode: place, remove, move */
    public string selection_mode;
    public string phase_selecection_mode;
    public const string PLACE = "place";
    public const string REMOVE = "remove";
    public const string MOVE = "move";
    public const string DEST = "dest";
    public const string NONE = "none";

    // A flag that can toggle switch selection_mode
    public bool enter_Phase2 = false;

    // Make a board
    GameBoard game_board;
    bNode[,] board;

    /* To strore move from and to selected nodes*/
    public char[] move_from_coord;
    public GameObject move_from_node;

    // A function to control placement of a piece
    public void place_piece(char input_coord_x, char input_coord_y, GameObject selectedNode)
    {
       
        int[] input_coord = game_board.ConverCoordination(input_coord_x, input_coord_y);
        int moveX = input_coord[0];
        int moveY = input_coord[1];
        if (board[moveY,moveX].Contain == GameBoard.emptyNode)
        {
            // Place at as the inputPOS
            char playerPiece = currentPlayer.GetComponent<PlayerScript>().playerPiece;
            board[moveY, moveX].Contain = playerPiece;

            // Decrease player's piece number
            currentPlayer.GetComponent<PlayerScript>().N_piece -= 1;

            // Increase player's piece on board
            currentPlayer.GetComponent<PlayerScript>().piece_on_board += 1;

            // Display pieces on node
            string playerColor = currentPlayer.GetComponent<PlayerScript>().playerColor;
            selectedNode.GetComponent<selectNode>().display_piece(playerColor);

            // Check for mill
            bool isMill = game_board.check_Mill();
            
            // If a mill was made, switch to remove mode
            if (isMill)
            {
                string playerName = currentPlayer.GetComponent<PlayerScript>().playerName;
                Debug.Log(playerName + "(" + playerColor + ")" + " 做成Mill 進入 Remove mode");
                progress_logger.text = playerColor + " Mill, Remove opponent's piece.";
                selection_mode = REMOVE;
            }
            else
            {
                player_turn = true;
            }

        }
        else
        {
            Debug.Log("不能放該位置");
            progress_logger.text = "In valide position!";
        }
    }

    // A function to control removal of a piece
    public void remove_piece(char input_coord_x, char input_coord_y, GameObject selectedNode)
    {
        // A collection for possible chooices
        List<char[]> remove_choices = new List<char[]>();
        List<string> remove_choices_str = new List<string>();

        // List to check
        char playerPiece = currentPlayer.GetComponent<PlayerScript>().playerPiece;
        char[] check_list = new char[] { GameBoard.emptyNode, GameBoard.nullNode, playerPiece };

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

        // Handle inputs
        int[] input_coord = game_board.ConverCoordination(input_coord_x, input_coord_y);
        string toRemoveStr = input_coord_x.ToString() + input_coord_y.ToString();
        int removeX = input_coord[0];
        int removeY = input_coord[1];

        if (!remove_choices_str.Contains(toRemoveStr))
        {
            Debug.Log("不能選擇移除該位置!");
            progress_logger.text = "In valide position!";
            return;
        }
        else
        {
            // Report Player's choice
            string playerName = currentPlayer.GetComponent<PlayerScript>().playerName;
            string playerColor = currentPlayer.GetComponent<PlayerScript>().playerColor;
            Debug.Log(playerName + "(" + playerColor + ")" + " 選擇: " + toRemoveStr);
            progress_logger.text = playerName + "(" + playerColor + ")" + " Remove : " + toRemoveStr;

            // Change the choosen one to empty node.
            board[removeY, removeX].Contain = GameBoard.emptyNode;

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
            GameObject opponent = currentPlayer.GetComponent<PlayerScript>().opponent;
            opponent.GetComponent<PlayerScript>().piece_on_board -= 1;

            // Delect piece on node
            selectedNode.GetComponent<selectNode>().delete_piece();

            selection_mode = phase_selecection_mode;
            player_turn = true;
        }
        
    }

    // A function to control movement of a piece from 
    public void move_piece_from(char input_coord_x, char input_coord_y, GameObject selectedNode)
    {
        // State Controller
        bool isSelectTargetOK = false;

        // Handle inputs
        int[] input_coord = game_board.ConverCoordination(input_coord_x, input_coord_y);
        int move_from_X = input_coord[0];
        int move_from_Y = input_coord[1];

        // Validate selection
        bNode selected = board[move_from_Y, move_from_X];
        char playerPiece = currentPlayer.GetComponent<PlayerScript>().playerPiece;
        if (selected.Contain == playerPiece)
        {
            int free_Node = 0;
            foreach (bNode node in selected.Connected)
            {
                if (node.Contain == GameBoard.emptyNode)
                {
                    free_Node++;
                }
            }
            if (free_Node >= 1)
            {
                isSelectTargetOK = true;
            }
            else
            {
                Debug.Log("無路可走, 請重新選擇");
                progress_logger.text = "In valide position!";
            }
        }
        else
        {
            Debug.Log("不是自己的棋子, 請重新選擇");
            progress_logger.text = "In valide piece!";
        }

        if (isSelectTargetOK)
        {
            selection_mode = DEST;
            move_from_coord = new char[] { input_coord_x, input_coord_y };
            move_from_node = selectedNode;
        }
    }

    // A function to control movement of a piece to
    public void move_piece_to(char input_coord_x, char input_coord_y, GameObject selectedNode)
    {
        // State Controller
        bool isSelectDestOK = false;

        // Handle from inputs 
        int[] input_from_coord = game_board.ConverCoordination(move_from_coord[0], move_from_coord[1]);
        int move_from_X = input_from_coord[0];
        int move_frpm_Y = input_from_coord[1];
        bNode selected_target = board[move_frpm_Y, move_from_X];

        // Handle to inputs
        int[] input_to_coord = game_board.ConverCoordination(input_coord_x, input_coord_y);
        int move_to_X = input_to_coord[0];
        int move_to_Y = input_to_coord[1];
        bNode selected_dist = board[move_to_Y, move_to_X];

        // Validate input
        if (selected_target.Connected.Contains(selected_dist) & selected_dist.Contain == GameBoard.emptyNode)
        {
            isSelectDestOK = true;
        }
        else if (selected_target.coordination == selected_dist.coordination)
        {
            // Switch selection mode to MOVE
            selection_mode = phase_selecection_mode;
            Debug.Log("> 輸入同一個位置, 重新選擇要移動的棋子 <");
            progress_logger.text = "Please re-select.";
            return ;
        }
        else
        {
            int remain_Piece = currentPlayer.GetComponent<PlayerScript>().piece_on_board;
            if (remain_Piece == 3)
            {
                isSelectDestOK = true;
            }
            else
            {
                Debug.Log("無法移動到該位置, 請重新選擇");
                progress_logger.text = "In valide position!";
            }
        }

        if (isSelectDestOK)
        {
            // Change Display of node
            if (move_from_node)
            {
                move_from_node.GetComponent<selectNode>().delete_piece();
            }
            string playerColor = currentPlayer.GetComponent<PlayerScript>().playerColor;
            selectedNode.GetComponent<selectNode>().display_piece(playerColor);

            // Change node contanins
            selected_target.Contain = GameBoard.emptyNode;
            selected_dist.Contain = currentPlayer.GetComponent<PlayerScript>().playerPiece;

            // If selection is in a mill, remove mill from list
            string playerMoveInput = move_from_coord[0].ToString() + move_from_coord[1].ToString();
            List<string> temp = new List<string>();
            foreach (string mill in Mill_list)
            {
                if (!mill.Contains(playerMoveInput))
                {
                    temp.Add(mill);
                }
            }
            Mill_list = temp;

            // Initialize move_from_coord
            move_from_coord = new char[] { };
            move_from_node = null;

            // Check for mill
            bool isMill = game_board.check_Mill();

            // If a mill was made, switch to remove mode
            if (isMill)
            {
                string playerName = currentPlayer.GetComponent<PlayerScript>().playerName;
                Debug.Log(playerName + "(" + playerColor + ")" + " 做成Mill 進入 Remove mode");
                progress_logger.text = playerName + "(" + playerColor + ")" + " Mill, Remove mode";
                selection_mode = REMOVE;
            }
            else
            {
                // Switch player and selection mode
                selection_mode = phase_selecection_mode;
                player_turn = true;
            }

            // If sudden death, remain round decrease
            if (sudden_death)
            {
                remain_round -= 1;
            }

        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug
        print("遊戲開始");
        progress_logger.text = "Game start!";

        // Make a board
        game_board = new GameBoard();
        board = game_board.board;

        // Join players to Game
        GameObject playerA = GameObject.Find("playerA");
        GameObject playerB = GameObject.Find("playerB");
        playerArray[0] = playerA; playerArray[1] = playerB;

        // Randomly decide who plays first hand
        RandomizePlayers(playerArray);
        currentPlayer = playerArray[0];

        // Set opponents
        playerArray[0].GetComponent<PlayerScript>().opponent = playerArray[1];
        playerArray[1].GetComponent<PlayerScript>().opponent = playerArray[0];

        // Selection mode will start with place
        phase_selecection_mode = PLACE;
        selection_mode = phase_selecection_mode;

        // Game status
        remain_round = sudden_death_round;

        // UI 顯示換哪一位玩家先
        string player_message = currentPlayer.name + "'s Turn";
        player_logger.text = player_message;

        // UI 顯示剩下的棋子數
        p1_number.text = "Player1:9/0";
        p2_number.text = "Player2:9/0";
    }

    // Update is called once per frame
    void Update()
    {
        /* 控制遊戲結束 */
        if (game_over)
        {
            phase_selecection_mode = NONE;
            selection_mode = phase_selecection_mode;
        }
        else
        {
            /* 控制玩家回合 */
            if (player_turn)
            {
                if (currentPlayer == playerArray[0])
                {
                    currentPlayer = playerArray[1];
                }
                else if (currentPlayer == playerArray[1])
                {
                    currentPlayer = playerArray[0];
                }
                player_turn = false;

                // UI 顯示換哪一位玩家
                string player_message = currentPlayer.name + "'s Turn";
                player_logger.text = player_message;
            }

            /*控制遊戲進展*/
            int player1_remain_Piece = playerArray[0].GetComponent<PlayerScript>().N_piece;
            int player2_remain_Piece = playerArray[1].GetComponent<PlayerScript>().N_piece;

            // 計算盤面上剩餘的棋子
            int player1_Piece_on_board = playerArray[0].GetComponent<PlayerScript>().piece_on_board;
            int player2_Piece_on_board = playerArray[1].GetComponent<PlayerScript>().piece_on_board;

            // UI 顯示剩下的棋子數
            p1_number.text = $"Player1:{player1_remain_Piece}/{player1_Piece_on_board}";
            p2_number.text = $"Player2:{player2_remain_Piece}/{player2_Piece_on_board}";

            // 用盡 remain_Piece, 進入Phase2
            if (player1_remain_Piece == 0 & player2_remain_Piece == 0 & selection_mode != REMOVE)
            {
                phase_selecection_mode = MOVE;

                /* Toggle switch selection_mode to MOVE */
                if (!enter_Phase2)
                {
                    selection_mode = phase_selecection_mode;
                    enter_Phase2 = true;
                }

                // UI 顯示進入第二階段
                Debug.Log("進入第二階段,請移動棋子");
                progress_logger.text = "Enter phase II, Please move pieces.";

            }

            // Phase2 進入 sudden death
            if (enter_Phase2)
            {
                // Turn on sudden death count down
                if (player1_Piece_on_board == 3 & player2_Piece_on_board == 4 | player1_Piece_on_board == 4 & player2_Piece_on_board == 3)
                {
                    // Debug.Log("陷入僵局, 倒數10次移動結束");
                    sudden_death = true;
                }

                // Draw the game if there is no remain round anymore.
                if (remain_round == 0)
                {
                    Debug.Log("已經用盡移動數, 遊戲結束, 平手");
                    progress_logger.text = "Run out of steps, Game Over. Tie!";
                    game_over = true;
                }

                // Decide who win the game. (Pieces on board < 3 will lose)
                if (player1_Piece_on_board < 3)
                {
                    GameObject winnerOBJ = playerArray[1];
                    string winner = winnerOBJ.GetComponent<PlayerScript>().playerName;
                    string win_color = winnerOBJ.GetComponent<PlayerScript>().playerColor;
                    Debug.Log(winner + "(" + win_color + ")" + " 獲勝 ! 遊戲結束");
                    progress_logger.text = winner + "(" + win_color + ")" + " Win ! Game over.";
                    game_over = true;
                }
                else if (player2_Piece_on_board < 3)
                {
                    GameObject winnerOBJ = playerArray[0];
                    string winner = winnerOBJ.GetComponent<PlayerScript>().playerName;
                    string win_color = winnerOBJ.GetComponent<PlayerScript>().playerColor;
                    Debug.Log(winner + "(" + win_color + ")" + " 獲勝 ! 遊戲結束");
                    progress_logger.text = winner + "(" + win_color + ")" + " Win ! Game over.";
                    game_over = true;
                }
            }
        }
       
    }
}
