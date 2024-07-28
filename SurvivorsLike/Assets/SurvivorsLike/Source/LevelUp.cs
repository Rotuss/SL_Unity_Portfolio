using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform Rect;
    Item[] Items;

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
        Rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
    }

    public void Hide()
    {
        Rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
    }

    public void Select(int InItemID)
    {
        Items[InItemID].Onclick();
    }
    #endregion
}
