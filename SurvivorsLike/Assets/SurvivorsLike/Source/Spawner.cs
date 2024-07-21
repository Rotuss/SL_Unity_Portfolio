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
        // 스폰 테스트용
        if(true == Input.GetButtonDown("Jump"))
        {
            // float형과 다르게 int형은 마지막 값이 포함되지 않으므로 +1 해야함
            GameManager.Instance.Pool.Get(Random.Range(0,2));
        }
    }
}
