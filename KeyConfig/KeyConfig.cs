using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020年5月10日
// キー設定用クラス。
/*  i = KeyConfig.GetButton(Fire1);
 *  i > 0   True
 *  i < 0   KeyUp
 *  i == 0  Flase
 *  i > 2   KeyDown
 *  i == 2  LongTap
 *  i == 4  DoubleTap
 */

public class KeyConfig
{
    public static readonly string filename = "KeyConfig";
    static KeyConfig instance;

    public float DoubleTapTime = 0.2f;
    public float LongTapTime = 1.0f;
    public float LongTapNextTime = 0.3f;

    [SerializeField]
    float[] prevTapTime = { };
    KeyCode[] keyCodes = { };

    //public
    public static int GetButton(Key key)
    {
        int i = 0;
        if (Input.GetKey(instance.keyCodes[(int)key]))
        {
            if (Input.GetKeyDown(instance.keyCodes[(int)key]))
            {
                if (instance.prevTapTime[(int)key] < 0 && Time.realtimeSinceStartup + instance.prevTapTime[(int)key] - instance.DoubleTapTime < 0)
                {
                    i = 4;
                }
                else
                {
                    i = 3;
                }
                instance.prevTapTime[(int)key] = Time.realtimeSinceStartup + instance.DoubleTapTime;
            }
            else
            {
                if (instance.prevTapTime[(int)key] > 0 && Time.realtimeSinceStartup - instance.prevTapTime[(int)key] > 0)
                {
                    instance.prevTapTime[(int)key] += instance.LongTapNextTime;
                    i = 2;
                }
                else
                {
                    i = 1;
                }
            }
        }
        else if (Input.GetKeyUp(instance.keyCodes[(int)key]))
        {
            i = -1;
            instance.prevTapTime[(int)key] = -Time.realtimeSinceStartup;
        }
        return i;
    }

    // static
    static public void Save()
    {
        FileManager.Instance.SaveFile<KeyConfig>(instance, filename);
    }

    static public void Set()
    {
        if (!FileManager.Instance.LoadFile<KeyConfig>(ref instance, filename))
        {
            instance = new KeyConfig();
        }
    }

    //

    KeyConfig()
    {
        prevTapTime = new float[System.Enum.GetNames(typeof(Key)).Length];
        KeyCode[] tempCodes =
            {
                KeyCode.Mouse0,
                KeyCode.Mouse1
            };
        keyCodes = tempCodes;
    }

    public enum Key
    {
        mouse0,
        mouse1
    }
}
