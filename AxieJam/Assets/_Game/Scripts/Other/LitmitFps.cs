using UnityEngine;

public class LitmitFps : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 60;
    private void Start()
    {
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = targetFrameRate;
    }
}
