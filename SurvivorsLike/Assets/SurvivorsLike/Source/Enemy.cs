using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D Target;
    public RuntimeAnimatorController[] AnimatorController;
    public float Speed;
    public float MaxHP;
    public float HP;

    Rigidbody2D Rigid;
    Collider2D Col;
    SpriteRenderer Spriter;
    Animator Anim;
    WaitForFixedUpdate Wait;

    bool IsLive;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Wait = new WaitForFixedUpdate();
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
        Rigid.simulated = true;
        Col.enabled = true;
        IsLive = true;
        Spriter.sortingOrder = 2;
        Anim.SetBool("Dead", false);
    }

    private void FixedUpdate()
    {
        if (true == GameManager.Instance.IsStop) return;
        if (false == IsLive || true == Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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
        if (true == GameManager.Instance.IsStop) return;
        if (false == IsLive) return;

        // Enemy�� x��ġ�� Target�� x��ġ���� ū ���(Enemy�� ������ Target�� ����) true(������ �ٶ󺸰� ����O)
        // Enemy�� x��ġ�� Target�� x��ġ���� ���� ���(Enemy�� ���� Target�� ������) false(�������� �ٶ󺸰� ����X)
        Spriter.flipX = Target.position.x < Rigid.position.x ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == collision.CompareTag("Bullet") || false == IsLive) return;

        HP -= collision.GetComponent<Bullet>().Damage;
        StartCoroutine(KnockBack());
        
        if (0 < HP)
        {
            // Animation
            Anim.SetTrigger("Hit");

            AudioManager.Instance.PlaySFX(AudioManager.ESFX.Hit);
        }
        else
        {
            Rigid.simulated = false;
            Col.enabled = false;
            IsLive = false;
            Spriter.sortingOrder = 1;
            Anim.SetBool("Dead", true);
            ++GameManager.Instance.KillCount;
            GameManager.Instance.GetExp(1);

            if (false == GameManager.Instance.IsStop) AudioManager.Instance.PlaySFX(AudioManager.ESFX.Dead);
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

    IEnumerator KnockBack()
    {
        // ���� �ϳ��� ���� ������ ������(���� ������Ʈ�� ���� ������ ���)
        // FixedUpdate �̵� ���� ������ ������ KnockBack ���� => ���������� KnockBack ��� ��� ����
        yield return Wait;

        Vector3 PlayerPos = GameManager.Instance.MainPlayer.transform.position;
        Vector3 HitDir = (transform.position - PlayerPos).normalized;
        Rigid.AddForce(HitDir * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);

        // ����ġ ����
        GameObject CoinObj = GameManager.Instance.Pool.Get(4);
        CoinObj.transform.position = transform.position;
        CoinObj.GetComponent<Coin>().Init(Random.Range(0, (GameManager.Instance.Level + 1) % 3));
    }

    #endregion
}
