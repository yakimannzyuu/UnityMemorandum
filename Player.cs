using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プレイヤ―入出力担当。
public class Player : MonoBehaviour
{
    public Text statusText;
    public Car MyCar;

    public float MoveSpeed = 1;
    public float Length = 10;
    public float Hight = 3;
    // Start is called before the first frame update
    void Start()
    {
        if(MyCar)
        {
            MyCar.Set();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MyCar)
        {
            MyCar.input.Accel = Input.GetAxis("Vertical");
            MyCar.input.Brake = -Input.GetAxis("Vertical");
            MyCar.input.Handle = new Vector2(Input.GetAxis("Horizontal"), 0);

            if(Input.GetKeyDown(KeyCode.KeypadPlus)){ MyCar.input.Gear++;}
            if(Input.GetKeyDown(KeyCode.KeypadMinus)){ MyCar.input.Gear--;}

            if(statusText)
            {
                statusText.text = "CarStatus\n";
                statusText.text += "Accel:" + MyCar.input.Accel.ToString() + "\n";
                statusText.text += "Brake:" + MyCar.input.Brake.ToString() + "\n";
                statusText.text += "Handle:" + MyCar.input.Handle.ToString() + "\n";
                statusText.text += "Gear:" + MyCar.input.Gear.ToString() + " ratio:" + MyCar.input.GearRatio.ToString() + "\n";
                statusText.text += "Clutch:" + MyCar.input.Clutch.ToString() + "\n";
            }
        }
    }

    void LateUpdate()
    {
        CameraMove();
    }

    void CameraMove()
    {
        if(MyCar)
        {
            transform.LookAt(MyCar.transform.position);

            Vector3 dist = MyCar.transform.position - transform.position;
            float length = Vector3.Scale(dist, new Vector3(1, 0, 1)).magnitude;

            transform.position += (Mathf.Max(0, length - Length) * dist.normalized + Vector3.up * (dist.y + Hight)) * MoveSpeed * Time.deltaTime;
        }
    }
}
