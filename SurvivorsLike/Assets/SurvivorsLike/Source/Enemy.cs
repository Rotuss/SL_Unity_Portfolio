using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D Target;
    public RuntimeAnimatorController[] AnimatorController;
    public float Speed;
    public float MaxHP;
    public float HP;

    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    Animator Anim;

    bool IsLive;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        // 씬에 활성화 시 타겟 지정
        Target = GameManager.Instance.MainPlayer.GetComponent<Rigidbody2D>();
        HP = MaxHP;
        IsLive = true;
    }

    private void FixedUpdate()
    {
        if (false == IsLive) return;

        // 방향 구하기
        Vector2 DirVec = Target.position - Rigid.position;
        Vector2 NextVec = DirVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + NextVec);
        Rigid.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (false == IsLive) return;

        // Enemy의 x위치가 Target의 x위치보다 큰 경우(Enemy가 오른쪽 Target이 왼쪽) true(왼쪽을 바라보게 반전O)
        // Enemy의 x위치가 Target의 x위치보다 작은 경우(Enemy가 왼쪽 Target이 오른쪽) false(오른쪽을 바라보게 반전X)
        Spriter.flipX = Target.position.x < Rigid.position.x ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == collision.CompareTag("Bullet")) return;

        HP -= collision.GetComponent<Bullet>().Damage;

        if (0 < HP)
        {
            //
        }
        else
        {
            Dead();
        }
    }

    #region CustomFunc
    // Enemy Spawn시 Init 호출(호출하면서 스폰데이터로 초기화 작업)
    public void Init(SpawnData Data)
    {
        Speed = Data.Speed;
        MaxHP = Data.HP;
        HP = Data.HP;
        Anim.runtimeAnimatorController = AnimatorController[Data.Type];
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
