using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �׽�Ʈ��
        if(true == Input.GetButtonDown("Jump"))
        {
            // float���� �ٸ��� int���� ������ ���� ���Ե��� �����Ƿ� +1 �ؾ���
            GameManager.Instance.Pool.Get(Random.Range(0,2));
        }
    }
}
