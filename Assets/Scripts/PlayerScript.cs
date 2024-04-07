using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // 玩家的名字
    public string playerName;

    // 玩家的棋子顏色
    public string playerColor;
    public char playerPiece;

    // 玩家可以放置的棋子數目
    public int N_piece;

    // 總共 9 顆棋子
    private const int Total_Piece = 5;

    // 玩家在盤上棋子的數目
    public int piece_on_board;

    // 對手
    public GameObject opponent;

    // Start is called before the first frame update
    void Start()
    {
        N_piece = Total_Piece;
        piece_on_board = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
