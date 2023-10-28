using UnityEngine;

public class LitmitFps : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 60;
    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
