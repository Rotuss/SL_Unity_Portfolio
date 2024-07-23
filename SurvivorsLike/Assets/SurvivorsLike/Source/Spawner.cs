using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ������ ��ġ���� �����ϴ� Transform �迭
    public Transform[] SpawnPoint;
    // ������������ �����ϴ� SpawnData �迭
    public SpawnData[] SpawnDataInfo;

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
        // �迭 ���� �Ѿ ó�� ���� �ʰ� Min ���
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime / 10.0f), SpawnDataInfo.Length - 1);

        // ���� ������ ���� SpawnDataInfo�� Level�� ���� ���� Ÿ�ӿ� ���� Enemy Spawn
        if (SpawnDataInfo[Level].SpawnTime < Timer)
        {
            Timer = 0.0f;
            Spawn();
        }

    }

    void Spawn()
    {
        // ������ Enemy�� Enemy Pool���� Object ��������
        GameObject EnemyObj = GameManager.Instance.Pool.Get(0);
        // ������ Enemy Object ��ġ ����
        EnemyObj.transform.position = SpawnPoint[UnityEngine.Random.Range(1, SpawnPoint.Length)].position;
        // ������ Enemy Object�� � �ӵ�, ü��, Ÿ������ �� ������ SpawnDataInfo�� ������ ���� ����
        EnemyObj.GetComponent<Enemy>().Init(SpawnDataInfo[Level]);

    }
}

// ����ȭ ó��
// [System.Serializable] �۾� �����ָ� ����Ƽ �����Ϳ��� �ش� Ŭ������ ������ ���� ���� �۾��� �� ����
[System.Serializable]
public class SpawnData
{
    public float    SpawnTime;
    public float    Speed;
    public int      Type;
    public int      HP;
}