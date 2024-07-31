using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform Rect;
    Item[] Items;
    List<Item> NonMaxLevelItems = new List<Item>();

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        Items = GetComponentsInChildren<Item>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region
    public void Show()
    {
        Next();

        Rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        
        AudioManager.Instance.PlaySFX(AudioManager.ESFX.LevelUp);
        AudioManager.Instance.EffectBGM(true);
    }

    public void Hide()
    {
        Rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();

        AudioManager.Instance.PlaySFX(AudioManager.ESFX.Select);
        AudioManager.Instance.EffectBGM(false);
    }

    public void Select(int InItemID)
    {
        Items[InItemID].Onclick();
    }

    void Next()
    {
        // 모든 아이템 비활성화
        foreach (Item Item in Items)
        {
            Item.gameObject.SetActive(false);
        }

        NonMaxLevelItems.Clear();
        // 만렙 체크하여 아닐 경우만 담기
        foreach (Item Item in Items)
        {
            if (Item.Data.Damages.Length > Item.Level)
            {
                NonMaxLevelItems.Add(Item);
            }
        }

        int ItemCount = NonMaxLevelItems.Count;

        if (3 <= ItemCount)
        {
            // 랜덤 아이템 3개 활성화
            for (int i = 0; i < 3; ++i)
            {
                int RandomIndex = Random.Range(0, ItemCount);
                Item RandomItem = NonMaxLevelItems[RandomIndex];
                RandomItem.gameObject.SetActive(true);
                NonMaxLevelItems.RemoveAt(RandomIndex);
                --ItemCount;
            }
        }
        else if (0 < ItemCount)
        {
            // 만렙이 아닌 아이템이 1개 혹은 2개일 경우
            // 음료수의 경우 레벨이 없어 항상 나옴 + 나머지 아이템 하나 or only 포션만 활성화
            foreach (Item Item in NonMaxLevelItems)
            {
                Item.gameObject.SetActive(true);
            }
        }
    }
    #endregion
}
