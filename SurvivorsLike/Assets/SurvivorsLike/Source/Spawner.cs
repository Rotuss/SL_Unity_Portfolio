using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoint;

    float Timer;
    int Level;

    private void Awake()
    {
        // GetComponentsInChildren�� �ڱ� �ڽŵ� ����, Ʈ�������� �׻� ���ԵǾ� �ֱ� ����
        SpawnPoint = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        Level = Mathf.FloorToInt(GameManager.Instance.GameTime / 10.0f);

        if((0 == Level ? 0.5f : 0.2f) < Timer)
        {
            Timer = 0.0f;
            Spawn();
        }

    }

    void Spawn()
    {
        GameObject EnemyObj = GameManager.Instance.Pool.Get(Level);
        EnemyObj.transform.position = SpawnPoint[UnityEngine.Random.Range(1, SpawnPoint.Length)].position;

    }
}
