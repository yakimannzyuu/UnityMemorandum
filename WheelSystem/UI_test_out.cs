using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_test_out : MonoBehaviour
{
    public Text text;
    static public UI_test_out Instance{ get; private set; }

    public string Txt;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(!text)
        {
            text = GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Txt != "")
        {
            txt = Txt;
            Txt = "";
        }
    }

    string txt
    {
        get{return text.text; }
        set{ text.text = value; }
    }
}
