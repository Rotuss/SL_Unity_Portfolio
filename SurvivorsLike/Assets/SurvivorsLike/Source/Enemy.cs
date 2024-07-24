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
        // ���� Ȱ��ȭ �� Ÿ�� ����
        Target = GameManager.Instance.MainPlayer.GetComponent<Rigidbody2D>();
        HP = MaxHP;
        IsLive = true;
    }

    private void FixedUpdate()
    {
        if (false == IsLive) return;

        // ���� ���ϱ�
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

        // Enemy�� x��ġ�� Target�� x��ġ���� ū ���(Enemy�� ������ Target�� ����) true(������ �ٶ󺸰� ����O)
        // Enemy�� x��ġ�� Target�� x��ġ���� ���� ���(Enemy�� ���� Target�� ������) false(�������� �ٶ󺸰� ����X)
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
    // Enemy Spawn�� Init ȣ��(ȣ���ϸ鼭 ���������ͷ� �ʱ�ȭ �۾�)
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
