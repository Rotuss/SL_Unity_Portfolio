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
        // 씬에 활성화 시 타겟 지정
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
        if (true == GameManager.Instance.IsStop) return;
        if (false == IsLive) return;

        // Enemy의 x위치가 Target의 x위치보다 큰 경우(Enemy가 오른쪽 Target이 왼쪽) true(왼쪽을 바라보게 반전O)
        // Enemy의 x위치가 Target의 x위치보다 작은 경우(Enemy가 왼쪽 Target이 오른쪽) false(오른쪽을 바라보게 반전X)
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
    // Enemy Spawn시 Init 호출(호출하면서 스폰데이터로 초기화 작업)
    public void Init(SpawnData Data)
    {
        Speed = Data.Speed;
        MaxHP = Data.HP;
        HP = Data.HP;
        Anim.runtimeAnimatorController = AnimatorController[Data.Type];
    }

    IEnumerator KnockBack()
    {
        // 다음 하나의 물리 프레임 딜레이(물리 업데이트가 끝날 때까지 대기)
        // FixedUpdate 이동 로직 실행이 끝나면 KnockBack 실행 => 안정적으로 KnockBack 결과 얻기 위함
        yield return Wait;

        Vector3 PlayerPos = GameManager.Instance.MainPlayer.transform.position;
        Vector3 HitDir = (transform.position - PlayerPos).normalized;
        Rigid.AddForce(HitDir * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);

        // 경험치 코인
        GameObject CoinObj = GameManager.Instance.Pool.Get(4);
        CoinObj.transform.position = transform.position;
        CoinObj.GetComponent<Coin>().Init(Random.Range(0, (GameManager.Instance.Level + 1) % 3));
    }

    #endregion
}
