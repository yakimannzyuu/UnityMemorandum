using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public CarInput input = new CarInput();
    public Rigidbody RB{ get; private set; }
    public Transform Center{ get; private set; }
    public Wheel[] wheels{ get; private set; }
    [Header("CarSpecs")]
    public bool[] wheelDrives = new bool[4];
    public float[] wheelSteers = new float[4];
    void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Steer = wheelSteers[i] * input.Handle.x;

            if(wheelDrives[i])
            {
                wheels[i].Torque = (input.Accel - input.Brake) * 30;
            }
        }
    }

    void FixedUpdate()
    {
        // RB.AddForce(transform.forward * (input.Accel - input.Brake) * 10000);
        // RB.AddForce(transform.right * (input.Handle) * 10000);
    }

    public void Set()
    {
        gameObject.SetActive(true);
        RB = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<Wheel>();
        Center = transform.Find("Center");

        for(int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Set();
            wheels[i].BaseSpring = RB.mass / (float)wheels.Length;
        }
    }
}

public class CarInput
{
    float accel = 0;
    Vector2 handle = new Vector2();
    float brake = 0;
    float clutch = 1;
    int gear = 0;

    // タイヤの直径も計算内。
    public float[] GearRatioData = {-3.5f, 0.001f, 3.6f, 2f, 1f, 0.5f};

    public float Accel
    {
        get{ return accel; }
        set{ accel = Fn.Limit(value, 0, 1); }
    }

    public Vector2 Handle
    {
        get{ return handle; }
        set{ handle = new Vector2(Fn.Limit(value.x, -1, 1), Fn.Limit(value.y, -1, 1)); }
    }

    public float Brake
    {
        get{ return brake; }
        set{ brake = Fn.Limit(value, 0, 1); }
    }

    public float Clutch
    {
        get{ return clutch; }
        set{ clutch = Fn.Limit(value, 0, 1); }
    }

    public int Gear
    {
        get{ return gear; }
        set{ gear = Fn.Limit(value, -1, GearRatioData.Length -2); }
    }

    public float GearRatio
    {
        get{ return GearRatioData[gear + 1]; }
    }
}