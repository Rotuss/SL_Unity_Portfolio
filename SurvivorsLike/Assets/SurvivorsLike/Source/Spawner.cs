using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 스폰될 위치들을 관리하는 Transform 배열
    public Transform[] SpawnPoint;
    // 스폰정보들을 관리하는 SpawnData 배열
    public SpawnData[] SpawnDataInfo;

    float Timer;
    int Level;

    private void Awake()
    {
        // GetComponentsInChildren는 자기 자신도 포함, 트랜스폼은 항상 포함되어 있기 때문
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
        // 배열 범위 넘어서 처리 되지 않게 Min 사용
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / 10.0f), SpawnDataInfo.Length - 1);

        // 현재 레벨에 맞춰 SpawnDataInfo의 Level에 따른 스폰 타임에 따라 Enemy Spawn
        if (SpawnDataInfo[Level].SpawnTime < Timer)
        {
            Timer = 0.0f;
            Spawn();
        }

    }

    void Spawn()
    {
        // 스폰할 Enemy를 Enemy Pool에서 Object 가져오기
        GameObject EnemyObj = GameManager.Instance.Pool.Get(0);
        // 가져온 Enemy Object 위치 지정
        EnemyObj.transform.position = SpawnPoint[UnityEngine.Random.Range(1, SpawnPoint.Length)].position;
        // 가져온 Enemy Object를 어떤 속도, 체력, 타입으로 할 것인지 SpawnDataInfo의 레벨에 따라 지정
        EnemyObj.GetComponent<Enemy>().Init(SpawnDataInfo[Level]);

    }
}

// 직렬화 처리
// [System.Serializable] 작업 안해주면 유니티 에디터에서 해당 클래스의 정보에 대해 보고 작업할 수 없음
[System.Serializable]
public class SpawnData
{
    public float    SpawnTime;
    public float    Speed;
    public int      Type;
    public int      HP;
}