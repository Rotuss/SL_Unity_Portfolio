using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
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

    private void FixedUpdate()
    {
        // 플레이어 월드 좌표와 유아이의 스크린 좌표는 다르기 때문에 변환 작업
        Rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.MainPlayer.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
