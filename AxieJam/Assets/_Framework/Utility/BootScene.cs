#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BootScene
    : MonoBehaviour
{
    [MenuItem("Play/Boot")] 
    static void Boot()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/_Scenes/BootScene.unity");
        EditorApplication.isPlaying = true;

    }
}
#endif