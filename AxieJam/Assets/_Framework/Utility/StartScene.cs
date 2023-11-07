using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
public class StartScene
    : MonoBehaviour
{
    [MenuItem("Play/Boot")] 
    static void Boot()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/BootScene.unity");
        EditorApplication.isPlaying = true;

    }
}
#endif