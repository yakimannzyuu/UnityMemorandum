using UnityEngine;

[System.Serializable] // Unity inspectorで見れるように。
public class Matrix21
{
    // 縦横の順に行列　値は二つ
    public float m11 = 0;
    public float m21 = 0; // protected

    // get set　でベクトルと変換可能
    public Vector2 vector{
        get{return new Vector2(m11,m21);}
        set{m11 = value.x; m21 = value.y;}
    }

    // デフォルトコンストラクタ　初期化するやつ
    public Matrix21(){
        m11 = 0;
        m21 = 0;
    }
    // コンストラクタのオーバーロード？
    public Matrix21(float _11, float _21){
        m11 = _11;
        m21 = _21;
    }

    // operator 型同士の計算式を定義できる。Vector2 + Vector2 のように。
    public static Matrix21 operator+ (Matrix21 _x, Matrix21 _y){
        return new Matrix21(_x.m11 + _y.m11, _x.m21 + _y.m21);
    }

    public static Matrix21 operator- (Matrix21 _x, Matrix21 _y){
        return new Matrix21(_x.m11 - _y.m11, _x.m21 - _y.m21);
    }

    public static Matrix21 operator* (Matrix21 _x, float _y){
        return new Matrix21(_x.m11 * _y, _x.m21 + _y);
    }
}

[System.Serializable]
public class Matrix22 : Matrix21
{
    // 保持値は合計4つ。右上はm12。
    public float m12;
    public float m22;

    // デフォルトコンストラクタ
    public Matrix22(){
        m11 = 0;
        m12 = 0;
        m21 = 0;
        m22 = 0;
    }

    public Matrix22(float _11, float _12, float _21, float _22){
        m11 = _11;
        m12 = _12;
        m21 = _21;
        m22 = _22;
    }
    
    // operator
    public static Matrix22 operator+ (Matrix22 _x, Matrix22 _y){
        return new Matrix22(_x.m11 + _y.m11, _x.m12 + _y.m12, _x.m21 + _y.m21, _x.m22 + _y.m22);
    }

    public static Matrix22 operator- (Matrix22 _x, Matrix22 _y){
        return new Matrix22(_x.m11 - _y.m11, _x.m12 - _y.m12, _x.m21 - _y.m21, _x.m22 - _y.m22);
    }

    public static Matrix22 operator* (Matrix22 _x, Matrix22 _y){
        return new Matrix22(_x.m11 * _y.m11 + _x.m12 * _y.m21, _x.m11 * _y.m12 + _x.m12 * _y.m22, _x.m21 * _y.m11 + _x.m22 * _y.m21, _x.m21 * _y.m12 + _x.m22 * _y.m22);
    }

    public static Matrix21 operator* (Matrix22 _x, Matrix21 _y){// オーバーロード　
        return new Matrix21(_x.m11 * _y.m11 + _x.m12 * _y.m21, _x.m21 * _y.m11 + _x.m22 * _y.m21);
    }

    // square matrix　(get
    public static Matrix22 zero{ // 零行列
        get{return new Matrix22(0, 0, 0, 0);}
    }
    public static Matrix22 identity{ // 単位行列
        get{return new Matrix22(1, 0, 0, 1);}
    }
}

// Matrix 3x1
[System.Serializable]
public class Matrix31 : Matrix21
{
    public float m31;

    // コンストラクタ
    public Matrix31()
    {
        m11 = 0;
        m21 = 0;
        m31 = 0;
    }
    public Matrix31(float _m11, float _m21, float _m31)
    {
        m11 = _m11;
        m21 = _m21;
        m31 = _m31;
    }

    // get
    public Vector3 vector
    {　// オーバーロード
        get{return new Vector3(m11, m21, m31); }
    }

    // operator
    public static Matrix31 operator+ (Matrix31 _x, Matrix31 _y){
        return new Matrix31(_x.m11 + _y.m11, _x.m21 + _y.m21, _x.m31 + _y.m31);
    }

