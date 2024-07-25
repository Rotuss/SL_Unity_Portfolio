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
        // �÷��̾� ���� ��ǥ�� �������� ��ũ�� ��ǥ�� �ٸ��� ������ ��ȯ �۾�
        Rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.MainPlayer.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
