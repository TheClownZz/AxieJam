#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneUtility
    : MonoBehaviour
{
    [MenuItem("Play/Boot")] 
    static void Boot()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/_Scenes/BootScene.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Play/Menu")]
    static void Menu()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/_Scenes/MenuScene.unity");
    }

    [MenuItem("Play/Game")]
    static void Game()
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/_Scenes/GameScene.unity");
    }
}
#endif