    public static Matrix31 operator- (Matrix31 _x, Matrix31 _y){
        return new Matrix31(_x.m11 - _y.m11, _x.m21 - _y.m21, _x.m31 - _y.m31);
    }

    public static Matrix31 operator* (Matrix31 _x, Matrix31 _y){
        return new Matrix31(_x.m11 * _y.m11, _x.m21 * _y.m21, _x.m31 * _y.m31);
    }
}

// matrix 33
[System.Serializable]
public class Matrix33 : Matrix31
{
    // 合計9。ここで6つ追加。右上はm13。
    public float m12;
    public float m22;
    public float m32;
    public float m13;
    public float m23;
    public float m33;

    //デフォルトコンストラクタ。
    public Matrix33(){
        m11 = 0;
        m21 = 0;
        m31 = 0;
        m12 = 0;
        m22 = 0;
        m32 = 0;
        m13 = 0;
        m23 = 0;
        m33 = 0;
    }

    public Matrix33(float _m11, float _m12, float _m13, 
    float _m21, float _m22, float _m23, 
    float _m31, float _m32, float _m33){
        m11 = _m11;
        m21 = _m21;
        m31 = _m31;
        m12 = _m12;
        m22 = _m22;
        m32 = _m32;
        m13 = _m13;
        m23 = _m23;
        m33 = _m33;
    }
    
    // operator
    public static Matrix33 operator+ (Matrix33 _x, Matrix33 _y){
        return new Matrix33(_x.m11 + _y.m11, _x.m12 + _y.m12, _x.m13 + _y.m13,
             _x.m21 + _y.m21, _x.m22 + _y.m22, _x.m23 + _y.m23,
             _x.m31 + _y.m31, _x.m32 + _y.m32, _x.m33 + _y.m33);
    }

    public static Matrix33 operator- (Matrix33 _x, Matrix33 _y){
        return new Matrix33(_x.m11 - _y.m11, _x.m12 - _y.m12, _x.m13 - _y.m13,
             _x.m21 - _y.m21, _x.m22 - _y.m22, _x.m23 - _y.m23,
             _x.m31 - _y.m31, _x.m32 - _y.m32, _x.m33 - _y.m33);
    }

    public static Matrix33 operator* (Matrix33 _x, Matrix33 _y){
        return new Matrix33(_x.m11*_y.m11 + _x.m12*_y.m21 + _x.m13*_y.m31,
            _x.m11*_y.m12 + _x.m12*_y.m22 + _x.m13*_y.m32,
            _x.m11*_y.m13 + _x.m12*_y.m23 + _x.m13*_y.m33,
            _x.m21*_y.m11 + _x.m22*_y.m21 + _x.m23*_y.m31,
            _x.m21*_y.m12 + _x.m22*_y.m22 + _x.m23*_y.m32,
            _x.m21*_y.m13 + _x.m22*_y.m23 + _x.m23*_y.m33,
            _x.m31*_y.m11 + _x.m32*_y.m21 + _x.m33*_y.m31,
            _x.m31*_y.m12 + _x.m32*_y.m22 + _x.m33*_y.m32,
            _x.m31*_y.m13 + _x.m32*_y.m23 + _x.m33*_y.m33
            );
    }

    public static Matrix31 operator* (Matrix33 _x, Matrix31 _y){
        return new Matrix31(_x.m11*_y.m11 + _x.m12*_y.m11 + _x.m13*_y.m11,
            _x.m11*_y.m21 + _x.m12*_y.m21 + _x.m13*_y.m21,
            _x.m11*_y.m31 + _x.m12*_y.m31 + _x.m13*_y.m31
            );
    }

    // square matrix　(get
    public static Matrix33 zero{ // 零行列
        get{return new Matrix33(0, 0, 0, 0, 0, 0, 0, 0, 0);}
    }
    public static Matrix33 identity{ // 単位行列
        get{return new Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);}
    }
}