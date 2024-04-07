using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    internal class GameBoard
    {
        // Board dimensions
        public const int board_width = 7;
        public const int board_heigh = 7;

        // Board axis names
        private char[] board_x = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        private char[] board_y = { '1', '2', '3', '4', '5', '6', '7' };

        // Axis dictionary
        public Dictionary<char, int> axis_x_Dict = new Dictionary<char, int>();
        public Dictionary<char, int> axis_y_Dict = new Dictionary<char, int>();

        // Empty node and Null node
        public const char emptyNode = 'o';
        public const char nullNode = '.';

        // Create a board
        public bNode[,] board = new bNode[board_width, board_heigh];

        // Get Game manager
        private GameObject gameManager;

        public GameBoard()
        {
            board = make_Board(board_width,board_heigh);
            gameManager = GameObject.Find("Game Manager");
            axis_x_Dict = axis_toDict(board_x);
            axis_y_Dict = axis_toDict(board_y);
        }

        // A function that check whether a mill was formed.
        public bool check_Mill()
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

            // Get Mill list
            List<string> Mill_list = gameManager.GetComponent<MainGame>().Mill_list;

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

        // A function make a board in back end.
        private bNode[,] make_Board(int width, int height)
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
                    for (int k = 0; k < blockList.Length; k++)
                    {
                        if (j == k || j == height - (k + 1))
                        {
                            if (blockList[k].Contains(i))
                            {
                                game_board[j, i] = new bNode(nullNode);
                            }
                            else
                            {
                                game_board[j, i] = new bNode(emptyNode);

                                // Record coordination
                                game_board[j, i].coordination[0] = board_x[i];
                                game_board[j, i].coordination[1] = board_y[j];
                            }
                        }
                    }
                }
            }

            // Connect the empty nodes Vertically
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (game_board[j, i].Contain == emptyNode)
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

            // Connect the emptyNode nodes Horizonally 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (game_board[j, i].Contain == emptyNode)
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

        // Make a dict for each axis to an integer
        private Dictionary<char, int> axis_toDict(char[] axis)
        {
            Dictionary<char, int> temp = new Dictionary<char, int>();
            for (int i = 0; i < axis.Length; i++)
            {
                temp.Add(axis[i], i);
            }
            return temp;
        }

        // A function to convert char array to int coordination
        public int[] ConverCoordination(char x, char y)
        {
            int[] coordination = new int[2];
            coordination[0] = axis_x_Dict[x];
            coordination[1] = axis_y_Dict[y];
            return coordination;
        }
    }
}
