using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // ���a���W�r
    public string playerName;

    // ���a���Ѥl�C��
    public string playerColor;
    public char playerPiece;

    // ���a�i�H��m���Ѥl�ƥ�
    public int N_piece;

    // �`�@ 9 ���Ѥl
    private const int Total_Piece = 5;

    // ���a�b�L�W�Ѥl���ƥ�
    public int piece_on_board;

    // ���
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
