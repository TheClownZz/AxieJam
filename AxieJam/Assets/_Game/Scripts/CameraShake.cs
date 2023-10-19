using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    [SerializeField] Transform camTransform;

    // How long the object should shake for.
    float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;

    Vector3 originalPos;

    bool isBigShake = false;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0 && !GameManager.Instance.isPause)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            isBigShake = false;
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }


    [Button]
    public void SmallShake(float time = 0.05f, float shakeAmount = 0.05f)
    {
        if (isBigShake)
        {
            return;
        }
        shakeDuration = time;
        this.shakeAmount = shakeAmount;
    }

    public void BigShake(float time = 0.1f, float shakeAmount = 0.2f)
    {
        if (isBigShake)
        {
            return;
        }
        isBigShake = true;
        shakeDuration = time;
        this.shakeAmount = shakeAmount;
    }

}
