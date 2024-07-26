using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int ID;                  // �� ��° ������ ID
    public int PrefabID;            // Pool Manager�� �ִ� �� ��° ������ID
    public int Count;

    Player MainPlayer;
    float Timer;

    private void Awake()
    {
        MainPlayer = GameManager.Instance.MainPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (ID)
        {
            case 0:
                transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
                break;
            case 1:
                Timer += Time.deltaTime;
                if (Speed < Timer)
                {
                    Timer = 0.0f;
                    Fire();
                }
                break;
            default:
                break;
        }

        // ������ �ӽ� �׽�Ʈ��
        if (true == Input.GetButtonDown("Jump")) LevelUp(30, 1);
    }

    #region Custom
    // Init�� �޾ƿ��� ItemData�� ���� �ٸ� ��
    public void Init(ItemData Data)
    {
        // �⺻ ����
        name = "Weapon " + Data.ItemID;
        transform.parent = MainPlayer.transform;
        transform.localPosition = Vector3.zero;

        // ������Ƽ ����
        Damage = Data.BaseDamage;
        ID = Data.ItemID;
        Count = Data.BaseCount;

        // Data�� Projectile�� �´� ������ ã��
        for(int i = 0; i < GameManager.Instance.Pool.Prefabs.Length; ++i)
        {
            if(Data.Projectile == GameManager.Instance.Pool.Prefabs[i])
            {
                PrefabID = i;
                break;
            }
        }

        switch(ID)
        {
            case 0:
                Speed = -150.0f;
                Batch();
                break;
            case 1:
                Speed = 0.5f;
                break;
            default:
                break;
        }

        MainPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float InDamage, int InCount)
    {
        Damage = InDamage;
        Count += InCount;

        if (0 == ID) Batch();
        MainPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int i = 0; i < Count; ++i)
        {
            // �⺻ ������Ʈ ���� Ȱ��
            // ���ڶ�� Ǯ������ ���
            Transform Bullet = transform.childCount > i? transform.GetChild(i) : GameManager.Instance.Pool.Get(PrefabID).transform;
            
            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            Bullet.parent = transform;                                          // PoolManager �θ𿡼� ���� Weapon�� transform���� ����
            Bullet.localPosition = Vector3.zero;                                // ���� ������ �ʱ�ȭ
            Bullet.localRotation = Quaternion.identity;                         // ���� �����̼� �ʱ�ȭ
            Bullet.Rotate(RotVec);                                              // ������ ���� ȸ��
            Bullet.Translate(Bullet.up * 1.5f, Space.World);                    // ��ġ
            Bullet.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero);       // -1: ���� ����, ���� ����
            
        }
    }

    void Fire()
    {
        if (null == MainPlayer.Scan.NearestTarget) return;

        // Bullet ���� ���ϱ�
        Vector3 TargetPos = MainPlayer.Scan.NearestTarget.position;
        Vector3 TargetDir = (TargetPos - transform.position).normalized;

        Transform Bullet = GameManager.Instance.Pool.Get(PrefabID).transform;
        Bullet.position = transform.position;                                   // ��ġ
        Bullet.rotation = Quaternion.FromToRotation(Vector3.up, TargetDir);     // ȸ��
        Bullet.GetComponent<Bullet>().Init(Damage, Count, TargetDir);
    }
    #endregion
}
