using UnityEngine;
using System.Collections;

public class Helper 
{
    public static bool isINRect(Vector3 point, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float x = point.x;

        float y = point.z;

        float v0x = v0.x;

        float v0y = v0.z;

        float v1x = v1.x;

        float v1y = v1.z;

        float v2x = v2.x;

        float v2y = v2.z;

        float v3x = v3.x;

        float v3y = v3.z;

        if (Multiply(x, y, v0x, v0y, v1x, v1y) * Multiply(x, y, v3x, v3y, v2x, v2y) <= 0 && Multiply(x, y, v3x, v3y, v0x, v0y) * Multiply(x, y, v2x, v2y, v1x, v1y) <= 0)
            return true;
        else
            return false;
    }

	public static bool isInRadious( Vector3 point, Vector3 v0, float fradious )
	{
		float fdis = (point - v0).magnitude;
		if (fdis <= fradious)
			return true;
		else 
			return false;
	}

    public static float Multiply(float p1x, float p1y, float p2x, float p2y, float p0x, float p0y)
    {
        return ((p1x - p0x) * (p2y - p0y) - (p2x - p0x) * (p1y - p0y));
    }

    public static Vector3 WorldToUI(Vector3 point)
    {

        Vector3 pt = Camera.main.WorldToScreenPoint(point);

        //我发现有时候UICamera.currentCamera 有时候currentCamera会取错，取的时候注意一下啊。

        Vector3 ff = UICamera.currentCamera.ScreenToWorldPoint(pt);

        //UI的话Z轴 等于0

        ff.z = 0;

        return ff;
    }


	public static bool MIsValuePct( int nValue )
	{
		if (nValue > 100000)
			return true;
		else
			return false;
	}

	public static int MValuePctTrans( int nValue )
	{
		return nValue - 100000;
	}
}


