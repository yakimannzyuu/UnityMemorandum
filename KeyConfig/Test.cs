using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyConfig.GetButton(KeyConfig.Key.action) > 0)
        {
            Debug.Log("call!");
        }

        if (KeyConfig.GetButton(KeyConfig.Key.up) > 1)
        {
            Debug.Log(Time.frameCount + "up:" + KeyConfig.GetButton(KeyConfig.Key.up));
        }
        switch(KeyConfig.GetButton(KeyConfig.Key.up))
        {
            case 0:
                image.color = Color.black;
                break;
            case -1:
                image.color = Color.red;
                break;
            case 1:
                image.color = Color.blue;
                break;
            case 2:
                image.color = Color.yellow;
                break;
            case 3:
                image.color = Color.green;
                break;
            case 4:
                image.color = Color.white;
                break;

        }
    }
}
