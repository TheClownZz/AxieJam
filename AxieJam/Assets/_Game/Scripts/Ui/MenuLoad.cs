using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class MenuLoad : MonoBehaviour
{
    [SerializeField] AssetReferenceT<Sprite> teamBgAsset;
    [SerializeField] AssetReferenceT<Sprite> axieBgAsset;

    Sprite teamBg;
    Sprite axieBg;
    IEnumerator Start()
    {
        yield return new WaitUntil(() => UIManager.Instance != null && UIManager.Instance.isInit);
        LoadBackGround();
    }

    private void OnDestroy()
    {
        UnloadBackGround();
    }
    void UnloadBackGround()
    {
        if(teamBgAsset.Asset)
        {
            teamBgAsset.ReleaseAsset();
        }
        if (axieBgAsset.Asset)
        {
            teamBgAsset.ReleaseAsset();
        }
        Debug.LogError("UnloadBackGround ok");
    }
    void LoadBackGround()
    {
        Addressables.LoadAssetAsync<Sprite>(teamBgAsset).Completed += (handler) =>
        {
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                teamBg = handler.Result;
                UIManager.Instance.GetScreen<ScreenTeam>().SetBg(handler.Result);
            }
            else
                Debug.LogError("can not load team bg");
        };

        Addressables.LoadAssetAsync<Sprite>(axieBgAsset).Completed += (handler) =>
        {
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                axieBg = handler.Result;
                UIManager.Instance.GetScreen<ScreenAxie>().SetBg(handler.Result);

            }
            else
                Debug.LogError("can not load axie bg");
        };

        Debug.LogError("LoadBackGround ok");

    }

}
