using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 車の統括（プレファブ生成等）
public class UnitManager : MonoBehaviour
{
    static public UnitManager Instance{ get; private set; }

    [Header("WheelObj")]
    public GameObject[] WheelObj;

    void Awake()
    {
        if(Instance){ Debug.LogError("シングルトンが複数あります。", transform);}
        else{ Instance = this; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateCar()
    {

    }

    public Transform CreateWheelObj()
    {
        return Instantiate(WheelObj[0]).transform;
    }
}
