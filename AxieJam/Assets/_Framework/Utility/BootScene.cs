using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
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