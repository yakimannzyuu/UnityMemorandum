using UnityEngine;

public class Fn
{
    public static float Limit(float x, float min, float max)
    {
        return Mathf.Max(min, Mathf.Min(max, x));
    }
    public static int Limit(int x, int min, int max)
    {
        return Mathf.Max(min, Mathf.Min(max, x));
    }
}
