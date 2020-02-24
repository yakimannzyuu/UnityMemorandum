using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オリジナルWheelCollider
// 地面は停止しているものとみなす。
public class Wheel : MonoBehaviour
{
    Transform Appearance;
    Rigidbody rb;

    [Header("Spec")]
    public float Radium = 0.1f;
    public float Mass = 10;

    // タイヤの位置（高さ）。原点は車体が水平時のタイヤ接地面の高さ。
    float hight = 0;
    public float HightLimit = 0.1f;
    float hightSpeed = 0;

    [Header("suspension( *Mass kg)")]
    public float Spring = 100;
    public float BaseSpring = 10;
    public float Dampar = 10;

    [Header("input")]
    public float AppSteer = 0;
    public float Steer = 0;
    public float Torque = 0;

    float wheelAngle = 0;
    public float RPM{ get; private set; }

    public float GripPow = 10;
    public float GripLimit = 1;

    void Update()
    {
        Appearance.localPosition = Vector3.up * ( Radium - hight );
        Appearance.localRotation = Quaternion.AngleAxis(Steer + AppSteer, Vector3.up) * Quaternion.AngleAxis(wheelAngle * 1200, Vector3.right);
    }

    void FixedUpdate()
    {
        // タイヤ計算（速度
        RPM += Torque / Mass;
        wheelAngle += RPM * Time.fixedDeltaTime;

        Vector3 pow = new Vector3();// ワールド軸に車体が受ける力（タイヤ接地面から）

        hight += hightSpeed * Time.fixedDeltaTime;

        Ray ray = new Ray(transform.position + transform.up * (Radium - hight), - transform.up);
        // 地面にぶつかった時。
        if(Physics.Raycast(ray, out RaycastHit hit, Radium))
        {
            // ワールド軸→サスペンション軸に変換するQuat。
            Quaternion rotation = Quaternion.Inverse(transform.rotation);

            // 地面軸に回転するQuat
            Quaternion normalToLand = Quaternion.LookRotation(hit.normal, Quaternion.AngleAxis(Steer, Vector3.up) * transform.forward);

            // 地面と同じ速度にする速度変化量。
            float normalDrug = Mathf.Min(0, (rotation * rb.GetPointVelocity(hit.point) ).y - hightSpeed);
            hightSpeed += normalDrug;
            // hightSpeed = Mathf.Min(0, (rotation * rb.GetPointVelocity(hit.point) ).y);
            // UI_test_out.Instance.Txt += transform.name + ":" + normalDrug.ToString() + "\n";
            // 次のfixフレームの高さを計算。
            hight = ( rotation * (transform.position - hit.point) ).y + hightSpeed * Time.fixedDeltaTime;

            // Debug.DrawRay(transform.position, normal * new Vector3(1, 0, 0), Color.green, 0, false);
            // pow = Vector3.Scale( Quaternion.Inverse(normalToLand) * ( rb.GetPointVelocity(hit.point) * normalDrug * GripPow ) , new Vector3(1, 1, 0) );

            // ワールド軸で（速度+垂直抗力）
            pow = rb.GetPointVelocity(hit.point) - transform.up * normalDrug * 2;
            // pow.y = -normalDrug;
            UI_test_out.Instance.Txt += transform.name + ":" + normalDrug.ToString() + "\n";

            // ここから地面軸。
            pow =  Quaternion.Inverse(normalToLand) * pow;
            pow.z = Mathf.Max(pow.z, 0);
            pow.x = Fn.Limit(pow.x * -pow.z * GripPow,  - GripLimit, GripLimit);
            float rpmDist = Fn.Limit(pow.y - RPM * 2 * Radium * Mathf.PI * -pow.z * GripPow, -GripLimit, GripLimit);
            pow.y = rpmDist;
            RPM -= rpmDist / Mass;
            pow.z = 0;

            // ワールド軸に戻す。
            pow = normalToLand * pow;
            Debug.DrawRay(transform.position, pow, Color.green, 0, false);
        }

        // サスペンションの力。
        float springPow = (hight * Spring + hightSpeed * Dampar - BaseSpring) * Time.fixedDeltaTime;
        hightSpeed -= springPow / Mass;

        // サスペンション可動域、下限になった時。
        if(hight < -HightLimit){
            springPow += hightSpeed;
            hightSpeed = 0;
            hight = -HightLimit;
        }
        // サスペンション可動域、上限になった時。
        if(hight > HightLimit){
            springPow += hightSpeed;
            hightSpeed = 0;
            hight = HightLimit;
        }

        rb.AddForceAtPosition( - transform.up * springPow * Mass + pow, transform.position + transform.up * hight, ForceMode.Impulse);
    }

    public void Set()
    {
        Appearance = UnitManager.Instance.CreateWheelObj();
        if(Appearance){
            Appearance.SetParent(transform);
            Update();
        }
        else{this.enabled = false;}

        rb = GetComponentInParent<Rigidbody>();
    }
}

/*
摩擦系sあんメモ

地面(hit.normal)に対して　垂直抗力と水平摩擦を計算。

手順。
1：垂直抗力を計算。
2：タイヤの方向から見た、地面とタイヤの速度差を計算。

予想以上に摩擦計算が難しい。
まず車輪接地面速度を計算、タイヤ軸に直してから速度差を出す。垂直抗力と速度差を地面法線軸に直して判定。そこからワールド軸に変換すれば力が出ると思うんだがなぁ…。

軸の種類
地面の法線方向：摩擦計算+垂直抗力の計算。
タイヤの向き：タイヤの速度計算に使う。
ワールド軸：AddForceに使う。

normalQuat * wheelQuat * hit.normal > ワールド座標から見た

タイヤの状態は2種類。
固定(静止摩擦)：速度差に比例。
動（動摩擦）：垂直抗力*速度差*動摩擦係数。
垂直抗力は一定。
*/