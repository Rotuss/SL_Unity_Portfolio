using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ������ ���� ����
    // Prefabs[Enemy A, Enemy B]
    public GameObject[] Prefabs;

    // Pool List
    // Pools[Enemy A] = {Enemy A 0, Enemy A 1, Enemy A 2, ...}
    // Pools[Enemy B] = {Enemy B 0, Enemy B 1, Enemy B 2, ...}
    List<GameObject>[] Pools;

    private void Awake()
    {
        // Pool ũ�� �ʱ�ȭ, Pool�� ��� �迭 �ʱ�ȭ
        Pools = new List<GameObject>[Prefabs.Length];

        // ���� �ʱ�ȭ
        for(int i = 0; i < Pools.Length; ++i)
        {
            // �迭 �ȿ� �ִ� �� List �ʱ�ȭ
            Pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject Select = null;

        foreach(GameObject Enemy in Pools[index])
        {
            // Enemy�� ��Ȱ��ȭ ����
            if (false == Enemy.activeSelf)
            {
                Select = Enemy;
                // Ȱ��ȭ
                Select.SetActive(true);
                break;
            }
        }

        // ���� Ȱ��ȭ �Ǿ� �ִ� ���
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
