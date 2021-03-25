using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static float ModLoop(this float lhs, float rhs)
    {
        return ((lhs % rhs) + rhs) % rhs;
    }

    public static Vector3 Add(this Vector3 a, Vector2 b) 
    {
        return a + new Vector3(b.x, b.y);
    }

    public static Vector3 Divide(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    public static Vector3 Multiply(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    /// <summary>
    /// The length of the component of vector a that is in the direction of b.
    /// </summary>
    /// <param name="a">Original vector</param>
    /// <param name="b">Your directional vector. Length is irrelevent.</param>
    /// <returns>The length of the component of vector a that is in the direction of b.</returns>
    public static float ScalarProjection(this Vector3 a, Vector3 b)
    {
        float topOfFraction = Vector3.Dot(a, b);
        float bottomOfFraction = Mathf.Pow(b.magnitude, 2);

        return topOfFraction / bottomOfFraction;
    }

    /// <summary>
    /// The component of vector a that is in the direction of b.
    /// </summary>
    /// <param name="a">Original vector</param>
    /// <param name="b">Your directional vector. Length is irrelevent. Returned vector will be in this direction.</param>
    /// <returns>The component of vector a that is in the direction of b.</returns>
    public static Vector3 VectorProjection(this Vector3 a, Vector3 b)
    {
        float scalarProjection = a.ScalarProjection(b);

        return scalarProjection * b;
    }

    public static Vector3 AbsoluteVector(this Vector3 vectorToAbsolute)
    {
        return new Vector3(Mathf.Abs(vectorToAbsolute.x), Mathf.Abs(vectorToAbsolute.y), Mathf.Abs(vectorToAbsolute.z));
    }

    /// <summary>
    /// The length of the component of vector a that is in the direction of b.
    /// </summary>
    /// <param name="a">Original vector</param>
    /// <param name="b">Your directional vector. Length is irrelevent.</param>
    /// <returns>The length of the component of vector a that is in the direction of b.</returns>
    public static float ScalarProjection(this Vector2 a, Vector2 b)
    {
        float topOfFraction = Vector2.Dot(a, b);
        float bottomOfFraction = Mathf.Pow(b.magnitude, 2);

        return topOfFraction / bottomOfFraction;
    }

    /// <summary>
    /// The component of vector a that is in the direction of b.
    /// </summary>
    /// <param name="a">Original vector</param>
    /// <param name="b">Your directional vector. Length is irrelevent. Returned vector will be in this direction.</param>
    /// <returns>The component of vector a that is in the direction of b.</returns>
    public static Vector2 VectorProjection(this Vector2 a, Vector2 b)
    {
        float scalarProjection = a.ScalarProjection(b);

        return scalarProjection * b;
    }

    /// <summary>
    /// The component of vector a that is on the plane defined by planeNormal.
    /// </summary>
    /// <param name="a">Original vector</param>
    /// <param name="b">Your plane normal. Length is irrelevent. Returned on the plane defined by this normal.</param>
    /// <returns>The component of vector a that is on the defined plane.</returns>
    public static Vector3 VectorProjectionToPlane(this Vector3 a, Vector3 planeNormal)
    {
        return a - a.VectorProjection(planeNormal);
    }

    public static Vector2 AbsoluteVector(this Vector2 vectorToAbsolute)
    {
        return new Vector2(Mathf.Abs(vectorToAbsolute.x), Mathf.Abs(vectorToAbsolute.y));
    }

    /// <summary>
    /// Returns a random vector3 with components 0 to range in each component.
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Vector3 RandomVector3(this float range)
    {
        return new Vector3(Random.Range(0, range), Random.Range(0, range), Random.Range(0, range));
    }

    /// <summary>
    /// Returns a random vector3 with components 0 to range in each component.
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Vector2 RandomVector2(this float range)
    {
        return new Vector2(Random.Range(0, range), Random.Range(0, range));
    }


    public static int Layermask_to_layer(this LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }


    public static bool SameAs(this Gradient g, Gradient other)
    {
        if (g.colorKeys.Length != other.colorKeys.Length || g.alphaKeys.Length != other.alphaKeys.Length || g.mode != other.mode)
            return false;

        for (int i = 0; i < g.colorKeys.Length; i++)
        {
            Color color = g.colorKeys[i].color;
            float time = g.colorKeys[i].time;

            if (color != other.colorKeys[i].color || time != other.colorKeys[i].time)
                return false;
        }

        for (int i = 0; i < g.alphaKeys.Length; i++)
        {
            float alpha = g.alphaKeys[i].alpha;
            float time = g.alphaKeys[i].time;

            if (alpha != other.alphaKeys[i].alpha || time != other.alphaKeys[i].time)
                return false;
        }

        return true;
    }

    public static Gradient Clone(this Gradient g)
    {
        return new Gradient() { colorKeys = g.colorKeys, alphaKeys = g.alphaKeys, mode = g.mode };
    }

    public static Quaternion Add(this Quaternion a, Quaternion b)
    {
        return Quaternion.Euler(a.eulerAngles + b.eulerAngles);
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static Color32 AddAlpha(this Color32 color, float alpha)
    {
        return new Color(color.r, color.g, color.b, color.a + alpha);

    }

    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }

    public static void DestroyChildren<T>(this T parent, float delay = 0f) where T : Component
    {
        for (int i = parent.transform.childCount - 1; i >= 0; i--)
            Object.Destroy(parent.transform.GetChild(i).gameObject, delay);
    }

    public static void CopyValuesFrom(this RectTransform to, RectTransform from)
    {
        to.anchoredPosition = from.anchoredPosition;
        to.offsetMin = from.offsetMin;
        to.offsetMax = from.offsetMax;
        to.pivot = from.pivot;
        to.sizeDelta = from.sizeDelta;
        to.anchorMin = from.anchorMin;
        to.anchorMax = from.anchorMax;
    }

    public static void SetSquareSize(this RectTransform rect, float val)
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, val);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, val);
    }

    public static bool IsWithinTargetThreshold(this float value, float target, float threshold)
    {
        return (value < target + threshold && value > target - threshold);
    }

    public static RectTransform AddDotAsChild(this RectTransform rect, float diameter)
    {
        RectTransform child = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Dot")).GetComponent<RectTransform>();
        child.SetSquareSize(diameter);
        child.SetParent(rect);
        return child;
    }

    public static void SetNormalizedPosition(this RectTransform rect, float x, float y)
    {
        rect.anchorMin = new Vector2(x, y);
        rect.anchorMax = new Vector2(x, y);
        rect.anchoredPosition = Vector2.zero;
    }

    public static void Circle(Color32[] tempArray, long cx, long cy, long r, int width, Color col)
    {
        long x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y < d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tempArray[py * width + px] = col;
                tempArray[py * width + nx] = col;
                tempArray[ny * width + px] = col;
                tempArray[ny * width + nx] = col;
            }
        }
    }

    public static void DrawCircle(this Texture2D texture, int radius, int centerX, int centerY, Color color)
    {
        int x = radius - 1;
        int y = 0;
        int dx = 1;
        int dy = 1;
        int diameter = radius << 1;
        int decision = dx - diameter;

        while (x >= y)
        {
            texture.SetPixel(centerX + x, centerY + y, color);
            texture.SetPixel(centerX + y, centerY + x, color);
            texture.SetPixel(centerX - y, centerY + x, color);
            texture.SetPixel(centerX - x, centerY + y, color);
            texture.SetPixel(centerX - x, centerY - y, color);
            texture.SetPixel(centerX - y, centerY - x, color);
            texture.SetPixel(centerX + y, centerY - x, color);
            texture.SetPixel(centerX + x, centerY - y, color);

            if (decision <= 0)
            {
                y++;
                decision += dy;
                dy += 2;
            }

            if (decision > 0)
            {
                x--;
                dx += 2;
                decision += dx - diameter;
            }
        }

        texture.Apply();
    }

    public static bool IsWithinRange(this float a, float min, float max)
    {
        return (a >= min && a <= max);
    }

}