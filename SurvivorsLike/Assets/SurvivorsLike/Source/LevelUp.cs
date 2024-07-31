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
        // ��� ������ ��Ȱ��ȭ
        foreach (Item Item in Items)
        {
            Item.gameObject.SetActive(false);
        }

        NonMaxLevelItems.Clear();
        // ���� üũ�Ͽ� �ƴ� ��츸 ���
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
            // ���� ������ 3�� Ȱ��ȭ
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
            // ������ �ƴ� �������� 1�� Ȥ�� 2���� ���
            // ������� ��� ������ ���� �׻� ���� + ������ ������ �ϳ� or only ���Ǹ� Ȱ��ȭ
            foreach (Item Item in NonMaxLevelItems)
            {
                Item.gameObject.SetActive(true);
            }
        }
    }
    #endregion
}
