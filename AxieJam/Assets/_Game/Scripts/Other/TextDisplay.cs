using UnityEngine;
using TMPro;
public class TextDisplay : MonoBehaviour
{
    const string MISS = "Miss";
    const string ONE_HIT = "Bingo";

    [SerializeField] TextMeshPro tmp;

    public void ShowDamage(float damage, bool isCrit)
    {
        if (isCrit)
        {
            tmp.color = GameManager.Instance.gameConfig.critColor;
            tmp.transform.localScale = Vector3.one * 1.5f;
        }
        else
        {
            tmp.color = Color.white;
            tmp.transform.localScale = Vector3.one;
        }

        tmp.SetText(((int)damage).ToString());
    }

    public void ShowMiss()
    {
        tmp.SetText(MISS);
        tmp.color = Color.white;
        tmp.transform.localScale = Vector3.one;
    }

    public void ShowOneHit()
    {
        tmp.SetText(ONE_HIT);
        tmp.color = Color.red;
        tmp.transform.localScale = Vector3.one * 1.5f;
    }

}
