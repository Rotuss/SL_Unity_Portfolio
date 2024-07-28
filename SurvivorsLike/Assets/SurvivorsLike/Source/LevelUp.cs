using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform Rect;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
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
    }

    public void Hide()
    {
        Rect.localScale = Vector3.zero;
    }
    #endregion
}
