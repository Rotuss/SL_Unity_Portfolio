using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int ID;                  // 몇 번째 무기의 ID
    public int PrefabID;            // Pool Manager에 있는 몇 번째 프리펩ID
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
    }

    #region Custom
    // Init은 ID에 따라 다를 것
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

    void Batch()
    {
        for(int i = 0; i < Count; ++i)
        {
            Transform Bullet = GameManager.Instance.Pool.Get(PrefabID).transform;
            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            Bullet.parent = transform;                          // PoolManager 부모에서 현재 Weapon의 transform으로 변경
            Bullet.Rotate(RotVec);                              // 개수에 맞춰 회전
            Bullet.Translate(Bullet.up * 1.5f, Space.World);    // 위치
            Bullet.GetComponent<Bullet>().Init(Damage, -1);     // -1: 무한 관통, 근접 공격
            
        }
    }
    #endregion
}
