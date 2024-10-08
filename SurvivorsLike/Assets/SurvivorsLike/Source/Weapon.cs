using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public int ID;                  // 몇 번째 무기의 ID
    public int PrefabID;            // Pool Manager에 있는 몇 번째 프리펩ID
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
        if (true == GameManager.Instance.IsStop) return;

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
            case 2:
                Timer += Time.deltaTime;
                if (Speed < Timer)
                {
                    Timer = 0.0f;
                    Throw();
                }
                break;
            default:
                break;
        }

        // 레벨업 임시 테스트용
        if (true == Input.GetButtonDown("Jump")) LevelUp(30, 1);
    }

    #region Custom
    // Init은 받아오는 ItemData에 따라 다를 것
    public void Init(ItemData Data)
    {
        // 기본 세팅
        name = "Weapon " + Data.ItemID;
        transform.parent = MainPlayer.transform;
        transform.localPosition = Vector3.zero;

        // 프로퍼티 세팅
        Damage = Data.BaseDamage * Passive.Damage;
        ID = Data.ItemID;
        Count = Data.BaseCount + Passive.Count;

        // Data의 Projectile과 맞는 프리팹 찾기
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
                Speed = -150.0f * Passive.WeaponSpeed;
                Batch();
                break;
            case 1:
                Speed = 0.5f * Passive.WeaponRate;
                break;
            case 2:
                Speed = 1.0f * Passive.WeaponRate;
                break;
            default:
                break;
        }

        // 무기 장착 손 설정(근거리0, 원거리1)
        Hand Hand = MainPlayer.Hands[(int)Data.Type];
        Hand.Spriter.sprite = Data.Hand;
        Hand.gameObject.SetActive(true);

        MainPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float InDamage, int InCount)
    {
        Damage = InDamage * Passive.Damage;
        Count += InCount;

        if (0 == ID) Batch();
        MainPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int i = 0; i < Count; ++i)
        {
            // 기본 오브젝트 먼저 활용
            // 모자라면 풀링에서 충당
            Transform Bullet = transform.childCount > i? transform.GetChild(i) : GameManager.Instance.Pool.Get(PrefabID).transform;
            
            Vector3 RotVec = Vector3.forward * 360 * i / Count;
            Bullet.parent = transform;                                          // PoolManager 부모에서 현재 Weapon의 transform으로 변경
            Bullet.localPosition = Vector3.zero;                                // 로컬 포지션 초기화
            Bullet.localRotation = Quaternion.identity;                         // 로컬 로테이션 초기화
            Bullet.Rotate(RotVec);                                              // 개수에 맞춰 회전
            Bullet.Translate(Bullet.up * 1.5f, Space.World);                    // 위치
            Bullet.GetComponent<Bullet>().Init(Damage, -100, Vector3.zero, ID);     // -100: 무한 관통, 근접 공격
            
        }
    }

    void Fire()
    {
        if (null == MainPlayer.Scan.NearestTarget) return;

        // Bullet 방향 구하기
        Vector3 TargetPos = MainPlayer.Scan.NearestTarget.position;
        Vector3 TargetDir = (TargetPos - transform.position).normalized;

        Transform Bullet = GameManager.Instance.Pool.Get(PrefabID).transform;
        Bullet.position = transform.position;                                   // 위치
        Bullet.rotation = Quaternion.FromToRotation(Vector3.up, TargetDir);     // 회전
        Bullet.GetComponent<Bullet>().Init(Damage, Count, TargetDir, ID);

        AudioManager.Instance.PlaySFX(AudioManager.ESFX.Range);
    }

    void Throw()
    {
        float PlayerDirX = 0 == MainPlayer.InputVec.x ? Random.Range(-0.3f, 0.3f) : MainPlayer.InputVec.x * 0.5f;
        Vector3 Dir = Vector3.right * PlayerDirX;
        Vector3 RotVec = Vector3.forward * Random.Range(-20.0f, 20.0f);

        Transform Bullet = GameManager.Instance.Pool.Get(PrefabID).transform;
        Bullet.position = transform.position;                                   // 위치
        Bullet.GetComponent<Bullet>().Init(Damage, Count, Vector3.up + Dir, ID);
        Bullet.Rotate(RotVec);
        AudioManager.Instance.PlaySFX(AudioManager.ESFX.Melee);
    }
    #endregion
}
