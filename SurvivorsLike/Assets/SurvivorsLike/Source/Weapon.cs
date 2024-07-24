using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int ID;                  // �� ��° ������ ID
    public int PrefabID;            // Pool Manager�� �ִ� �� ��° ������ID
    public int Count;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (ID)
        {
            case 0:
                transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // ������ �ӽ� �׽�Ʈ��
        if (true == Input.GetButtonDown("Jump")) LevelUp(30);
    }

    #region Custom
    // Init�� ID�� ���� �ٸ� ��
    public void Init()
    {
        switch(ID)
        {
            case 0:
                Speed = -150.0f;
                Batch();
                break;
            default:
                break;
        }
    }

    public void LevelUp(float InDamage)
    {
        Damage = InDamage;
        ++Count;

        if (0 == ID) Batch();
    }

    void Batch()
    {
        for(int i = 0; i < Count; ++i)
        {
            // �⺻ ������Ʈ ���� Ȱ��
            // ���ڶ�� Ǯ������ ���
            Transform Bullet = transform.childCount > i? transform.GetChild(i) : GameManager.Instance.Pool.Get(PrefabID).transform;
            
            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            Bullet.parent = transform;                          // PoolManager �θ𿡼� ���� Weapon�� transform���� ����
            Bullet.localPosition = Vector3.zero;                // ���� ������ �ʱ�ȭ
            Bullet.localRotation = Quaternion.identity;         // ���� �����̼� �ʱ�ȭ
            Bullet.Rotate(RotVec);                              // ������ ���� ȸ��
            Bullet.Translate(Bullet.up * 1.5f, Space.World);    // ��ġ
            Bullet.GetComponent<Bullet>().Init(Damage, -1);     // -1: ���� ����, ���� ����
            
        }
    }
    #endregion
}
