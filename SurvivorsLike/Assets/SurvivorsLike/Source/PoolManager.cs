using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리펩 보관 변수
    // Prefabs[Enemy A, Enemy B]
    public GameObject[] Prefabs;

    // Pool List
    // Pools[Enemy A] = {Enemy A 0, Enemy A 1, Enemy A 2, ...}
    // Pools[Enemy B] = {Enemy B 0, Enemy B 1, Enemy B 2, ...}
    List<GameObject>[] Pools;

    private void Awake()
    {
        // Pool 크기 초기화, Pool을 담는 배열 초기화
        Pools = new List<GameObject>[Prefabs.Length];

        // 세부 초기화
        for(int i = 0; i < Pools.Length; ++i)
        {
            // 배열 안에 있는 각 List 초기화
            Pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject Select = null;

        foreach(GameObject Enemy in Pools[index])
        {
            // Enemy가 비활성화 상태
            if (false == Enemy.activeSelf)
            {
                Select = Enemy;
                // 활성화
                Select.SetActive(true);
                break;
            }
        }

        // 전부 활성화 되어 있는 경우
        if(null == Select)
        {
            Select = Instantiate(Prefabs[index], transform);
            Pools[index].Add(Select);
        }

        return Select;
    }

    public List<GameObject> PoolsGet(int Index)
    {
        return Pools[Index];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
