using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameProgress_logger : MonoBehaviour
{

    public TMP_Text text_contain;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化顯示
        text_contain.text = "Debug";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
