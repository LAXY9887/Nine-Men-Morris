using UnityEngine;

public class selectNode : MonoBehaviour
{

    // 紀錄該Node的座標
    public char axis_x;
    public char axis_y;

    // 記錄當下的棋子
    public GameObject currentPiece;

    // 紀錄當前的Pointer以在未來消滅它
    private GameObject currentPointer;

    // Pointer object
    public GameObject pointer;

    // 黑子與白子 Object
    public GameObject black_piece;
    public GameObject white_piece;

    // 顯示Pointer之開關
    private bool show_pointer = false;

    // 控制Node是否確定被鼠標按下
    private bool isClicked = false;
    private bool isConfirmed = false;

    // GameManager
    private GameObject gameManager;

    // Get selection mode choices
    private string selection_mode;
    private const string PLACE = MainGame.PLACE;
    private const string REMOVE = MainGame.REMOVE;
    private const string MOVE = MainGame.MOVE;
    private const string DEST = MainGame.DEST;

    // 放下棋子到當前位置
    public void display_piece(string piece_color)
    {

        switch (piece_color)
        {
            case MainGame.BLACK:
                currentPiece = Instantiate(black_piece, transform.position, Quaternion.identity);
                break;

            case MainGame.WHITE:
                currentPiece = Instantiate(white_piece, transform.position, Quaternion.identity);
                break;
        }

    }

    // 將棋子移除
    public void delete_piece()
    {
        Destroy(currentPiece);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get game manager
        gameManager = GameObject.Find("Game Manager");

    }

    // Update is called once per frame
    void Update()
    {
        // Get selection mode
        selection_mode = gameManager.GetComponent<MainGame>().selection_mode;

        // 確認點擊完成後, 將座標當作input
        if (isClicked & isConfirmed)
        {
            switch (selection_mode)
            {
                case PLACE:
                    // 執行 Place piece 動作
                    gameManager.GetComponent<MainGame>().place_piece(axis_x, axis_y, gameObject);
                    break;

                case REMOVE:
                    // 執行 Remove piece 動作
                    gameManager.GetComponent<MainGame>().remove_piece(axis_x, axis_y, gameObject);
                    break;

                case MOVE:
                    // 執行移動棋子動作: 選要移動的旗子
                    gameManager.GetComponent<MainGame>().move_piece_from(axis_x, axis_y, gameObject);
                    break;

                case DEST:
                    // 執行移動棋子動作: 選目的地
                    gameManager.GetComponent<MainGame>().move_piece_to(axis_x, axis_y, gameObject);
                    break;
            }

            // 關閉 Click & Confirm 開關
            isClicked = false;
            isConfirmed = false;
        }

    }

    private void OnMouseOver()
    {
        // 滑鼠指到, 顯示Pointer
        if (!show_pointer)
        {
            SpriteRenderer renderer = pointer.GetComponent<SpriteRenderer>();
            // Change color when in different selection modes
            switch (selection_mode)
            {
                case PLACE:
                    renderer.color = new Color(0, 0, 255);
                    break;

                case REMOVE:
                    renderer.color = new Color(255, 0, 0);
                    break;

                case MOVE:
                    renderer.color = new Color(0,255,0);
                    break;

                case DEST:
                    renderer.color = new Color(255, 0, 255);
                    break;

            }
            currentPointer = Instantiate(pointer, transform.position, Quaternion.identity);
            show_pointer = true;
        }

        // Mouse UP within node to confirm the click
        if (isClicked & Input.GetMouseButtonUp(0))
        {
            isConfirmed = true;
        }
    }

    private void OnMouseExit()
    {
        // 滑鼠離開, 關閉Pointer
        if (show_pointer)
        {
            Destroy(currentPointer);
            show_pointer = false;
        }

        // 滑鼠離開, 關閉isClick
        if (isClicked)
        {
            isClicked = false;
        }
    }

    private void OnMouseDown()
    {
        // Mouse Click
        if (!isClicked)
        {
            isClicked = true;
        }
    }

}
