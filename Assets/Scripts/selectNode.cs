using UnityEngine;

public class selectNode : MonoBehaviour
{

    // ������Node���y��
    public char axis_x;
    public char axis_y;

    // �O����U���Ѥl
    public GameObject currentPiece;

    // ������e��Pointer�H�b���Ӯ�����
    private GameObject currentPointer;

    // Pointer object
    public GameObject pointer;

    // �¤l�P�դl Object
    public GameObject black_piece;
    public GameObject white_piece;

    // ���Pointer���}��
    private bool show_pointer = false;

    // ����Node�O�_�T�w�Q���Ы��U
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

    // ��U�Ѥl���e��m
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

    // �N�Ѥl����
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

        // �T�{�I��������, �N�y�з�@input
        if (isClicked & isConfirmed)
        {
            switch (selection_mode)
            {
                case PLACE:
                    // ���� Place piece �ʧ@
                    gameManager.GetComponent<MainGame>().place_piece(axis_x, axis_y, gameObject);
                    break;

                case REMOVE:
                    // ���� Remove piece �ʧ@
                    gameManager.GetComponent<MainGame>().remove_piece(axis_x, axis_y, gameObject);
                    break;

                case MOVE:
                    // ���沾�ʴѤl�ʧ@: ��n���ʪ��X�l
                    gameManager.GetComponent<MainGame>().move_piece_from(axis_x, axis_y, gameObject);
                    break;

                case DEST:
                    // ���沾�ʴѤl�ʧ@: ��ت��a
                    gameManager.GetComponent<MainGame>().move_piece_to(axis_x, axis_y, gameObject);
                    break;
            }

            // ���� Click & Confirm �}��
            isClicked = false;
            isConfirmed = false;
        }

    }

    private void OnMouseOver()
    {
        // �ƹ�����, ���Pointer
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
        // �ƹ����}, ����Pointer
        if (show_pointer)
        {
            Destroy(currentPointer);
            show_pointer = false;
        }

        // �ƹ����}, ����isClick
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
