using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public static class FrameWorkUtility
{
    #region Math
    public static float sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(pt, v1, v2);
        d2 = sign(pt, v2, v3);
        d3 = sign(pt, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }
    public static Vector3 WrapRotation(Quaternion rot)
    {
        return new Vector3(WrapAngle(rot.eulerAngles.x), WrapAngle(rot.eulerAngles.y), WrapAngle(rot.eulerAngles.z));
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    public static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
    #endregion

    #region Material_Mesh
    public static void ChangeMaterial(this Renderer rend, Material mat, int index = 0)
    {
        Material[] mats = rend.materials;
        mats[index] = mat;
        rend.materials = mats;
    }

    public static void ChangeMesh(this SkinnedMeshRenderer rend, Mesh mesh)
    {
        rend.sharedMesh = mesh;
    }
    public static void ChangeTextureProp(this Renderer renderer, Texture tex)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
        int propID = Shader.PropertyToID("_MainTex");
        propBlock.SetTexture(propID, tex);
        renderer.SetPropertyBlock(propBlock);
    }

    public static void ChangeColorProp(this Renderer renderer, Color color)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
        int propID = Shader.PropertyToID("_Color");
        propBlock.SetColor(propID, color);
        renderer.SetPropertyBlock(propBlock);
    }
    public static void SetColor(this Renderer renderer, Color color)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(propBlock);
    }

    public static void LerpColor(this MonoBehaviour self, Renderer renderer, Color color1, Color color2, float duration = 0f, System.Action OnCompleted = null)
    {
        self.StartCoroutine(I_SetColor(renderer, color1, color2, duration, OnCompleted));
    }

    static IEnumerator I_SetColor(Renderer renderer, Color color1, Color color2, float duration, System.Action OnCompleted)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            propBlock.SetColor("_Color", Color.Lerp(color1, color2, Mathf.SmoothStep(0f, 1f, t)));
            renderer.SetPropertyBlock(propBlock);
            yield return null;
        }
        OnCompleted?.Invoke();
    }
    #endregion

    #region Convert
    public static long DateTimeToUnix(DateTime MyDateTime)
    {
        TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);

        return (long)timeSpan.TotalSeconds;
    }

    public static DateTime UnixTimeToDateTime(long unixtime)
    {
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
        return dtDateTime;
    }

    public static TimeSpan GetTimeSpan(DateTime to, DateTime from)
    {
        var toDay = new DateTime(to.Year, to.Month, to.Day);
        var fromDay = new DateTime(from.Year, from.Month, from.Day);

        return toDay - fromDay;
    }

    public static string ToString(Int64 num)
    {
        if (num > 999999999 || num < -999999999)
        {
            num = num / 1000000000;
            num = num * 1000000000;
            return num.ToString("0,,,.#B", CultureInfo.InvariantCulture);
        }
        else if (num > 999999 || num < -999999)
        {
            num = num / 1000000;
            num = num * 1000000;
            return num.ToString("0,,.#M", CultureInfo.InvariantCulture);
        }
        // else if (num > 99999 || num < -99999)
        // {
        //     num = num / 1000;
        //     num = num * 1000;
        //     return num.ToString("0,.#K", CultureInfo.InvariantCulture);
        // }
        else if (num > 10000 || num < -10000)
        {
            num = num / 1000;
            num = num * 1000;
            return num.ToString("0,,#00", CultureInfo.InvariantCulture);
        }
        else
        {
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }

    public static string ToPercent(float num, int digit = 0)
    {
        return num.ToString("P" + digit).Replace(" ", string.Empty);
    }
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string IntToBinaryString(int input, int stringLength)
    {
        return Convert.ToString(input, 2).PadLeft(stringLength, '0');
    }
    #endregion

    #region MonoBehaviourExtention

    // Runs the Callback at the end of the current frame, after GUI rendering
    public static Coroutine OnEndOfFrame(this MonoBehaviour self, UnityAction Callback)
    {
        return self.StartCoroutine(EndOfFrameCoroutine(Callback));
    }

    static IEnumerator EndOfFrameCoroutine(UnityAction Callback)
    {
        yield return new WaitForEndOfFrame();
        Callback?.Invoke();
    }

    // Runs the Callback after the next Update completes
    public static Coroutine OnUpdate(this MonoBehaviour self, UnityAction Callback)
    {
        return self.InUpdates(1, Callback);
    }

    // Runs the Callback after a given number of Updates complete
    public static Coroutine InUpdates(this MonoBehaviour self, int updates, UnityAction Callback)
    {
        return self.StartCoroutine(InUpdatesCoroutine(updates, Callback));
    }

    static IEnumerator InUpdatesCoroutine(int updates, UnityAction Callback)
    {
        for (int i = 0; i < updates; i++)
        {
            yield return null;
        }
        Callback?.Invoke();
    }

    // Runs the Callback after the next FixedUpdate completes
    public static Coroutine OnFixedUpdate(this MonoBehaviour self, UnityAction Callback)
    {
        return self.InFixedUpdates(1, Callback);
    }

    // Runs the Callback after a given number of FixedUpdates complete
    public static Coroutine InFixedUpdates(this MonoBehaviour self, int ticks, UnityAction Callback)
    {
        return self.StartCoroutine(InFixedUpdatesCoroutine(ticks, Callback));
    }

    static IEnumerator InFixedUpdatesCoroutine(int ticks, UnityAction Callback)
    {
        for (int i = 0; i < ticks; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        Callback?.Invoke();
    }

    // Runs the Callback after a given number of seconds, after the Update completes
    public static Coroutine InSeconds(this MonoBehaviour self, float seconds, UnityAction Callback)
    {
        return self.StartCoroutine(InSecondsCoroutine(seconds, Callback));
    }

    static IEnumerator InSecondsCoroutine(float seconds, UnityAction Callback)
    {
        yield return new WaitForSeconds(seconds);
        Callback?.Invoke();
    }

    public static Coroutine InSecondsInterval(this MonoBehaviour self, float seconds, float interval, UnityAction Callback)
    {
        return self.StartCoroutine(InSecondsIntervalCoroutine(self, seconds, interval, Callback));
    }

    static IEnumerator InSecondsIntervalCoroutine(MonoBehaviour self, float seconds, float interval, UnityAction Callback)
    {
        yield return new WaitForSeconds(seconds);
        while (self.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(interval);
            Callback?.Invoke();
        }
    }

    #endregion

    #region Tween
    public static void DoTransform(this Transform tf, Transform target, float duration, float delay = 0f, System.Action OnCompleted = null)
    {
        tf.DOMove(target.position, duration)
        .SetDelay(delay);

        tf.DORotateQuaternion(target.rotation, duration)
        .SetDelay(delay)
        .OnComplete(() => { OnCompleted?.Invoke(); });
    }

    public static void DoLocalTransform(this Transform tf, Transform target, float duration, float delay = 0f, System.Action OnCompleted = null)
    {
        tf.DOLocalMove(target.localPosition, duration)
        .SetDelay(delay);

        tf.DOLocalRotateQuaternion(target.localRotation, duration)
        .SetDelay(delay)
        .OnComplete(() => { OnCompleted?.Invoke(); });
    }

    public static void DOPunchScaleEff(this Transform target, float punch = 0.17f, Ease ease = Ease.Linear)
    {
        if (target != null)
        {
            target.DOKill();
            target.DOPunchScale(new Vector3(punch, punch, punch), 0.15f, 2).SetEase(ease);
        }
    }
    public static void DOProgress(this RectTransform rtf, float max, float percent, MonoBehaviour monoBehaviour = null, float duration = 0f)
    {
        float width = percent * max;
        if (duration > 0f)
        {
            Vector2 start = rtf.sizeDelta;
            Vector2 end = new Vector2(width, start.y);
            // MEC.Timing.RunCoroutine(I_Progress(rtf, start, end, duration));
            monoBehaviour.StartCoroutine(I_Progress(rtf, start, end, duration));
        }
        else
        {
            rtf.sizeDelta = new Vector2(width, rtf.sizeDelta.y);
        }

    }

    static IEnumerator<float> I_Progress(RectTransform rtf, Vector2 start, Vector2 end, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            rtf.sizeDelta = Vector2.Lerp(start, end, t);
            yield return 0f;
        }
    }

    #endregion

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }


    public static void Log(this Exception excep)
    {
        Debug.LogErrorFormat("[Exception]{0}", excep.Message);
    }
    public static string LogVector2(Vector2 vector2)
    {
        return string.Format(vector2.x + "  " + vector2.y);
    }



    public static void IsVisible(this CanvasGroup canvasGroup, bool isVisible, float minAlpha = 0.0f)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1 : minAlpha;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }


    public static void SmoothLookAtInSeconds(this MonoBehaviour self, Transform tf, Transform target, float damping, float duration, System.Action OnCompleted = null)
    {
        self.StartCoroutine(I_SmoothLookAtInSeconds(tf, target, damping, duration, OnCompleted));
    }

    static IEnumerator I_SmoothLookAtInSeconds(Transform tf, Transform target, float damping, float duration, System.Action OnCompleted)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            var targetRotation = Quaternion.LookRotation(target.position - tf.position);
            tf.rotation = Quaternion.Slerp(tf.rotation, targetRotation, t /* * damping */);
            yield return null;
        }
        OnCompleted?.Invoke();
    }

    public static void SmoothLookAt(this Transform tf, Transform target, float damping)     //Use in Update()
    {
        var targetRotation = Quaternion.LookRotation(target.position - tf.position);
        tf.rotation = Quaternion.Slerp(tf.rotation, targetRotation, damping * Time.deltaTime);
    }

    public static bool IsVisible(this Camera camera, Vector3 position)
    {
        Vector3 viewPos = camera.WorldToViewportPoint(position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            return true;
        }

        return false;
    }

    public static bool IsObjectVisible(this Camera @this, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), renderer.bounds);
    }

    public static void MutipleRayCastAll(ref List<RaycastHit2D> result, Vector2 original, Vector2 point1, Vector2 point2, float distance, int layer)
    {
        if (result == null)
            return;

        result.Clear();

        Vector2 diff = point2 - point1;
        Vector2 direction = diff.normalized;

        int maxIndex = (int)(diff.magnitude / distance);

        Vector2 diff2;
        RaycastHit2D[] hitResult;
        for (int i = 0; i <= maxIndex; i++)
        {
            Vector2 end = point1 + i * direction * distance;
            diff2 = end - original;
            hitResult = Physics2D.RaycastAll(original, diff2.normalized, diff2.magnitude, layer);

            foreach (RaycastHit2D hit in hitResult)
                if (hit.collider != null && !result.Find(x => x.collider == hit.collider))
                {
                    result.Add(hit);
                }
        }

        // cast last
        diff2 = point2 - original;
        hitResult = Physics2D.RaycastAll(original, diff2.normalized, diff2.magnitude, layer);
        foreach (RaycastHit2D hit in hitResult)
            if (hit.collider != null && !result.Find(x => x.collider == hit.collider))
            {
                result.Add(hit);
            }
    }
    public static RaycastHit2D MutipleRayCast(Vector2 original, Vector2 point1, Vector2 point2, float distance, int layer)
    {
        Vector2 diff = point2 - point1;
        Vector2 direction = diff.normalized;

        int maxIndex = (int)(diff.magnitude / distance);

        Vector2 diff2;
        RaycastHit2D hitResult;
        for (int i = 0; i <= maxIndex; i++)
        {
            Vector2 end = point1 + i * direction * distance;
            diff2 = end - original;
            hitResult = Physics2D.Raycast(original, diff2.normalized, diff2.magnitude, layer);
            if (hitResult.collider != null)
            {
                return hitResult;
            }
        }

        // cast last
        diff2 = point2 - original;
        hitResult = Physics2D.Raycast(original, diff2.normalized, diff2.magnitude, layer);
        return hitResult;
    }

    public static void DrawPlane(Vector3 normal, Vector3 position)
    {
        Vector3 v3;
        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;

        Debug.DrawLine(corner0, corner2, Color.green, 100);
        Debug.DrawLine(corner1, corner3, Color.green, 100);
        Debug.DrawLine(corner0, corner1, Color.green, 100);
        Debug.DrawLine(corner1, corner2, Color.green, 100);
        Debug.DrawLine(corner2, corner3, Color.green, 100);
        Debug.DrawLine(corner3, corner0, Color.green, 100);
        Debug.DrawRay(position, normal, Color.red);
    }

    public static bool GetRating(float target)
    {
        return UnityEngine.Random.value <= target;
    }

    public static void SetActive(this CanvasGroup canvasGroup, bool isActive, float fadeTime = 0.0f)
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(isActive ? 1.0f : 0.0f, fadeTime);
        canvasGroup.blocksRaycasts = isActive;
    }

    public static string TimeFormatter(float seconds, bool forceHHMMSS = false)
    {
        float secondsRemainder = Mathf.Floor((seconds % 60) * 100) / 100.0f;
        int minutes = ((int)(seconds / 60)) % 60;
        int hours = (int)(seconds / 3600);

        if (!forceHHMMSS)
        {
            if (hours == 0)
            {
                return String.Format("{0:00}:{1:00}", minutes, secondsRemainder);
            }
        }
        return String.Format("{0}:{1:00}:{2:00}", hours, minutes, secondsRemainder);
    }

    public static Vector3 SpawnInCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
