using System.Collections.Generic;
using UnityEngine;

public interface IInfiniteScrollItem
{
    int index { get; set; }
    void OnUpdateItem();
}

public class InfiniteScroll : MonoBehaviour
{
    public int instantateItemCount;

    public float offset;

    [SerializeField] bool isHorizontal = true;

    [SerializeField]
    private RectTransform m_OriginalItem;

    [SerializeField]
    private RectTransform m_Content;

    // 生成済みの要素
    private LinkedList<RectTransform> m_Items = new LinkedList<RectTransform>();

    // 生成済みの要素についているコンポーネントのキャッシュ
    private Dictionary<RectTransform, IInfiniteScrollItem> m_ItemComponents = new Dictionary<RectTransform, IInfiniteScrollItem>();

    [SerializeField] private float m_FilledSize;

    [SerializeField] private int m_CurrentItemIndex;

    [SerializeField] private bool m_IsSetupped = false;

    // Use this for initialization

    
    public void Clear()
    {
        if (!m_IsSetupped)
            return;
        m_IsSetupped = false;
        foreach (var item in m_Items)
        {
            Destroy(item.gameObject);
        }
        m_Items.Clear();
    }

    public LinkedList<RectTransform> GetListItem()
    {
        return m_Items;
    }
    public void Setup()
    {
        if (m_IsSetupped)
            return;

        m_IsSetupped = true;

        var itemSize = isHorizontal ? m_OriginalItem.sizeDelta.x : m_OriginalItem.sizeDelta.y;
        Vector2 itemSizeVec = isHorizontal ? Vector2.right * itemSize : Vector2.down * itemSize;
        // 指定の数だけ生成
        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = Instantiate<RectTransform>(m_OriginalItem);
            item.SetParent(m_Content, false);
            item.name = "item " + i;
            item.gameObject.SetActive(true);

            item.anchoredPosition = i * itemSizeVec;

            m_Items.AddLast(item);

            var itemComponent = item.GetComponent<IInfiniteScrollItem>();
            if (itemComponent != null)
            {
                m_ItemComponents[item] = itemComponent;
                OnUpdateItem(i, item);
            }
        }
    }

    void Update()
    {
        RectTransform itemUpdated;

        var itemSize = isHorizontal ? m_OriginalItem.sizeDelta.x : m_OriginalItem.sizeDelta.y;
        var anchoredPosition = isHorizontal ? m_Content.anchoredPosition.x : m_Content.anchoredPosition.y;
        int direction = isHorizontal ? 1 : -1;
        // 正の方向にスクロールした時、スクロール量が1アイテムを超えた分だけ後ろに補填する
        while (direction * anchoredPosition + m_FilledSize < -itemSize - offset)
        {
            m_FilledSize += itemSize;

            itemUpdated = m_Items.First.Value;
            m_Items.RemoveFirst();
            m_Items.AddLast(itemUpdated);

            itemUpdated.anchoredPosition = isHorizontal ? Vector2.right * (m_CurrentItemIndex + instantateItemCount) * itemSize
                : Vector2.down * (m_CurrentItemIndex + instantateItemCount) * itemSize;

            OnUpdateItem(m_CurrentItemIndex + instantateItemCount, itemUpdated);

            m_CurrentItemIndex++;
        }

        // 負の方向にスクロールした時、補填分が余るようになったら補填分を先頭に戻す
        while (direction * anchoredPosition + m_FilledSize > -offset)
        {
            m_FilledSize -= itemSize;

            itemUpdated = m_Items.Last.Value;
            m_Items.RemoveLast();
            m_Items.AddFirst(itemUpdated);

            m_CurrentItemIndex--;

            itemUpdated.anchoredPosition = itemUpdated.anchoredPosition = isHorizontal ? Vector2.right * m_CurrentItemIndex * itemSize
                : Vector2.down * m_CurrentItemIndex * itemSize; ;

            OnUpdateItem(m_CurrentItemIndex, itemUpdated);
        }
    }

    private void OnUpdateItem(int index, RectTransform item)
    {
        IInfiniteScrollItem itemComponent;
        if (m_ItemComponents.TryGetValue(item, out itemComponent))
        {
            itemComponent.index = index;
            itemComponent.OnUpdateItem();
        }
    }
}
